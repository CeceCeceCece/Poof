import 'dart:developer';

import 'package:bang/cards/widgets/button.dart';
import 'package:bang/core/lang/app_strings.dart';
import 'package:bang/routes/routes.dart';
import 'package:bang/services/lobby_service.dart';
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
    findOrPutLobbyService()
        .then((service) => service.joinLobby(lobbyName: roomID));
  }

  Future<LobbyService> findOrPutLobbyService() async {
    var service = Get.find<LobbyService>();
    if (service.isPlayerInsideLobby) await service.disconnect();
    service.isPlayerInsideLobby = false;
    await service.initWebsocket();

    return service;
  }

  void logout() {
    Get.offAndToNamed(Routes.LOGIN);
    SharedPreferenceService.removeCredentials();
  }

  void readQR() {
    Get.defaultDialog(
        title: AppStrings.read_the_code.tr,
        onCancel: () {
          controller?.dispose();
          log('contoller disposed');
        },
        cancel: BangButton(
          text: AppStrings.back.tr,
          height: 50,
          width: 90,
          isNormal: false,
          onPressed: () {
            controller?.dispose();

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
    Fluttertoast.showToast(msg: AppStrings.joining_room.tr + qrValue);
  }

  void createGame(String? lobbyName) {
    if (lobbyName == null) return;
    findOrPutLobbyService()
        .then((service) => service.createLobby(lobbyName: lobbyName));
  }
}
