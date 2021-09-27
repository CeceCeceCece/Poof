import 'dart:developer';

import 'package:bang/core/colors.dart';
import 'package:bang/core/strings.dart';
import 'package:bang/services/audio_service.dart';
import 'package:flutter/material.dart';
import 'package:get/get.dart';
import 'routes/routes.dart';

class App extends StatefulWidget {
  @override
  _AppState createState() => _AppState();
}

class _AppState extends State<App> with WidgetsBindingObserver {
  @override
  void initState() {
    super.initState();
    WidgetsBinding.instance!.addObserver(this);
  }

  @override
  void dispose() {
    WidgetsBinding.instance!.removeObserver(this);
    super.dispose();
  }

  @override
  void didChangeAppLifecycleState(AppLifecycleState state) {
    log('APP LIFECYCLE STATE CHANGED, NEW STATE: $state');
    AudioService.handleLifecycleChange(state);
  }

  @override
  Widget build(BuildContext context) {
    return GetMaterialApp(
      debugShowCheckedModeBanner: false,
      title: BangStrings.appname,
      getPages: Pages.routes,
      initialRoute: Pages.initial,
      home: Scaffold(
        backgroundColor: BangColors.background,
      ),
    );
  }
}
