import 'package:bang/services/auth_service.dart';
import 'package:flutter/material.dart';
import 'package:get/get.dart';

class LoginController extends GetxController {
  var loading = false.obs;
  final usernameC = TextEditingController(text: 'Cece');
  final passwordC = TextEditingController(text: 'admin');
  final confirmPasswordC = TextEditingController(text: 'admin');

  var isLoginPage = true.obs;

  @override
  void onInit() {
    AuthService.enhancedLogin = false;
    super.onInit();
  }

  void login() {
    loading.value = true;
    Get.find<AuthService>().login(usernameC.text, passwordC.text);
  }

  void register() {
    loading.value = true;
    Get.find<AuthService>().register(usernameC.text, passwordC.text);
  }

  void goToRegister() {
    isLoginPage.value = false;
    usernameC.clear();
    passwordC.clear();
    confirmPasswordC.clear();
  }

  void goToLogin() {
    isLoginPage.value = true;

    usernameC.clear();
    passwordC.clear();
    confirmPasswordC.clear();
  }
}
