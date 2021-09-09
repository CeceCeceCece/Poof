import 'package:firebase_analytics/firebase_analytics.dart';
import 'package:firebase_core/firebase_core.dart';
import 'package:firebase_crashlytics/firebase_crashlytics.dart';
import 'package:firebase_database/firebase_database.dart';
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

  DatabaseReference testRef = FirebaseDatabase(
          databaseURL:
              "https://poof-8e0bc-default-rtdb.europe-west1.firebasedatabase.app")
      .reference()
      .child('test');

  final dbResult = await testRef.get();

  print('Database entries:  ${dbResult.value}');

  testRef.set("testvalue").then((value) => print('database entry added'));

  final analytics = FirebaseAnalytics();
  analytics.logEvent(name: "analytics_working");

  _performanceCustomTrace();

  // ! A következő pár sor crashelné az alkalmazást, ezekről instant report érekzik firebase-re
  // await FirebaseCrashlytics.instance
  //    .recordFlutterError(FlutterErrorDetails(exception: "exception"));
  //throw FlutterError("error");
  //FirebaseCrashlytics.instance.crash(); működik
}

void _performanceCustomTrace() async {
  final trace = FirebasePerformance.instance.newTrace('myCustomTrace');
  trace.start();
  await Future.delayed(Duration(seconds: 5));
  trace.stop();
}
