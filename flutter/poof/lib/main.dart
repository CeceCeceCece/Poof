import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:get_storage/get_storage.dart';
import 'package:wakelock/wakelock.dart';
import 'app.dart';
import 'core/colors.dart';
import 'services/app_services.dart';

void main() async {
  //FirebaseTests.runTests();
  AppServices.init();
  await GetStorage.init();
  _setScreenProperties();
  runApp(App());
}

Future<void> _setScreenProperties() async {
  Wakelock.enable();

  SystemChrome.setSystemUIOverlayStyle(SystemUiOverlayStyle(
      systemStatusBarContrastEnforced: false,
      statusBarColor: BangColors.background));
  await SystemChrome.setEnabledSystemUIMode(SystemUiMode.manual,
      overlays: [SystemUiOverlay.top]);

  await SystemChrome.setPreferredOrientations(
      [DeviceOrientation.portraitDown, DeviceOrientation.portraitUp]);
}
