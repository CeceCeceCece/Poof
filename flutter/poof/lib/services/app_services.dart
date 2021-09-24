import 'package:bang/services/audio_service.dart';
import 'package:bang/services/service_base.dart';
import 'package:bang/services/shared_preference_service.dart';
import 'package:get/get.dart';

import 'connectivity_service.dart';

abstract class AppServices {
  static final List<ServiceBase> _services = [
    SharedPreferenceService(),
    ConnectivityService()
  ];

  static Future<void> initAudio() async => await Get.put(AudioService()).init();
  static Future<void> init() async => Future.delayed(
      Duration(milliseconds: 500), () => _services.forEach(_initService));

  static _initService(ServiceBase service) => Get.put(service).init();
}
