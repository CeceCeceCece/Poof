import 'dart:async';
import 'dart:developer';

import 'package:bang/core/app_constants.dart';
import 'package:bang/core/lang/app_strings.dart';
import 'package:bang/models/lobby_dto.dart';
import 'package:bang/models/message_dto.dart';
import 'package:bang/models/user_dto.dart';
import 'package:bang/pages/lobby/lobby_controller.dart';
import 'package:bang/routes/routes.dart';
import 'package:bang/services/service_base.dart';
import 'package:bang/services/shared_preference_service.dart';
import 'package:flutter/cupertino.dart';
import 'package:fluttertoast/fluttertoast.dart';
import 'package:get/get.dart';
import 'package:get/get_core/src/get_main.dart';
import 'package:get/get_instance/src/extension_instance.dart';
import 'package:signalr_core/signalr_core.dart';

import 'audio_service.dart';

class LobbyService extends ServiceBase {
  var isPlayerInsideLobby = false;
  var delay = Duration(milliseconds: 150);
  var statusInterval = Duration(seconds: 10);
  String? roomID;
  Timer? statusTimer;
  StateSetter? onMessageArrivedCallback;

  late HubConnection _connection;
  Future<void> initWebsocket() async {
    _connection = HubConnectionBuilder()
        .withUrl(
          AppConstants.BASE_URL + AppConstants.LOBBY_HUB,
          HttpConnectionOptions(
            transport: HttpTransportType.longPolling,
            logging: (level, message) => print('SIGNALR ---- $message'),
            accessTokenFactory: () async => SharedPreferenceService.token,
          ),
        )
        .withHubProtocol(JsonHubProtocol())
        .withAutomaticReconnect(
          DefaultReconnectPolicy(
            retryDelays: [
              0,
              2000,
              10000,
              30000,
              60000,
              60000,
              60000,
              null,
            ],
          ),
        )
        .build();
    try {
      await _connection.start();
    } catch (error) {
      log('$error');
      _connection.start();
    }
    _connection.onreconnected((connectionId) {
      log('RECONNECTED');
      //statusTimer = Timer.periodic(statusInterval, (_) => _status()); //TODO
    });
    _connection.onreconnecting((exception) {
      log(exception.toString());
    });
    _connection.onclose((exception) {
      log('$exception');
      statusTimer?.cancel();
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
      'LobbyJoined',
      (lobby) => _lobbyJoined(
        LobbyDto.fromJson(lobby?[0]),
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
      'OnStatus',
      (_) => _onStatus(),
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

  void _lobbyJoined(LobbyDto lobby) {
    log('$lobby');
    AudioService.playBackgroundMusic();
    Get.toNamed(Routes.LOBBY);
    Future.delayed(delay, () {
      var lobbyController = Get.find<LobbyController>();
      lobbyController.admin.value = lobby.owner;
      lobbyController.users.value = lobby.users;
      roomID = lobby.name;
      lobbyController.refreshUI();
    });
  }

  void _lobbyCreated(LobbyDto lobby) async {
    log('Arrived:$lobby');

    AudioService.playBackgroundMusic();
    Get.toNamed(Routes.LOBBY);
    statusTimer = Timer.periodic(statusInterval, (_) => _status());
    Future.delayed(delay, () {
      var lobbyController = Get.find<LobbyController>();
      lobbyController.admin.value = lobby.owner;
      lobbyController.users.value = lobby.users;
      roomID = lobby.name;
      lobbyController.refreshUI();
    });
  }

  void _lobbyDeleted(String lobbyName) {
    log('Arrived:$lobbyName');
    Fluttertoast.showToast(
      msg: AppStrings.room_deleted.trParams({'roomName': lobbyName}),
      toastLength: Toast.LENGTH_SHORT,
    );
    Get.back();
  }

  void _setUsers(List<UserDto> lobbyUsers) {
    log('Arrived:${lobbyUsers.toString()}');
    Get.find<LobbyController>().users.value = lobbyUsers;
  }

  void _setMessages(List<MessageDto> lobbyMessages) {
    log('Arrived:${lobbyMessages.toString()}');
    Get.find<LobbyController>().messages.value = lobbyMessages;
  }

  void _status() {
    _connection.invoke('Status', args: [roomID]);
  }

  void _onStatus() {
    log('Status recieved');
  }

  void _userEntered(UserDto user) {
    log('Arrived:${user.name}');
    Future.delayed(delay, () {
      var controller = Get.find<LobbyController>();
      controller.users.add(user);
      if (controller.showingQr()) {
        controller.resetQRBoolean();
        Get.back();
      }
      controller.refreshUI();
    });

    Fluttertoast.showToast(
      msg: AppStrings.user_connected.trParams({'userName': user.name}),
    );
  }

  void _userLeft(String userId) {
    log('Arrived:$userId');
    Get.find<LobbyController>().removeUserCallback(userId);
  }

  void _recieveMessage(MessageDto message) {
    log('Arrived:$message');
    var controller = Get.find<LobbyController>();
    controller.messages.add(message);
    onMessageArrivedCallback?.call(() {});
    controller.modalSheetScrollController.animateTo(
        controller.modalSheetScrollController.position.maxScrollExtent + 100,
        curve: Curves.fastLinearToSlowEaseIn,
        duration: Duration(milliseconds: 300));
    controller.refreshUI();
  }

  void _gameCreated(String gameId) {
    log('Arrived:$gameId');
    Get.find<LobbyController>().join();
  }

  void createLobby({required String lobbyName}) async {
    log('${_connection.state}');
    await _connection.invoke('CreateLobby', args: [lobbyName]);
    log('success!');
  }

  void joinLobby({required String lobbyName}) async {
    await _connection.invoke('JoinLobby', args: [lobbyName]);
  }

  Future<void> disconnectLobby() async {
    await _connection.invoke('DisconnectLobby', args: []);
    disconnect();
  }

  void removeUser(String userId) async {
    await _connection.invoke('DeletePlayer', args: [userId]);
  }

  void sendMessage({required String message}) async {
    if (message.isNotEmpty) {
      await _connection.invoke('SendMessage', args: [
        message,
        roomID,
      ]).then(
        (value) => print('MESSAGE SENT!'),
      );
    }
  }

  Future<void> disconnect() async {
    statusTimer?.cancel();
    await _connection.stop();
  }

  @override
  void onClose() async {
    disconnect();
    super.onClose();
  }
}
