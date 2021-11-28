import 'package:get/get_utils/src/extensions/string_extensions.dart';

abstract class Constants {
  static const MUSIC = 'MUSIC';
  static const SFX = 'SFX';
  static const NOTIFICATIONS = 'NOTIFICATIONS';
  static const TOKEN = 'TOKEN';
  static const NAME = 'NAME';
  static const LOBBY_HUB = '/hubs/poof';
  static const BASE_URL = "https://poofapi.azurewebsites.net";
  static const backgroundPath = 'assets/background/bg2.jpg';

  static void defaultOnChanged(String s) {}
  static String? defaultValidator(String? value) {
    if (value == null || value.isEmpty) {
      return 'Ez a mező nem lehet üres!';
    }
    if (value.removeAllWhitespace != value) return 'Space-kkel nem versz át :P';
  }
}
