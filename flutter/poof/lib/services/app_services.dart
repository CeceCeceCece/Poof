import 'package:bang/services/audio_service.dart';
import 'package:bang/services/shared_preference_service.dart';
import 'package:get/get.dart';

import 'connectivity_service.dart';

abstract class AppServices {
  static Future<void> initAudio() async => await Get.put(AudioService()).init();
  static Future<void> init() async {
    await Future.delayed(Duration(milliseconds: 500));
    Get.put(ConnectivityService()).init();
    Get.put(SharedPreferenceService()).init();
  }
}
