import 'package:bang/services/audio_service.dart';
import 'package:get/get.dart';

class LoginController extends GetxController {
  @override
  void onInit() {
    AudioService.playMenuSong();
    //AudioService.playBackgroundMusic();
    super.onInit();
  }
}
