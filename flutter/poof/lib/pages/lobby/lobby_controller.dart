import 'package:bang/core/lang/app_strings.dart';
import 'package:bang/models/message_dto.dart';
import 'package:bang/models/user_dto.dart';
import 'package:bang/routes/routes.dart';
import 'package:bang/services/audio_service.dart';
import 'package:bang/services/auth_service.dart';
import 'package:bang/services/game_service.dart';
import 'package:bang/services/lobby_service.dart';
import 'package:bang/services/shared_preference_service.dart';
import 'package:bang/widgets/bang_button.dart';
import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:fluttertoast/fluttertoast.dart';
import 'package:get/get.dart';
import 'package:qr_flutter/qr_flutter.dart';

class LobbyController extends GetxController {
  var lobbyService = Get.find<LobbyService>();
  var lobbyName = ''.obs;
  var admin = ''.obs;

  var modalSheetScrollController = ScrollController();
  var chatTextController = TextEditingController();
  StateSetter? onMessageArrivedCallback;
  var messages = <MessageDto>[].obs;
  var showingQr = false.obs;
  var users = <UserDto>[].obs;
  late String playerName;

  bool get playerIsLobbyAdmin => admin() == playerName;
  @override
  void onInit() async {
    admin = lobbyService.admin;
    messages = lobbyService.messages;
    users = lobbyService.users;
    lobbyName = lobbyService.lobbyName;
    playerName = Get.find<AuthService>().player;
    await SystemChrome.setEnabledSystemUIMode(SystemUiMode.manual,
        overlays: []);
    super.onInit();
  }

  void scrollToBottom() {
    Future.delayed(
      Duration(milliseconds: 100),
      () {
        onMessageArrivedCallback?.call(() {});
        modalSheetScrollController.animateTo(
          modalSheetScrollController.position.maxScrollExtent + 100,
          curve: Curves.fastLinearToSlowEaseIn,
          duration: Duration(milliseconds: 300),
        );
      },
    );
  }

  void sendMessage() {
    var message = chatTextController.text;
    lobbyService.sendMessage(message: message);
    chatTextController.clear();
  }

  void showQR() {
    var qrValue = lobbyName();
    showingQr.value = true;
    Get.defaultDialog(
      title: AppStrings.read_the_code.tr,
      onCancel: resetQRBoolean,
      cancel: BangButton(
          text: AppStrings.back.tr,
          height: 50,
          width: 90,
          isNormal: false,
          onPressed: () {
            resetQRBoolean();
            Get.back();
          }),
      onWillPop: () async {
        resetQRBoolean();
        Get.back();
        return false;
      },
      content: Container(
        width: 250,
        height: 250,
        child: QrImage(
          data: qrValue,
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
    var gameService = Get.put(GameService());
    gameService.roomId.value = lobbyName();
    Get.offAndToNamed(Routes.GAME);
  }

  void resetQRBoolean() {
    showingQr.value = false;
  }

  void removeUser(UserDto user) {
    if (user.name != Get.find<AuthService>().player)
      lobbyService.removeUser(user.id);
  }

  void removeUserCallback(String username) {
    if (username == SharedPreferenceService.name)
      back();
    else
      Fluttertoast.showToast(
        msg: AppStrings.user_left_room.trParams({'player': username}),
        toastLength: Toast.LENGTH_SHORT,
      );
  }

  bool isAdmin(UserDto user) => user.name == admin.value;

  void refreshUI() {
    refresh();
  }

  void back() async {
    await lobbyService.disconnectLobby();
    Get.back();
    AudioService.playMenuSong();
  }
}
