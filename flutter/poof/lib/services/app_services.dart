import 'package:bang/services/audio_service.dart';
import 'package:get/get.dart';

import 'connectivity_service.dart';

abstract class AppServices {
  static Future<void> init() async {
    Future.delayed(
        Duration(seconds: 1), () => Get.put(ConnectivityService()).init());
  }
}
