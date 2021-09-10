import 'package:flutter/material.dart';
import 'app.dart';
import 'firebase/firebase_testing.dart';

void main() async {
  FirebaseTests.runTests();
  runApp(App());
}
