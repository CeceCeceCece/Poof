import 'package:bang/routes/routes.dart';
import 'package:get/get.dart';

class HomeController extends GetxController {
  void joinRoom(String roomId) {
    Get.toNamed(Routes.GAME);
  }
}
