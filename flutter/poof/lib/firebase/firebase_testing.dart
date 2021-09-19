import 'package:firebase_analytics/firebase_analytics.dart';
import 'package:firebase_core/firebase_core.dart';
import 'package:firebase_crashlytics/firebase_crashlytics.dart';
import 'package:firebase_database/firebase_database.dart';
import 'package:firebase_performance/firebase_performance.dart';
import 'package:flutter/material.dart';

class FirebaseTests {
  static void runTests() async {
    await _init();

    _rtdb();

    _analytics();

    _performance();

    // _crashlytics();
  }

  static Future<void> _init() async {
    WidgetsFlutterBinding.ensureInitialized();
    await Firebase.initializeApp();
  }

  static void _performance() async {
    final trace = FirebasePerformance.instance.newTrace('myCustomTrace');
    trace.start();
    await Future.delayed(Duration(seconds: 5));
    trace.stop();
  }

  // ignore: unused_element
  static void _crashlytics() async {
    await FirebaseCrashlytics.instance
        .recordFlutterError(FlutterErrorDetails(exception: "exception"));
    FirebaseCrashlytics.instance.crash();
    throw FlutterError("error");
  }

  static void _analytics() {
    final analytics = FirebaseAnalytics();
    analytics.logEvent(name: "analytics_working");
  }

  static void _rtdb() async {
    DatabaseReference testRef = FirebaseDatabase(
            databaseURL:
                "https://poof-8e0bc-default-rtdb.europe-west1.firebasedatabase.app")
        .reference()
        .child('test');

    final dbResult = await testRef.get();

    print('Database entries:  ${dbResult.value}');

    testRef.set("testvalue").then((value) => print('database entry added'));
  }
}
