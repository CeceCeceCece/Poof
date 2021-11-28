import 'dart:developer';

import 'package:bang/network/user_provider.dart';
import 'package:bang/routes/routes.dart';
import 'package:bang/services/service_base.dart';
import 'package:fluttertoast/fluttertoast.dart';
import 'package:get/get.dart';

import 'shared_preference_service.dart';

class AuthService extends ServiceBase {
  final UserProvider _userProvider = Get.find();

  String get player => SharedPreferenceService.name;
  @override
  Future<void> init() async {
    log('SERVICES: AUTH_SERVICE INITIALIZED');
  }

  static bool get hasValidToken => SharedPreferenceService.token != '';

  static String? get tryGetToken => SharedPreferenceService.token == ''
      ? null
      : SharedPreferenceService.token;

  void login(String username, String password) async {
    try {
      final body =
          'client_id=poof-flutter&grant_type=password&username=$username&password=$password&scope=openid+api-openid';
      final response = await _userProvider.login(body);
      if (response.statusCode == 200) {
        SharedPreferenceService.token = response.body!.token!;
        Fluttertoast.showToast(msg: 'token acquired');
        SharedPreferenceService.name = username;
        Get.offAndToNamed(Routes.HOME);
      } else {
        Fluttertoast.showToast(msg: '${response.statusCode}');
      }
    } catch (error) {
      log('$error');
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
    }
  }
}
