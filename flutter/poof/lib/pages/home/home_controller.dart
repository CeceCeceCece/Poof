import 'package:bang/pages/login/login_view.dart';
import 'package:bang/routes/routes.dart';
import 'package:bang/services/audio_service.dart';
import 'package:bang/services/shared_preference_service.dart';
import 'package:get/get.dart';

class HomeController extends GetxController {
  void joinRoom(String roomId) {
    Get.toNamed(Routes.GAME);
    AudioService.playBackgroundMusic();
  }

  void logout() {
    SharedPreferenceService.token = '';
    Get.offAndToNamed(Routes.LOGIN);
  }
}
