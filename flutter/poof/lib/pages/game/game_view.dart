import 'package:bang/cards/model/action_cards/action_card.dart';
import 'package:bang/cards/model/action_cards/equipment_card.dart';
import 'package:bang/cards/model/card_constants.dart' as Bang;
import 'package:bang/cards/model/non_playable_cards/character_card.dart';
import 'package:bang/cards/model/non_playable_cards/role_card.dart';
import 'package:bang/cards/widgets/bang_card_widget.dart';
import 'package:bang/cards/widgets/non_playable_card_widget.dart';
import 'package:bang/core/constants.dart';
import 'package:bang/pages/game/game_controller.dart';
import 'package:bang/pages/game/widgets/player_view.dart';
import 'package:bang/services/game_service.dart';
import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:fluttertoast/fluttertoast.dart';
import 'package:get/get.dart';

class GameView extends GetView<GameController> {
  @override
  Widget build(BuildContext context) {
    var height = MediaQuery.of(context).size.height;
    var width = MediaQuery.of(context).size.width;
    return Container(
      decoration: BoxDecoration(
          image: DecorationImage(
              image: AssetImage(
                Constants.backgroundPath,
              ),
              fit: BoxFit.fitHeight)),
      child: WillPopScope(
        onWillPop: controller.showBackPopupForResult,
        child: Scaffold(
          backgroundColor: Colors.transparent,
          body: Center(
            child: Obx(
              () => Stack(
                alignment: Alignment.center,
                fit: StackFit.passthrough,
                children: [
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
                  ..._buildLayout(height, width),
                ],
              ),
            ),
          ),
        ),
      ),
    );
  }

  Widget _ph([Color? color]) => Container(
      width: 100,
      height: 100,
      child: Column(
        children: [
          Row(children: [
            Stack(
              alignment: Alignment.bottomLeft,
              children: [
                NonPlayableCardWidget(
                  scale: 0.5,
                  highlightMultiplier: 1.75,
                  card: CharacterCard(
                      background: 'willythekid',
                      health: 4,
                      name: 'willythekid'),
                  onTapCallback: () {},
                ),
                Text('❤: 4', style: TextStyle(fontWeight: FontWeight.bold)),
              ],
            ),
            Stack(
              alignment: Alignment.center,
              children: [
                BangCardWidget(
                  scale: 0.5,
                  showBackPermanently: true,
                  card: ActionCard(
                    background: 'stagecoach',
                    name: 'stagecoach',
                    suit: Bang.Suit.Clubs,
                    value: Bang.Value.Nine,
                    type: Bang.CardType.Action,
                    range: 0,
                  ),
                  handCallback: () {},
                  onDragSuccessCallback: () {},
                ),
                Container(
                  height: 30,
                  width: 30,
                  decoration: BoxDecoration(
                      border: Border.all(color: Colors.white, width: 1.5),
                      shape: BoxShape.circle),
                  child: Opacity(
                    opacity: 0.6,
                    child: Material(
                      shape: CircleBorder(),
                    ),
                  ),
                ),
                Text(
                  '5',
                  style: TextStyle(
                      color: Colors.white,
                      fontWeight: FontWeight.bold,
                      fontSize: 20),
                )
              ],
            ),
          ]),
        ],
      ));

  List<Widget> _buildLayout(double height, double width) {
    switch (controller.playerNumber()) {
      case 4:
        return [
          Positioned(child: _ph(), top: 20, left: width / 2 - 50),
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
          Positioned(child: _ph(), top: 40, left: width * 0.75 + 50),
          Positioned(child: _ph(), top: 40, left: width * 0.25 - 50),
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
          Positioned(child: _ph(), top: height * 0.1, left: width / 2 - 50),
          Positioned(child: _ph(), top: height * 0.25, left: width * 0.9 - 50),
          Positioned(child: _ph(), top: height * 0.25, left: width * 0.10 + 50),
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
