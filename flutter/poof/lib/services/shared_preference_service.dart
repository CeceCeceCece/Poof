import 'dart:developer';

import 'package:bang/core/constants.dart';
import 'package:bang/services/service_base.dart';

import 'package:shared_preferences/shared_preferences.dart';

class SharedPreferenceService extends ServiceBase {
  static late SharedPreferences _preferences;
  static String? _token;

  static String get token {
    if (_token == null || _token == '') {
      _token = _preferences.getString(Constants.TOKEN) ?? '';
    }
    return _token!;
  }

  static set token(String token) {
    _preferences.setString(Constants.TOKEN, token);
    _token = token;
  }

  @override
  Future<void> init() async {
    _preferences = await SharedPreferences.getInstance();

    log('SERVICES: SHARED_PREF INITIALIZED');
  }
}
