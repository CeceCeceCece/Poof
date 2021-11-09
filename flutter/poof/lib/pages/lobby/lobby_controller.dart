import 'package:bang/routes/routes.dart';
import 'package:bang/services/game_service.dart';
import 'package:get/get.dart';

class LobbyController extends GetxController {
  String? get roomID => Get.find<GameService>().roomId.value!;

  void join() {
    Get.offAndToNamed(Routes.GAME);
  }
}
