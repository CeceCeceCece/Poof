import 'dart:developer';

import 'package:bang/core/app_constants.dart';
import 'package:bang/services/audio_service.dart';
import 'package:bang/services/service_base.dart';
import 'package:shared_preferences/shared_preferences.dart';

class SharedPreferenceService extends ServiceBase {
  static late SharedPreferences _preferences;
  static String? _token;
  static String? _name;

  static String get token {
    if (_token == null || _token == '') {
      _token = _preferences.getString(AppConstants.TOKEN) ?? '';
    }
    return _token!;
  }

  static String get name {
    if (_name == null || _name == '') {
      _name = _preferences.getString(AppConstants.NAME) ?? '';
    }
    return _name!;
  }

  static void removeCredentials() {
    _preferences.clear();
    token = '';
    name = '';
  }

  static void saveCredentials(String username, String pw) {
    name = username;
    _preferences.setString(AppConstants.PASSWORD, pw);
  }

  static String? get password => _preferences.getString(AppConstants.PASSWORD);

  static set name(String name) {
    _preferences.setString(AppConstants.NAME, name);
    _name = name;
  }

  static set token(String token) {
    _preferences.setString(AppConstants.TOKEN, token);
    _token = token;
  }

  static bool get music {
    return _preferences.getBool(AppConstants.MUSIC) ?? true;
  }

  static bool get sfx {
    return _preferences.getBool(AppConstants.SFX) ?? true;
  }

  static set music(bool music) {
    _preferences.setBool(AppConstants.MUSIC, music);
    music ? AudioService.playMenuSong() : AudioService.stopAll();
  }

  static set sfx(bool sfx) => _preferences.setBool(AppConstants.SFX, sfx);
  @override
  Future<void> init() async {
    _preferences = await SharedPreferences.getInstance();

    log('SERVICES: SHARED_PREF INITIALIZED');
  }
}
