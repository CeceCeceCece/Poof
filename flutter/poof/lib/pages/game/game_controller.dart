import 'dart:developer';
import 'dart:math' as Math;

import 'package:bang/cards/widgets/button.dart';
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
      title: 'Megerősítés szükséges',
      onWillPop: () => Future.value(true),
      onConfirm: _exit,
      confirm: BangButton(
        text: 'Értem, kilépek!',
        onPressed: _exit,
        height: 35,
        width: 105,
        isNormal: false,
      ),
      content: Text(
        'Biztosan ki akarsz lépni a játékból?\n\nTöbbszöri kilépés szankciókat vonhat maga után!',
        textAlign: TextAlign.center,
      ),
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
