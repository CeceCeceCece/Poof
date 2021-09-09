import 'package:firebase_analytics/firebase_analytics.dart';
import 'package:flutter/material.dart';
import 'app.dart';

void main() async {
  WidgetsFlutterBinding.ensureInitialized();
  firebaseTesting();

  runApp(App());
}

void firebaseTesting() async {
  final analytics = FirebaseAnalytics();
  analytics.logEvent(name: "analytics_working");
}
