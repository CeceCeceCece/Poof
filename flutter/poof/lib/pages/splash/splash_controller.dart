import 'package:bang/routes/routes.dart';
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
    await _splashDuration.delay();
    Get.offAllNamed(Routes.LOGIN);
  }
}
