import 'package:bang/network/user_provider.dart';
import 'package:bang/services/audio_service.dart';
import 'package:bang/services/auth_service.dart';
import 'package:bang/services/lobby_service.dart';
import 'package:bang/services/shared_preference_service.dart';
import 'package:get/get.dart';

import 'connectivity_service.dart';

abstract class AppServices {
  static Future<void> initAudio() async => await Get.put(AudioService()).init();
  static Future<void> init() async {
    Get.put(UserProvider());
    await Future.delayed(Duration(milliseconds: 100));
    Get.put(ConnectivityService()).init();
    Get.put(SharedPreferenceService()).init();
    Get.put(LobbyService());
    Get.put(AuthService()).init();
  }
}
