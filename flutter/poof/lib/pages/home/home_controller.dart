import 'dart:developer';

import 'package:bang/cards/widgets/button.dart';
import 'package:bang/routes/routes.dart';
import 'package:bang/services/audio_service.dart';
import 'package:bang/services/game_service.dart';
import 'package:bang/services/shared_preference_service.dart';
import 'package:flutter/material.dart';
import 'package:fluttertoast/fluttertoast.dart';
import 'package:get/get.dart';
import 'package:qr_code_scanner/qr_code_scanner.dart';

class HomeController extends GetxController {
  final GlobalKey _qrKey = GlobalKey(debugLabel: 'QR');
  QRViewController? controller;

  Rx<String?> roomCodeToJoin = ''.obs;

  void joinRoom([String? roomId]) {
    var roomID = roomId ?? roomCodeToJoin.value;
    if (roomID == null) return;
    log(roomID);
    var gameService = Get.put(GameService());
    Get.toNamed(Routes.LOBBY);
    gameService.roomId = roomID.obs;
    AudioService.playBackgroundMusic();
  }

  void logout() {
    SharedPreferenceService.token = '';
    Get.offAndToNamed(Routes.LOGIN);
  }

  void readQR() {
    Get.defaultDialog(
        title: 'Olvasd be a kódot!',
        onCancel: () {
          controller?.dispose();
          log('contoller disposed');
        },
        cancel: BangButton(
          text: 'Vissza',
          height: 50,
          width: 90,
          isNormal: false,
          onPressed: () {
            controller?.dispose();
            Get.back();
            log('contoller disposed');
          },
        ),
        onWillPop: () async {
          controller?.dispose();
          Get.back();
          log('contoller disposed');
          return false;
        },
        content: Container(
          width: 300,
          height: 300,
          child: QRView(
            key: _qrKey,
            onQRViewCreated: _onQRViewCreated,
          ),
        ));
  }

  void _onQRViewCreated(QRViewController controller) {
    this.controller = controller;
    controller.scannedDataStream
        .listen((scanData) => _onQRReadSuccessfully(scanData.code));
  }

  void _onQRReadSuccessfully(String qrValue) {
    Get.back();
    controller?.dispose();
    joinRoom(qrValue);
    Fluttertoast.showToast(
        msg: "Joining to room with code: $qrValue",
        toastLength: Toast.LENGTH_LONG,
        gravity: ToastGravity.BOTTOM,
        timeInSecForIosWeb: 1,
        backgroundColor: Colors.red,
        textColor: Colors.white,
        fontSize: 16.0);
  }

  void createGame() {
    Get.put(GameService());
    Get.toNamed(Routes.LOBBY);
  }
}
