import 'package:bang/cards/model/action_cards/equipment_card.dart';
import 'package:bang/cards/model/card_constants.dart' as Bang;
import 'package:bang/cards/model/non_playable_cards/character_card.dart';
import 'package:bang/cards/model/non_playable_cards/role_card.dart';
import 'package:bang/cards/widgets/bang_card_widget.dart';
import 'package:bang/core/colors.dart';
import 'package:bang/pages/game/game_controller.dart';
import 'package:bang/pages/game/widgets/player_view.dart';
import 'package:bang/services/game_service.dart';
import 'package:flutter/material.dart';
import 'package:fluttertoast/fluttertoast.dart';
import 'package:get/get.dart';

class GameView extends GetView<GameController> {
  @override
  Widget build(BuildContext context) {
    var height = MediaQuery.of(context).size.height;
    var width = MediaQuery.of(context).size.width;
    return WillPopScope(
      onWillPop: controller.showBackPopupForResult,
      child: Scaffold(
        backgroundColor: BangColors.background,
        body: Center(
          child: Obx(
            () => Stack(
              alignment: Alignment.center,
              children: [
                ..._buildLayout(height, width),
                Center(
                    child: Row(
                  mainAxisAlignment: MainAxisAlignment.center,
                  children: [
                    DragTarget<String>(
                        onAccept: (data) => Fluttertoast.showToast(msg: data),
                        builder: (BuildContext context, List incoming,
                            List rejected) {
                          return Container(
                            color: Colors.green,
                            width: 80,
                            height: 150,
                          );
                        }),
                    SizedBox(width: 20),
                    Draggable<String>(
                      data: 'siker',
                      feedback: BangCardWidget(
                          card: EquipmentCard(
                              background: 'barrel',
                              name: 'barrel',
                              value: Bang.Value.Ten,
                              type: Bang.CardType.Equipment,
                              suit: Bang.Suit.Diamonds),
                          showBackPermanently: true,
                          scale: 0.8,
                          onDragSuccessCallback: () {},
                          handCallback: () {}),
                      child: BangCardWidget(
                          card: EquipmentCard(
                              background: 'barrel',
                              name: 'barrel',
                              value: Bang.Value.Ten,
                              type: Bang.CardType.Equipment,
                              suit: Bang.Suit.Diamonds),
                          showBackPermanently: true,
                          scale: 0.8,
                          onDragSuccessCallback: () {},
                          handCallback: () {}),
                    ),
                  ],
                )),
                Column(
                  mainAxisAlignment: MainAxisAlignment.center,
                  children: [
                    ElevatedButton(
                        onPressed: () {
                          controller.randomizePlayerNumber();
                          Get.find<GameService>().setCards();
                        },
                        child: Icon(Icons.replay_outlined)),
                  ],
                ),
              ],
            ),
          ),
        ),
      ),
    );
  }

  Widget _ph([Color? color]) => Container(
        height: 20,
        width: 20,
        decoration: BoxDecoration(color: color ?? Colors.red),
      );

  List<Widget> _buildLayout(double height, double width) {
    switch (controller.playerNumber()) {
      case 4:
        return [
          Positioned(child: _ph(), top: 20, left: width / 2 - 10),
          Positioned(child: _ph(), top: height * 0.4 - 10, left: 10),
          Positioned(child: _ph(), top: height * 0.4 - 10, right: 10),
          Align(
              alignment: Alignment.bottomCenter,
              child: PlayerView(
                characterCard: CharacterCard(
                    background: 'willythekid', health: 4, name: 'willythekid'),
                roleCard: RoleCard(name: 'sheriff', background: 'sheriff'),
              )),
        ];
      case 5:
        return [
          Positioned(child: _ph(), top: 40, left: width * 0.75 - 10),
          Positioned(child: _ph(), top: 40, left: width * 0.25 - 10),
          Positioned(child: _ph(), top: height * 0.45 - 10, left: 10),
          Positioned(child: _ph(), top: height * 0.45 - 10, right: 10),
          Align(
              alignment: Alignment.bottomCenter,
              child: PlayerView(
                characterCard: CharacterCard(
                    background: 'willythekid', health: 4, name: 'willythekid'),
                roleCard: RoleCard(name: 'sheriff', background: 'sheriff'),
              )),
        ];
      case 6:
        return [
          Positioned(child: _ph(), top: height * 0.1, left: width / 2 - 10),
          Positioned(child: _ph(), top: height * 0.25, left: width * 0.9 - 10),
          Positioned(child: _ph(), top: height * 0.25, left: width * 0.10 - 10),
          Positioned(child: _ph(), top: height * 0.5 - 10, left: 10),
          Positioned(child: _ph(), top: height * 0.5 - 10, right: 10),
          Align(
              alignment: Alignment.bottomCenter,
              child: PlayerView(
                characterCard: CharacterCard(
                    background: 'willythekid', health: 4, name: 'willythekid'),
                roleCard: RoleCard(name: 'sheriff', background: 'sheriff'),
              )),
        ];
      case 7:
        return [
          Positioned(child: _ph(), top: height * 0.1, left: width * 0.3 - 10),
          Positioned(child: _ph(), top: height * 0.1, left: width * 0.7 - 10),
          Positioned(child: _ph(), top: height * 0.3, left: width * 0.95 - 10),
          Positioned(child: _ph(), top: height * 0.3, left: width * 0.05 - 10),
          Positioned(child: _ph(), top: height * 0.55 - 10, left: 10),
          Positioned(child: _ph(), top: height * 0.55 - 10, right: 10),
          Align(
              alignment: Alignment.bottomCenter,
              child: PlayerView(
                characterCard: CharacterCard(
                    background: 'willythekid', health: 4, name: 'willythekid'),
                roleCard: RoleCard(name: 'sheriff', background: 'sheriff'),
              )),
        ];
      default:
        return [Text('default')];
    }
  }
}
