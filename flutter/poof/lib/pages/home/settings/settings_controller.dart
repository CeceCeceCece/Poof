import 'package:bang/services/shared_preference_service.dart';
import 'package:get/get.dart';

class SettingsController extends GetxController {
  @override
  void onInit() {
    isMusicPlayingEnabled(SharedPreferenceService.music);
    isSFXEnabled(SharedPreferenceService.sfx);
    super.onInit();
  }

  var isMusicPlayingEnabled = true.obs;
  var isSFXEnabled = true.obs;

  void changeMusicSettings(bool val) {
    isMusicPlayingEnabled(val);
    SharedPreferenceService.music = val;
  }

  void changeSFXSettings(bool val) {
    isSFXEnabled(val);
    SharedPreferenceService.sfx = val;
  }
}
