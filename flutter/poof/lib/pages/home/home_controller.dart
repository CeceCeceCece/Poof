import 'package:bang/routes/routes.dart';
import 'package:bang/services/audio_service.dart';
import 'package:bang/services/game_service.dart';
import 'package:bang/services/shared_preference_service.dart';
import 'package:get/get.dart';

class HomeController extends GetxController {
  void joinRoom(String roomId) {
    Get.toNamed(Routes.GAME);
    AudioService.playBackgroundMusic();
    Get.put(GameService());
  }

  void logout() {
    SharedPreferenceService.token = '';
    Get.offAndToNamed(Routes.LOGIN);
  }
}
