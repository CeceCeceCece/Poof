import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:get_storage/get_storage.dart';
import 'package:wakelock/wakelock.dart';

import 'app.dart';
import 'services/app_services.dart';

void main() async {
  //FirebaseTests.runTests();
  WidgetsFlutterBinding.ensureInitialized();
  await AppServices.initAudio();
  AppServices.init();
  await GetStorage.init();
  await _setScreenProperties();
  await Future.delayed(Duration(milliseconds: 500));
  runApp(App());
}

Future<void> _setScreenProperties() async {
  Wakelock.enable();

  await SystemChrome.setPreferredOrientations(
      [DeviceOrientation.portraitDown, DeviceOrientation.portraitUp]);
  await SystemChrome.setEnabledSystemUIMode(SystemUiMode.immersiveSticky,
      overlays: []);
}
