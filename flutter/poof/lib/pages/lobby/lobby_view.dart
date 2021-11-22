import 'package:bang/cards/widgets/button.dart';
import 'package:bang/core/constants.dart';
import 'package:bang/pages/lobby/lobby_controller.dart';
import 'package:flutter/material.dart';
import 'package:get/get.dart';

class LobbyView extends GetView<LobbyController> {
  @override
  Widget build(BuildContext context) {
    return WillPopScope(
      onWillPop: () async {
        controller.back();
        return true;
      },
      child: Container(
        decoration: BoxDecoration(
            image: DecorationImage(
                image: AssetImage(
                  Constants.backgroundPath,
                ),
                fit: BoxFit.fitHeight)),
        child: Scaffold(
          backgroundColor: Colors.transparent,
          body: Center(
            child: Obx(
              () => Column(
                mainAxisAlignment: MainAxisAlignment.center,
                children: [
                  Spacer(),
                  Text('Szoba kód: ${controller.roomID}'),
                  Text('Játékosok: ${controller.playerList().length}/7'),
                  SizedBox(height: 50),
                  Container(
                    padding: EdgeInsets.only(top: 20),
                    height: 7 * 50,
                    child: ListView(
                      children: controller.playerWidgetList,
                      physics: NeverScrollableScrollPhysics(),
                    ),
                  ),
                  BangButton(
                    text: 'Játék indítása',
                    onPressed: controller.join,
                  ),
                  SizedBox(height: 40),
                  BangButton(
                    onPressed: controller.showQR,
                    text: 'QR kód mutatása',
                  ),
                  SizedBox(height: 20),

                  /*BangButton(
                    text: 'Add players',
                    onPressed: () => controller.addPlayers(['players']),
                  ),*/
                  Expanded(
                    child: Align(
                      alignment: Alignment.bottomRight,
                      child: Padding(
                        padding: const EdgeInsets.all(12.0),
                        child: BangButton(
                          width: 90,
                          height: 50,
                          onPressed: controller.back,
                          text: 'Kilépés',
                        ),
                      ),
                    ),
                  )
                ],
              ),
            ),
          ),
        ),
      ),
    );
  }
}
