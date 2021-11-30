import 'dart:developer';

import 'package:bang/routes/routes.dart';
import 'package:bang/services/audio_service.dart';
import 'package:bang/services/auth_service.dart';
import 'package:bang/services/shared_preference_service.dart';
import 'package:get/get.dart';

class SplashController extends GetxController {
  double _splashDuration = 1;
  double _animationFrames = 40;
  get frameRate => _animationFrames / _splashDuration;
  bool shouldRepeat = false;

  @override
  void onReady() {
    super.onReady();
    _initState();
  }

  Future<void> _initState() async {
    var password = SharedPreferenceService.password;
    var name = SharedPreferenceService.name;
    bool success = false;
    AudioService.playMenuSong();
    if (name != '' && password != null)
      success = await Get.find<AuthService>().login(name, password);
    else
      log('credentials not present');
    if (!success) {
      await _splashDuration.delay();
      log('login not successful');
      Get.offAllNamed(Routes.LOGIN);
    }
  }
}
