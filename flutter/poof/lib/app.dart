import 'dart:developer';

import 'package:bang/core/constants.dart';
import 'package:bang/core/lang/app_translations.dart';
import 'package:bang/services/audio_service.dart';
import 'package:flutter/material.dart';
import 'package:get/get.dart';

import 'core/app_theme.dart';
import 'core/lang/strings.dart';
import 'routes/routes.dart';

class App extends StatefulWidget {
  @override
  _AppState createState() => _AppState();
}

class _AppState extends State<App> with WidgetsBindingObserver {
  @override
  Widget build(BuildContext context) {
    return GetMaterialApp(
      debugShowCheckedModeBanner: false,
      title: AppStrings.app_name.tr,
      getPages: Pages.routes,
      initialRoute: Pages.initial,
      locale: AppTranslations.locale,
      fallbackLocale: AppTranslations.fallbackLocale,
      translations: AppTranslations(),
      theme: AppTheme.basic,
      home: Scaffold(),
    );
  }

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
  void didChangeDependencies() {
    precacheImage(
        AssetImage(
          AssetPaths.backgroundPath,
        ),
        context);
    super.didChangeDependencies();
  }

  @override
  void didChangeAppLifecycleState(AppLifecycleState state) {
    log('APP LIFECYCLE STATE CHANGED, NEW STATE: $state');
    AudioService.handleLifecycleChange(state);
  }
}
