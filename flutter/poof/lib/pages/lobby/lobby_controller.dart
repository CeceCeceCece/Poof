import 'package:bang/routes/routes.dart';
import 'package:bang/services/audio_service.dart';
import 'package:bang/services/game_service.dart';
import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:fluttertoast/fluttertoast.dart';
import 'package:get/get.dart';
import 'package:qr_flutter/qr_flutter.dart';

class LobbyController extends GetxController {
  String? get roomID => Get.find<GameService>().roomId.value;

  var playerIsLobbyAdmin = true.obs;
  @override
  void onInit() async {
    refreshUI();
    await SystemChrome.setEnabledSystemUIMode(SystemUiMode.manual,
        overlays: []);
    super.onInit();
  }

  void showQR([String? data]) {
    Get.defaultDialog(
      content: Container(
        width: 250,
        height: 250,
        child: QrImage(
          data: data ?? (roomID ?? 'RANDOM'),
          version: QrVersions.auto,
          errorCorrectionLevel: QrErrorCorrectLevel.Q,
          size: 100.0,
          embeddedImage: AssetImage(
            'assets/icons/bang_logo.png',
          ),
          embeddedImageStyle: QrEmbeddedImageStyle(size: Size(30, 30)),
        ),
      ),
    );
  }

  var playerList = [
    'asfsagags',
    'asgagagdfhh',
    'Ferike',
    'safffff',
    'ddddddd',
    'EGYNAGYONHOSSZÚNEVŰFELHASZNÁLÓXDDDDDDDDDD',
    'HGKJGKUJZGK'
  ].obs;
  List<Widget> playerWidgetList = [];

  void join() {
    Get.offAndToNamed(Routes.GAME);
  }

  void addPlayers(List<String> players) {
    playerList.addAll(players);
    refreshUI();
  }

  void removePlayer(String id) {
    if (!isAdmin(id)) playerList.removeWhere((element) => element == id);
    refreshUI();
  }

  bool isAdmin(String player) => player == 'B1GD1CK';

  void toggleAdmin(String playerID) {
    Fluttertoast.showToast(
        msg: '$playerID is now admin', gravity: ToastGravity.BOTTOM);
    refreshUI();
  }

  void refreshUI() {
    var widgetList = <Widget>[];

    for (int i = 0; i < playerList.length; i++)
      widgetList.add(playerIsLobbyAdmin()
          ? Dismissible(
              confirmDismiss: (DismissDirection details) async =>
                  Future.delayed(Duration(seconds: 1), () {
                return !isAdmin(playerList[i]);
              }),
              onDismissed: (_) => removePlayer(playerList[i]),
              key: Key(playerList[i]),
              background: Container(
                alignment: Alignment.centerLeft,
                color: Colors.transparent,
                child: Row(
                  children: [
                    SizedBox(
                      width: 30,
                    ),
                    Icon(Icons.highlight_remove_sharp, color: Colors.white),
                  ],
                ),
              ),
              secondaryBackground: Container(
                alignment: Alignment.centerRight,
                color: Colors.transparent,
                child: Row(
                  mainAxisAlignment: MainAxisAlignment.end,
                  children: [
                    Icon(Icons.highlight_remove_sharp, color: Colors.white),
                    SizedBox(
                      width: 30,
                    ),
                  ],
                ),
              ),
              child: Padding(
                padding: EdgeInsets.fromLTRB(30, 3, 30, 3),
                child: Container(
                  height: 35,
                  decoration: BoxDecoration(
                      borderRadius: BorderRadius.circular(10.0),
                      color: Colors.white),
                  child: Row(
                      mainAxisAlignment: MainAxisAlignment.center,
                      crossAxisAlignment: CrossAxisAlignment.center,
                      children: [
                        SizedBox(
                          width: 35,
                        ),
                        Expanded(
                          child: Container(
                            child: Text(
                              playerList[i],
                              overflow: TextOverflow.ellipsis,
                              maxLines: 1,
                              //softWrap: false,
                            ),
                          ),
                        ),
                        IconButton(
                          icon: Icon(
                            Icons.star,
                            color: isAdmin(playerList[i])
                                ? Colors.amber
                                : Colors.grey,
                          ),
                          onPressed: () => toggleAdmin(playerList[i]),
                        ),
                        SizedBox(
                          width: 20,
                        )
                      ]),
                ),
              ),
            )
          : Padding(
              padding: EdgeInsets.fromLTRB(30, 3, 30, 3),
              child: Flexible(
                child: Container(
                  height: 35,
                  decoration: BoxDecoration(
                      borderRadius: BorderRadius.circular(10.0),
                      color: Colors.white),
                  child: Row(
                      mainAxisAlignment: MainAxisAlignment.center,
                      crossAxisAlignment: CrossAxisAlignment.center,
                      children: [
                        SizedBox(
                          width: 35,
                        ),
                        Expanded(
                          child: Text(
                            playerList[i],
                            overflow: TextOverflow.ellipsis,
                            maxLines: 1,
                            //softWrap: false,
                          ),
                        ),
                        isAdmin(playerList[i])
                            ? IconButton(
                                icon: Icon(Icons.star, color: Colors.amber),
                                onPressed: null,
                              )
                            : Container(),
                        SizedBox(
                          width: 20,
                        )
                      ]),
                ),
              ),
            ));

    playerWidgetList = widgetList.obs;
  }

  void back() {
    Get.back();
    AudioService.playMenuSong();
  }
}
