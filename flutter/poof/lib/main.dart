import 'package:firebase_analytics/firebase_analytics.dart';
import 'package:firebase_core/firebase_core.dart';
import 'package:firebase_crashlytics/firebase_crashlytics.dart';
import 'package:firebase_performance/firebase_performance.dart';
import 'package:flutter/material.dart';
import 'app.dart';

void main() async {
  _firebaseTesting();

  runApp(App());
}

void _firebaseTesting() async {
  WidgetsFlutterBinding.ensureInitialized();
  await Firebase.initializeApp();

  final analytics = FirebaseAnalytics();
  analytics.logEvent(name: "analytics_working");

  _performanceCustomTrace();

  await FirebaseCrashlytics.instance
      .recordFlutterError(FlutterErrorDetails(exception: "exception"));

  // ! A következő két sor crashelné az alkalmazást, ezekről instant report érekzik firebase-re
  //throw FlutterError("error");
  //FirebaseCrashlytics.instance.crash(); működik
}

void _performanceCustomTrace() async {
  final trace = FirebasePerformance.instance.newTrace('myCustomTrace');
  trace.start();
  await Future.delayed(Duration(seconds: 5));
  trace.stop();
}
