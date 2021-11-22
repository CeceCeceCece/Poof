import 'package:bang/routes/routes.dart';
import 'package:bang/services/audio_service.dart';
import 'package:bang/services/auth_service.dart';
import 'package:get/get.dart';

class SplashController extends GetxController {
  double _splashDuration = 1;
  double _animationFrames = 40;
  get frameRate => _animationFrames / _splashDuration;
  bool shouldRepeat = false;

  @override
  void onReady() {
    super.onReady();
    _initState();
  }

  Future<void> _initState() async {
    var route = AuthService.hasValidToken ? Routes.HOME : Routes.LOGIN;
    await _splashDuration.delay();

    Get.offAllNamed(route);
    AudioService.playMenuSong();
  }
}
