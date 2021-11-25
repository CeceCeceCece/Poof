import 'dart:developer';

import 'package:bang/core/constants.dart';
import 'package:bang/models/lobby_dto.dart';
import 'package:bang/models/message_dto.dart';
import 'package:bang/models/user_dto.dart';
import 'package:bang/routes/routes.dart';
import 'package:bang/services/audio_service.dart';
import 'package:bang/services/auth_service.dart';
import 'package:bang/services/game_service.dart';
import 'package:bang/services/shared_preference_service.dart';
import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:fluttertoast/fluttertoast.dart';
import 'package:get/get.dart';
import 'package:qr_flutter/qr_flutter.dart';
import 'package:signalr_core/signalr_core.dart';

class LobbyController extends GetxController {
  late final HubConnection _connection;
  String? get roomID => Get.find<GameService>().roomId.value;

  var admin = 'default_playername'.obs;

  var messages = <MessageDto>[].obs;
  var users = <UserDto>[UserDto(name: 'Én', id: 'id2')].obs;

  bool get playerIsLobbyAdmin => admin() == Get.find<AuthService>().player;
  @override
  void onInit() async {
    await SystemChrome.setEnabledSystemUIMode(SystemUiMode.manual,
        overlays: []);
    await initWebsocket();
    createLobby(lobbyName: roomID ?? 'RANDOM');
    super.onInit();
  }

  void showQR([String? data]) {
    Get.defaultDialog(
      content: Container(
        width: 250,
        height: 250,
        child: QrImage(
          data: data ?? (roomID ?? 'RANDOM'),
          version: QrVersions.auto,
          errorCorrectionLevel: QrErrorCorrectLevel.Q,
          size: 100.0,
          embeddedImage: AssetImage(
            'assets/icons/bang_logo.png',
          ),
          embeddedImageStyle: QrEmbeddedImageStyle(size: Size(30, 30)),
        ),
      ),
    );
  }

  void join() {
    Get.offAndToNamed(Routes.GAME);
  }

  void removeUser(UserDto user) {
    if (!isAdmin(user)) users.removeWhere((element) => element.id == user.id);
  }

  bool isAdmin(UserDto user) =>
      true; //user.name == Get.find<AuthService>().player;

  void toggleAdmin(UserDto user) {
    Fluttertoast.showToast(
        msg: '${user.name} is now admin', gravity: ToastGravity.BOTTOM);
  }

  void refreshUI() {
    refresh();
  }

  void back() {
    Get.back();
    AudioService.playMenuSong();
  }

  Future<void> initWebsocket() async {
    _connection = HubConnectionBuilder()
        .withUrl(
          Constants.BASE_URL + Constants.LOBBY_HUB,
          HttpConnectionOptions(
            transport: HttpTransportType.longPolling,
            logging: (level, message) => print('SIGNALR ---- $message'),
            accessTokenFactory: () async => SharedPreferenceService.token,
          ),
        )
        .withHubProtocol(JsonHubProtocol())
        .withAutomaticReconnect()
        .build();
    try {
      _connection.start()?.then((value) =>
          createLobby(lobbyName: 'lobbyName')); // !visszakommentezni!!!
    } catch (error) {
      log('$error');
      _connection.start();
    }
    _connection.onreconnected((connectionId) {
      log('RECONNECTED');
    });
    _connection.onreconnecting((exception) {
      log(exception.toString());
    });
    _connection.onclose((exception) {
      log('$exception');
    });

    _connection.on(
      'LobbyCreated',
      (lobby) => _lobbyCreated(
        LobbyDto.fromJson(lobby?[0]),
      ),
    );

    _connection.on(
      'LobbyDeleted',
      (name) => _lobbyDeleted(
        name?[0],
      ),
    );

    _connection.on(
      'SetUsers',
      (users) => _setUsers(
        users?.map((e) => UserDto.fromJson(e)).toList() ?? [],
      ),
    );

    _connection.on(
      'SetMessages',
      (messages) => _setMessages(
        messages?.map((e) => MessageDto.fromJson(e)).toList() ?? [],
      ),
    );

    _connection.on(
      'UserEntered',
      (user) => _userEntered(
        UserDto.fromJson(
          user?[0],
        ),
      ),
    );

    _connection.on(
      'UserLeft',
      (userId) => _userLeft(
        userId?[0],
      ),
    );

    _connection.on(
      'RecieveMessage',
      (message) => _recieveMessage(
        MessageDto.fromJson(
          message?[0],
        ),
      ),
    );

    _connection.on(
      'GameCreated',
      (gameId) => _gameCreated(
        gameId?[0],
      ),
    );
  }

  void _lobbyCreated(LobbyDto lobby) {
    log('Arrived:$lobby');
    Get.find<GameService>().roomId = lobby.name.obs;
    admin = lobby.owner.obs;
  }

  void _lobbyDeleted(String lobbyName) {
    log('Arrived:$lobbyName');
    Fluttertoast.showToast(
      msg: 'A $lobbyName szobát törölték!',
      toastLength: Toast.LENGTH_SHORT,
    );
    Get.back();
  }

  void _setUsers(List<UserDto> lobbyUsers) {
    log('Arrived:${lobbyUsers.toString()}');
    users = lobbyUsers.obs;
  }

  void _setMessages(List<MessageDto> lobbyMessages) {
    log('Arrived:${lobbyMessages.toString()}');
    messages = lobbyMessages.obs;
  }

  void _userEntered(UserDto user) {
    log('Arrived:${user.name}');
    users.add(user);
    Fluttertoast.showToast(
      msg: '${user.name} csatlakozott a szobához!',
      toastLength: Toast.LENGTH_SHORT,
    );
  }

  void _userLeft(String userId) {
    log('Arrived:$userId');
    var userThatLeft = users.firstWhere((user) => user.id == userId);
    users.removeWhere((user) => user.id == userId);
    Fluttertoast.showToast(
      msg: '${userThatLeft.name} kilépett a szobából!',
      toastLength: Toast.LENGTH_SHORT,
    );
  }

  void _recieveMessage(MessageDto message) {
    log('Arrived:$message');
    messages.add(message);
  }

  void _gameCreated(String gameId) {
    log('Arrived:$gameId');
    // TODO
  }

  void createLobby({required String lobbyName}) async {
    await _connection.invoke('CreateLobby', args: [lobbyName]);
    log('success!');
  }

  void joinLobby({required String lobbyName}) async {
    await _connection.invoke('JoinLobby', args: [lobbyName]);
  }

  void disconnectLobby() async {
    await _connection.invoke('DisconnectLobby', args: []);
  }

  void sendMessage({required String message, required String username}) async {
    if (message.isNotEmpty)
      _recieveMessage(MessageDto(
          sender: username, text: message, postedDate: DateTime.now()));
    //await _connection.invoke('SendMessage', args: [message, username]);
  }

  void deleteLobby({required String lobbyName}) async {
    await _connection.invoke('DeleteLobby', args: [lobbyName]);
  }

  @override
  void onClose() async {
    await _connection.stop();
    super.onClose();
  }
}
