import 'package:bang/pages/game/game_controller.dart';
import 'package:bang/pages/game/game_view.dart';
import 'package:bang/pages/home/home_controller.dart';
import 'package:bang/pages/home/home_view.dart';
import 'package:bang/pages/home/settings/settings_controller.dart';
import 'package:bang/pages/home/settings/settings_view.dart';
import 'package:bang/pages/login/login_controller.dart';
import 'package:bang/pages/login/login_view.dart';
import 'package:bang/pages/profile/profile_controller.dart';
import 'package:bang/pages/profile/profile_view.dart';
import 'package:bang/pages/splash/splash_controller.dart';
import 'package:bang/pages/splash/splash_view.dart';
import 'package:get/get.dart';

abstract class Routes {
  static const LOGIN = '/login';
  static const SPLASH = '/splash';
  static const SETTINGS = '/settings';
  static const HOME = '/home';
  static const GAME = '/game';
  static const PROFILE = '/profile';
}

abstract class Pages {
  static String initial = Routes.SPLASH;

  static final routes = [
    GetPage(
        name: Routes.SPLASH,
        page: () => SplashView(),
        binding: BindingsBuilder.put(() => SplashController())),
    GetPage(
        name: Routes.LOGIN,
        page: () => LoginView(),
        binding: BindingsBuilder.put(() => LoginController())),
    GetPage(
        name: Routes.HOME,
        page: () => HomeView(),
        binding: BindingsBuilder.put(() => HomeController())),
    GetPage(
        name: Routes.GAME,
        page: () => GameView(),
        binding: BindingsBuilder.put(() => GameController())),
    GetPage(
        name: Routes.PROFILE,
        page: () => ProfileView(),
        binding: BindingsBuilder.put(() => ProfileController())),
    GetPage(
        name: Routes.SETTINGS,
        page: () => SettingsView(),
        binding: BindingsBuilder.put(() => SettingsController())),
  ];
}
