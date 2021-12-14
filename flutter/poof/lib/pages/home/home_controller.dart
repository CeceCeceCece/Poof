import 'dart:developer';

import 'package:bang/core/lang/app_strings.dart';
import 'package:bang/routes/routes.dart';
import 'package:bang/services/lobby_service.dart';
import 'package:bang/services/shared_preference_service.dart';
import 'package:bang/widgets/bang_button.dart';
import 'package:flutter/material.dart';
import 'package:fluttertoast/fluttertoast.dart';
import 'package:get/get.dart';
import 'package:qr_code_scanner/qr_code_scanner.dart';

class HomeController extends GetxController {
  final GlobalKey _qrKey = GlobalKey(debugLabel: 'QR');
  QRViewController? controller;

  void joinRoom(String roomId) => _initalizeLobbyService()
      .then((service) => service.joinLobby(lobbyName: roomId));

  Future<LobbyService> _initalizeLobbyService() async {
    var service = Get.put(LobbyService());
    await service.reconnect();
    return service;
  }

  void logout() {
    Get.offAndToNamed(Routes.LOGIN_AND_REGISTER);
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
    Fluttertoast.showToast(
        msg: AppStrings.joining_room.trParams({'lobbyName': qrValue}));
  }

  void createGame(String lobbyName) => _initalizeLobbyService()
      .then((service) => service.createLobby(lobbyName: lobbyName));
}
