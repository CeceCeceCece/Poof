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
  static const ROOM_SELECTION = '/roomselection';
  static const GAME_ROOM = '/gameroom';
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
  ];
}
