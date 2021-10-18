import 'dart:developer';
import 'dart:math' as Math;

import 'package:bang/cards/model/action_cards/equipment_card.dart';
import 'package:bang/cards/model/card_constants.dart' as Bang;
import 'package:bang/services/audio_service.dart';
import 'package:bang/services/game_service.dart';
import 'package:flutter/material.dart';
import 'package:get/get.dart';

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
}
