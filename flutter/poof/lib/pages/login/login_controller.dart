import 'package:bang/routes/routes.dart';
import 'package:bang/services/audio_service.dart';
import 'package:flutter/material.dart';
import 'package:get/get.dart';

class LoginController extends GetxController {
  final usernameC = TextEditingController();
  final passwordC = TextEditingController();

  void login() {
    Get.offAndToNamed(Routes.HOME);
  }

  @override
  void onInit() {
    AudioService.playMenuSong();

    super.onInit();
  }
}
