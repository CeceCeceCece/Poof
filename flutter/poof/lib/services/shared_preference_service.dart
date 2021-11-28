import 'dart:developer';

import 'package:bang/core/constants.dart';
import 'package:bang/services/audio_service.dart';
import 'package:bang/services/service_base.dart';
import 'package:shared_preferences/shared_preferences.dart';

class SharedPreferenceService extends ServiceBase {
  static late SharedPreferences _preferences;
  static String? _token;
  static String? _name;

  static String get token {
    if (_token == null || _token == '') {
      _token = _preferences.getString(Constants.TOKEN) ?? '';
    }
    return _token!;
  }

  static String get name {
    if (_name == null || _name == '') {
      _name = _preferences.getString(Constants.NAME) ?? '';
    }
    return _name!;
  }

  static set name(String name) {
    _preferences.setString(Constants.NAME, name);
    _name = name;
  }

  static set token(String token) {
    _preferences.setString(Constants.TOKEN, token);
    _token = token;
  }

  static bool get music {
    return _preferences.getBool(Constants.MUSIC) ?? true;
  }

  static bool get sfx {
    return _preferences.getBool(Constants.SFX) ?? true;
  }

  static bool get noti {
    return _preferences.getBool(Constants.NOTIFICATIONS) ?? true;
  }

  static set music(bool music) {
    _preferences.setBool(Constants.MUSIC, music);
    music ? AudioService.playMenuSong() : AudioService.stopAll();
  }

  static set sfx(bool sfx) => _preferences.setBool(Constants.SFX, sfx);
  static set noti(bool noti) =>
      _preferences.setBool(Constants.NOTIFICATIONS, noti);

  @override
  Future<void> init() async {
    _preferences = await SharedPreferences.getInstance();

    log('SERVICES: SHARED_PREF INITIALIZED');
  }
}
