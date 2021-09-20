import 'package:bang/core/colors.dart';
import 'package:bang/core/strings.dart';
import 'package:flutter/material.dart';
import 'package:get/get.dart';
import 'routes/routes.dart';

class App extends StatelessWidget {
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
