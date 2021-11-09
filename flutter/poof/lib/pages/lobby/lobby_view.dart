import 'package:bang/cards/widgets/button.dart';
import 'package:bang/core/constants.dart';
import 'package:bang/pages/lobby/lobby_controller.dart';
import 'package:flutter/material.dart';
import 'package:get/get.dart';

class LobbyView extends GetView<LobbyController> {
  @override
  Widget build(BuildContext context) {
    return Container(
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
                Text('LOBBY: ${controller.roomID}'),
                Text('PLAYERS: ${controller.playerList().length}/7'),
                SizedBox(height: 50),
                BangButton(
                  text: 'Enter game',
                  onPressed: controller.join,
                ),
                Container(
                  padding: EdgeInsets.only(top: 20),
                  height: 7 * 50,
                  child: ListView(
                    children: controller.playerWidgetList,
                    physics: NeverScrollableScrollPhysics(),
                  ),
                ),
                SizedBox(height: 20),
                BangButton(
                  onPressed: controller.showQR,
                  text: 'Show QR code!',
                ),
                SizedBox(height: 20),
                BangButton(
                  text: 'Add players',
                  onPressed: () => controller.addPlayers(['players']),
                ),
                Expanded(
                  child: Align(
                    alignment: Alignment.bottomRight,
                    child: Padding(
                      padding: const EdgeInsets.all(12.0),
                      child: BangButton(
                        width: 70,
                        height: 40,
                        onPressed: Get.back,
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
    );
  }
}
