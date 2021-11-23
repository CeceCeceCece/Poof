import 'dart:developer';
import 'dart:math' as Math;

import 'package:bang/cards/widgets/button.dart';
import 'package:bang/core/constants.dart';
import 'package:bang/services/audio_service.dart';
import 'package:flutter/material.dart';
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
      onWillPop: () => Future.value(false),
      onConfirm: _exit,
      onCancel: () => Future.value(false),
      cancel: BangButton(
        text: 'Mégse',
        onPressed: Get.back,
        height: 35,
        width: 60,
      ),
      confirm: BangButton(
        text: 'Értem, kilépek!',
        onPressed: _exit,
        height: 40,
        width: 150,
        isNormal: false,
      ),
      content: Text(
        'Biztosan ki akarsz lépni a játékból?\n\nTöbbszöri kilépés szankciókat vonhat maga után!',
        textAlign: TextAlign.center,
      ),
    );
    var returnValue = _exitConfirmed;
    _exitDone();
    return Future.value(returnValue);
  }

  void _exit() {
    _exitConfirmed = true;
    Get.back();
    AudioService.playMenuSong();
  }

  void _exitDone() => _exitConfirmed = false;

  @override
  void onInit() async {
    //initWebsocket();
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

    connection.on(
        'LobbyCreated', (setHealthDto) => log(setHealthDto.toString()));

    connection.invoke('sendMessage', args: ['Üzenet', 'Lobby neve']);
  }
}
