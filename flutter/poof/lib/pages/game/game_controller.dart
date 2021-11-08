import 'dart:developer';
import 'dart:math' as Math;

import 'package:bang/core/constants.dart';
import 'package:bang/services/audio_service.dart';
import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:get/get.dart';
import 'package:signalr_core/signalr_core.dart';

class GameController extends GetxController {
  var _exitConfirmed = false;
  var playerNumber = 4.obs;

  void randomizePlayerNumber() {
    playerNumber.value = Math.Random().nextInt(4) + 4;
    log('PLAYERS: $playerNumber');
  }

  Future<bool> showBackPopupForResult() async {
    await Get.defaultDialog<bool>(
      title: 'Really?!',
      onWillPop: () => Future.value(true),
      onConfirm: _exit,
      content: Text('Do you really wish to exit this game?'),
    );
    return Future.value(_exitConfirmed);
  }

  void _exit() {
    _exitConfirmed = true;
    Get.back();
    AudioService.playMenuSong();
  }

  @override
  void onInit() async {
    //initWebsocket();
    await SystemChrome.setEnabledSystemUIMode(SystemUiMode.manual,
        overlays: []);
    super.onInit();
  }

  Future<void> initWebsocket() async {
    final connection = HubConnectionBuilder()
        .withUrl(
            Constants.BASE_URL + 'HUBNAME', // TODO
            HttpConnectionOptions(
              logging: (level, message) => print(message),
            ))
        .withAutomaticReconnect()
        .build();
    try {
      await connection.start();
    } catch (error) {
      log('$error');
    }

    connection.on('SendMessage', (message) {
      log(message.toString());
    });
  }
}
