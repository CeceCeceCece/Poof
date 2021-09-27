import 'package:bang/pages/game/game_controller.dart';
import 'package:bang/pages/game/game_view.dart';
import 'package:bang/pages/home/home_controller.dart';
import 'package:bang/pages/home/home_view.dart';
import 'package:bang/pages/login/login_controller.dart';
import 'package:bang/pages/login/login_view.dart';
import 'package:bang/pages/splash/splash_controller.dart';
import 'package:bang/pages/splash/splash_view.dart';
import 'package:get/get.dart';

abstract class Routes {
  static const LOGIN = '/login';
  static const SPLASH = '/splash';
  static const SIGNUP = '/signup';
  static const SETTINGS = '/settings';
  static const HOME = '/home';
  static const GAME = '/game';
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
  ];
}
