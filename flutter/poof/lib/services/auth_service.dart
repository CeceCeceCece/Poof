import 'dart:developer';

import 'package:bang/core/lang/strings.dart';
import 'package:bang/network/user_provider.dart';
import 'package:bang/pages/login/login_controller.dart';
import 'package:bang/routes/routes.dart';
import 'package:bang/services/service_base.dart';
import 'package:fluttertoast/fluttertoast.dart';
import 'package:get/get.dart';

import 'shared_preference_service.dart';

class AuthService extends ServiceBase {
  final UserProvider _userProvider = Get.find();

  static bool enhancedLogin = true;

  String get player => SharedPreferenceService.name;
  @override
  Future<void> init() async {
    log('SERVICES: AUTH_SERVICE INITIALIZED');
  }

  static bool get hasValidToken => SharedPreferenceService.token != '';

  static String? get tryGetToken => SharedPreferenceService.token == ''
      ? null
      : SharedPreferenceService.token;

  Future<bool> login(String username, String password) async {
    bool success = false;
    try {
      final body =
          'client_id=poof-flutter&grant_type=password&username=$username&password=$password&scope=openid+api-openid';
      final response = await _userProvider.login(body);
      if (response.statusCode == 200) {
        SharedPreferenceService.token = response.body!.token!;

        Get.offAndToNamed(Routes.HOME);

        SharedPreferenceService.saveCredentials(username, password);
        success = true;
      } else {
        Fluttertoast.showToast(msg: AppStrings.error_while_login.tr);
        success = false;
      }
    } catch (error) {
      success = false;
      log('$error');
    } finally {
      if (!enhancedLogin) Get.find<LoginController>().loading.value = false;
      return success;
    }
  }

  void register(String username, String password) async {
    try {
      final response = await _userProvider.register(username, password);
      if (response.statusCode == 200) {
        login(username, password);
      }
    } catch (error) {
      log('$error');
    } finally {
      Get.find<LoginController>().loading.value = false;
    }
  }
}
