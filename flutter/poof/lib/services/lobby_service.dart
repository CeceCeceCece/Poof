import 'dart:async';

import 'package:bang/core/lang/app_strings.dart';
import 'package:bang/models/lobby_dto.dart';
import 'package:bang/models/message_dto.dart';
import 'package:bang/models/user_dto.dart';
import 'package:bang/network/websocket/lobby_provider.dart';
import 'package:bang/pages/lobby/lobby_controller.dart';
import 'package:bang/routes/routes.dart';
import 'package:bang/services/service_base.dart';
import 'package:fluttertoast/fluttertoast.dart';
import 'package:get/get.dart';
import 'package:get/get_core/src/get_main.dart';
import 'package:get/get_instance/src/extension_instance.dart';

import 'audio_service.dart';

class LobbyService extends ServiceBase {
  var delay = Duration(milliseconds: 150);

  String? gameId;

  var admin = ''.obs;
  var users = <UserDto>[].obs;
  var messages = <MessageDto>[].obs;
  var lobbyName = ''.obs;
  var connectionInitialized = false;
  var notInGame = true;
  late LobbyProvider provider;

  @override
  void onInit() async {
    provider = Get.put(LobbyProvider());
    await provider.initWebsocket();
    super.onInit();
  }

  void onLobbyJoined(LobbyDto lobby) {
    AudioService.playBackgroundMusic();
    Get.toNamed(Routes.LOBBY);
    admin.value = lobby.owner;
    users.value = lobby.users;
    lobbyName.value = lobby.name;
  }

  void onLobbyCreated(LobbyDto lobby) async {
    AudioService.playBackgroundMusic();
    Get.toNamed(Routes.LOBBY);
    admin.value = lobby.owner;
    users.value = lobby.users;
    lobbyName.value = lobby.name;
  }

  void onLobbyDeleted(String lobbyName) {
    Fluttertoast.showToast(
      msg: AppStrings.room_deleted.trParams({'roomName': lobbyName}),
      toastLength: Toast.LENGTH_SHORT,
    );
    Get.back();
  }

  void onSetUsers(List<UserDto> lobbyUsers) => users.value = lobbyUsers;

  void onSetMessages(List<MessageDto> lobbyMessages) =>
      messages.value = lobbyMessages;

  void onUserEntered(UserDto user) {
    Future.delayed(delay, () {
      var controller = Get.find<LobbyController>();
      users.add(user);
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

  void onUserLeft(String userId) {
    var userThatLeft = users.firstWhere((user) => user.id == userId);
    if (notInGame)
      Get.find<LobbyController>().removeUserCallback(userThatLeft.name);
    users.removeWhere((element) => element.id == userId);
  }

  void onRecieveMessage(MessageDto message) {
    messages.add(message);
    Get.find<LobbyController>().scrollToBottom();
  }

  void onGameCreated(String gameId) {
    Get.find<LobbyController>().joinGame(gameId);
  }

  void createLobby({required String lobbyName}) async {
    await provider.createLobby();
  }

  void joinLobby({required String lobbyName}) async {}

  Future<void> disconnectLobby() async {
    await provider.disconnect();
  }

  void removeUser(String userId) async {
    provider.removeUser(userId);
  }

  void sendMessage({required String message}) async {
    if (message.isNotEmpty) {
      await provider.sendMessage(
        message: message,
      );
    }
  }

  void createGame() async => await provider.createGame();

  Future<void> reconnect() async {
    await provider.disconnect();
    await provider.initWebsocket();
  }
}
