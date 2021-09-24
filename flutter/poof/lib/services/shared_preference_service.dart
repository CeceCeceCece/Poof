import 'package:bang/core/constants.dart';
import 'package:bang/services/service_base.dart';
import 'package:get/get.dart';
import 'package:shared_preferences/shared_preferences.dart';

class SharedPreferenceService extends GetxService implements ServiceBase {
  static late SharedPreferences _preferences;

  static String get token => _preferences.getString(Constants.TOKEN) ?? '';
  static set token(String token) =>
      _preferences.setString(Constants.TOKEN, token);

  @override
  Future<void> init() async {
    _preferences = await SharedPreferences.getInstance();
  }
}
