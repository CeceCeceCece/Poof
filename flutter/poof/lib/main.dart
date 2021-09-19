import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'app.dart';
import 'core/colors.dart';
import 'firebase/firebase_testing.dart';

void main() async {
  //FirebaseTests.runTests();
  _setScreenProperties();
  runApp(App());
}

Future<void> _setScreenProperties() async {
  SystemChrome.setSystemUIOverlayStyle(SystemUiOverlayStyle(
      systemStatusBarContrastEnforced: false,
      statusBarColor: BangColors.background));
  await SystemChrome.setEnabledSystemUIMode(SystemUiMode.manual,
      overlays: [SystemUiOverlay.top]);

  await SystemChrome.setPreferredOrientations(
      [DeviceOrientation.portraitDown, DeviceOrientation.portraitUp]);
}
