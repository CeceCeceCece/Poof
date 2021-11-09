import 'package:bang/routes/routes.dart';
import 'package:bang/services/game_service.dart';
import 'package:fluttertoast/fluttertoast.dart';
import 'package:get/get.dart';

class LobbyController extends GetxController {
  String? get roomID => Get.find<GameService>().roomId.value!;

  var playerIsLobbyAdmin = true.obs;

  var playerList = ['Kisfaszu123', 'B1GD1CK', 'Ferike', 'Puhap0cs'].obs;

  void join() {
    Get.offAndToNamed(Routes.GAME);
  }

  void addPlayers(List<String> players) => playerList.addAll(players);

  void removePlayer(String id) {
    if (!isAdmin(id)) playerList.removeWhere((element) => element == id);
  }

  bool isAdmin(String player) => player == 'B1GD1CK';

  void toggleAdmin(String playerID) => Fluttertoast.showToast(
      msg: '$playerID is now admin', gravity: ToastGravity.BOTTOM);
}
