import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'app.dart';
import 'core/colors.dart';
import 'firebase/firebase_testing.dart';

void main() async {
  FirebaseTests.runTests();
  _goFullscreen();
  runApp(App());
}

Future<void> _goFullscreen() async {
  SystemChrome.setSystemUIOverlayStyle(SystemUiOverlayStyle(
      systemStatusBarContrastEnforced: false,
      statusBarColor: BangColors.background));
  await SystemChrome.setEnabledSystemUIMode(SystemUiMode.manual,
      overlays: [SystemUiOverlay.top]);
}
