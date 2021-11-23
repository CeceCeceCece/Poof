import 'package:bang/cards/widgets/button.dart';
import 'package:bang/services/audio_service.dart';
import 'package:flutter/material.dart';
import 'package:get/get.dart';

class GameController extends GetxController {
  var _exitConfirmed = false;
  var playerNumber = 7.obs;

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
}
