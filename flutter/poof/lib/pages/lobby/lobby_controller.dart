import 'package:bang/cards/widgets/button.dart';
import 'package:bang/models/message_dto.dart';
import 'package:bang/models/user_dto.dart';
import 'package:bang/routes/routes.dart';
import 'package:bang/services/audio_service.dart';
import 'package:bang/services/auth_service.dart';
import 'package:bang/services/game_service.dart';
import 'package:bang/services/lobby_service.dart';
import 'package:bang/services/shared_preference_service.dart';
import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:fluttertoast/fluttertoast.dart';
import 'package:get/get.dart';
import 'package:qr_flutter/qr_flutter.dart';

class LobbyController extends GetxController {
  var lobbyService = Get.find<LobbyService>();
  String? get roomID => lobbyService.roomID;
  var modalSheetScrollController = ScrollController();

  var showingQr = false.obs;
  var admin = 'default_playername'.obs;

  var messages = <MessageDto>[].obs;
  var users = <UserDto>[].obs;

  bool get playerIsLobbyAdmin => admin() == Get.find<AuthService>().player;
  @override
  void onInit() async {
    await SystemChrome.setEnabledSystemUIMode(SystemUiMode.manual, overlays: []);
    super.onInit();
  }

  void showQR([String? data]) {
    var qrValue = data ?? (roomID ?? null);
    if (qrValue == null) return;
    showingQr.value = true;
    Get.defaultDialog(
      title: 'Olvasd be a kódot!',
      onCancel: resetQRBoolean,
      cancel: BangButton(
          text: 'Vissza',
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
    gameService.roomId.value = roomID;
    Get.offAndToNamed(Routes.GAME);
  }

  void resetQRBoolean() {
    showingQr.value = false;
  }

  void removeUser(UserDto user) {
    lobbyService.removeUser(user.id);
  }

  void removeUserCallback(String userId) {
    var userThatLeft = users.firstWhere((user) => user.id == userId);

    users.removeWhere((element) => element.id == userId);
    if (userThatLeft.name == SharedPreferenceService.name)
      back();
    else
      Fluttertoast.showToast(
        msg: '${userThatLeft.name} kilépett a szobából!',
        toastLength: Toast.LENGTH_SHORT,
      );
  }

  bool isAdmin(UserDto user) => user.name == admin.value;

  void toggleAdmin(UserDto user) {
    Fluttertoast.showToast(msg: '${user.name} is now admin', gravity: ToastGravity.BOTTOM);
  }

  void refreshUI() {
    refresh();
  }

  void back() async {
    Get.back();
    await lobbyService.disconnectLobby();
    AudioService.playMenuSong();
  }
}
