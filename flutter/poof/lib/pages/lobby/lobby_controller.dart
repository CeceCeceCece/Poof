import 'dart:developer';

import 'package:bang/core/constants.dart';
import 'package:bang/models/lobby_dto.dart';
import 'package:bang/models/message_dto.dart';
import 'package:bang/models/user_dto.dart';
import 'package:bang/routes/routes.dart';
import 'package:bang/services/audio_service.dart';
import 'package:bang/services/game_service.dart';
import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:fluttertoast/fluttertoast.dart';
import 'package:get/get.dart';
import 'package:qr_flutter/qr_flutter.dart';
import 'package:signalr_core/signalr_core.dart';

class LobbyController extends GetxController {
  late final HubConnection _connection;
  String? get roomID => Get.find<GameService>().roomId.value;

  var playerIsLobbyAdmin = true.obs;
  @override
  void onInit() async {
    refreshUI();
    await SystemChrome.setEnabledSystemUIMode(SystemUiMode.manual,
        overlays: []);
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

  var playerList = [
    'asfsagags',
    'asgagagdfhh',
    'Ferike',
    'safffff',
    'ddddddd',
    'EGYNAGYONHOSSZÚNEVŰFELHASZNÁLÓXDDDDDDDDDD',
    'HGKJGKUJZGK'
  ].obs;
  List<Widget> playerWidgetList = [];

  void join() {
    Get.offAndToNamed(Routes.GAME);
  }

  void addPlayers(List<String> players) {
    playerList.addAll(players);
    refreshUI();
  }

  void removePlayer(String id) {
    if (!isAdmin(id)) playerList.removeWhere((element) => element == id);
    refreshUI();
  }

  bool isAdmin(String player) => player == 'B1GD1CK';

  void toggleAdmin(String playerID) {
    Fluttertoast.showToast(
        msg: '$playerID is now admin', gravity: ToastGravity.BOTTOM);
    refreshUI();
  }

  void refreshUI() {
    var widgetList = <Widget>[];

    for (int i = 0; i < playerList.length; i++)
      widgetList.add(playerIsLobbyAdmin()
          ? Dismissible(
              confirmDismiss: (DismissDirection details) async =>
                  Future.delayed(Duration(seconds: 1), () {
                return !isAdmin(playerList[i]);
              }),
              onDismissed: (_) => removePlayer(playerList[i]),
              key: Key(playerList[i]),
              background: Container(
                alignment: Alignment.centerLeft,
                color: Colors.transparent,
                child: Row(
                  children: [
                    SizedBox(
                      width: 30,
                    ),
                    Icon(Icons.highlight_remove_sharp, color: Colors.white),
                  ],
                ),
              ),
              secondaryBackground: Container(
                alignment: Alignment.centerRight,
                color: Colors.transparent,
                child: Row(
                  mainAxisAlignment: MainAxisAlignment.end,
                  children: [
                    Icon(Icons.highlight_remove_sharp, color: Colors.white),
                    SizedBox(
                      width: 30,
                    ),
                  ],
                ),
              ),
              child: Padding(
                padding: EdgeInsets.fromLTRB(30, 3, 30, 3),
                child: Container(
                  height: 35,
                  decoration: BoxDecoration(
                      borderRadius: BorderRadius.circular(10.0),
                      color: Colors.white),
                  child: Row(
                      mainAxisAlignment: MainAxisAlignment.center,
                      crossAxisAlignment: CrossAxisAlignment.center,
                      children: [
                        SizedBox(
                          width: 35,
                        ),
                        Expanded(
                          child: Container(
                            child: Text(
                              playerList[i],
                              overflow: TextOverflow.ellipsis,
                              maxLines: 1,
                              //softWrap: false,
                            ),
                          ),
                        ),
                        IconButton(
                          icon: Icon(
                            Icons.star,
                            color: isAdmin(playerList[i])
                                ? Colors.amber
                                : Colors.grey,
                          ),
                          onPressed: () => toggleAdmin(playerList[i]),
                        ),
                        SizedBox(
                          width: 20,
                        )
                      ]),
                ),
              ),
            )
          : Padding(
              padding: EdgeInsets.fromLTRB(30, 3, 30, 3),
              child: Flexible(
                child: Container(
                  height: 35,
                  decoration: BoxDecoration(
                      borderRadius: BorderRadius.circular(10.0),
                      color: Colors.white),
                  child: Row(
                      mainAxisAlignment: MainAxisAlignment.center,
                      crossAxisAlignment: CrossAxisAlignment.center,
                      children: [
                        SizedBox(
                          width: 35,
                        ),
                        Expanded(
                          child: Text(
                            playerList[i],
                            overflow: TextOverflow.ellipsis,
                            maxLines: 1,
                            //softWrap: false,
                          ),
                        ),
                        isAdmin(playerList[i])
                            ? IconButton(
                                icon: Icon(Icons.star, color: Colors.amber),
                                onPressed: null,
                              )
                            : Container(),
                        SizedBox(
                          width: 20,
                        )
                      ]),
                ),
              ),
            ));

    playerWidgetList = widgetList.obs;
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
              logging: (level, message) => print(message),
            ))
        .withAutomaticReconnect()
        .build();
    try {
      await _connection.start();
    } catch (error) {
      log('$error');
    }

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
  }

  void _lobbyDeleted(String lobbyName) {
    log('Arrived:$lobbyName');
  }

  void _setUsers(List<UserDto> users) {
    log('Arrived:${users.toString()}');
  }

  void _setMessages(List<MessageDto> messages) {
    log('Arrived:${messages.toString()}');
  }

  void _userEntered(UserDto user) {
    log('Arrived:$user');
  }

  void _userLeft(String userId) {
    log('Arrived:$userId');
  }

  void _recieveMessage(MessageDto message) {
    log('Arrived:$message');
  }

  void _gameCreated(String gameId) {
    log('Arrived:$gameId');
  }

  void createLobby({required String lobbyName}) async {
    await _connection.invoke('CreateLobby', args: [lobbyName]);
  }

  void joinLobby({required String lobbyName}) async {
    await _connection.invoke('JoinLobby', args: [lobbyName]);
  }

  void disconnectLobby() async {
    await _connection.invoke('DsiconnectLobby', args: []);
  }

  void sendMessage({required String message, required String username}) async {
    await _connection.invoke('SendMessage', args: [message, username]);
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
