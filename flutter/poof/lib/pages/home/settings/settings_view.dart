import 'package:bang/core/colors.dart';
import 'package:bang/pages/home/settings/settings_controller.dart';
import 'package:flutter/material.dart';
import 'package:get/get.dart';

class SettingsView extends GetView<SettingsController> {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
        backgroundColor: BangColors.background, body: Text('Settings Page'));
  }
}
