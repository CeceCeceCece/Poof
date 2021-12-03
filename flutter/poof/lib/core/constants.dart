abstract class Constants {
  static const MUSIC = 'MUSIC';
  static const SFX = 'SFX';
  static const PASSWORD = 'PASSWORD';
  static const TOKEN = 'TOKEN';
  static const NAME = 'NAME';
  static const LOBBY_HUB = '/hubs/poof';
  static const GAME_HUB = '/hubs/poofgame';
  static const BASE_URL = "https://poofapi.azurewebsites.net";
  static const MAX_USER_NUMBER = 7;

  static void defaultOnChanged(String s) {}
}

abstract class AssetPaths {
  static const backgroundPath = 'assets/background/bg2.jpg';
  static const bangLogo = 'assets/icons/bang_logo.png';
}
