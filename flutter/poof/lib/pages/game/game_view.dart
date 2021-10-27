import 'package:bang/cards/model/action_cards/equipment_card.dart';
import 'package:bang/cards/model/non_playable_cards/role_card.dart';
import 'package:bang/cards/model/action_cards/weapon_card.dart';
import 'package:bang/cards/model/card_constants.dart' as Bang;
import 'package:bang/cards/model/non_playable_cards/character_card.dart';
import 'package:bang/cards/widgets/bang_card_widget.dart';
import 'package:bang/cards/widgets/non_playable_card_widget.dart';
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
    return WillPopScope(
      onWillPop: controller.showBackPopupForResult,
      child: Scaffold(
        backgroundColor: BangColors.background,
        body: Center(
          child: Obx(
            () => Stack(
              alignment: Alignment.center,
              children: [
                _buildLayout(),
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

  Widget _buildLayout() {
    switch (controller.playerNumber()) {
      case 4:
        return Column(
          mainAxisAlignment: MainAxisAlignment.spaceBetween,
          children: [
            Center(child: _ph()),
            Row(
              children: [_ph(), _ph()],
              mainAxisAlignment: MainAxisAlignment.spaceBetween,
            ),
            Align(
                alignment: Alignment.bottomCenter,
                child: PlayerView(
                  characterCard: CharacterCard(
                      background: 'willythekid',
                      health: 4,
                      name: 'willythekid'),
                  roleCard: RoleCard(name: 'sheriff', background: 'sheriff'),
                )),
          ],
        );
      case 5:
        return Column(
          mainAxisAlignment: MainAxisAlignment.spaceAround,
          children: [
            Row(
              children: [_ph(), _ph()],
              mainAxisAlignment: MainAxisAlignment.spaceAround,
            ),
            Row(
              children: [_ph(), _ph()],
              mainAxisAlignment: MainAxisAlignment.spaceBetween,
            ),
            Center(
              child: BangCardWidget(
                card: WeaponCard(
                  range: 3,
                  background: 'remington',
                  name: 'remington',
                  suit: Bang.Suit.Clubs,
                  value: Bang.Value.Ten,
                  type: Bang.CardType.Weapon,
                ),
                handCallback: () {},
                onDragSuccessCallback: () {},
              ),
            ),
          ],
        );
      case 6:
        return Column(
            mainAxisAlignment: MainAxisAlignment.spaceAround,
            children: [
              Center(child: _ph()),
              Row(
                children: [_ph(), _ph()],
                mainAxisAlignment: MainAxisAlignment.spaceBetween,
              ),
              Row(
                children: [_ph(), _ph()],
                mainAxisAlignment: MainAxisAlignment.spaceBetween,
              ),
              Center(
                child: NonPlayableCardWidget(
                  card: RoleCard(name: 'sheriff', background: 'sheriff'),
                  onTapCallback: () {},
                ),
              ),
            ]);
      case 7:
        return Column(
            mainAxisAlignment: MainAxisAlignment.spaceAround,
            children: [
              Row(
                children: [_ph(), _ph()],
                mainAxisAlignment: MainAxisAlignment.spaceAround,
              ),
              Row(
                children: [_ph(), _ph()],
                mainAxisAlignment: MainAxisAlignment.spaceBetween,
              ),
              Row(
                children: [_ph(), _ph()],
                mainAxisAlignment: MainAxisAlignment.spaceBetween,
              ),
              Center(
                child: NonPlayableCardWidget(
                  card: CharacterCard(
                      background: 'willythekid',
                      name: 'willythekid',
                      health: 4),
                  onTapCallback: () {},
                ),
              )
            ]);
      default:
        return Text('default');
    }
  }
}
