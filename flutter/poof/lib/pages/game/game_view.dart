import 'package:bang/cards/model/action_cards/equipment_card.dart';
import 'package:bang/cards/model/non_playable_cards/role_card.dart';
import 'package:bang/cards/model/action_cards/weapon_card.dart';
import 'package:bang/cards/model/card_constants.dart' as Bang;
import 'package:bang/cards/model/non_playable_cards/character_card.dart';
import 'package:bang/cards/widgets/bang_card_widget.dart';
import 'package:bang/cards/widgets/non_playable_card_widget.dart';
import 'package:bang/core/colors.dart';
import 'package:bang/pages/game/game_controller.dart';
import 'package:flutter/material.dart';
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
                Column(
                  mainAxisAlignment: MainAxisAlignment.end,
                  children: [
                    ElevatedButton(
                        onPressed: controller.randomizePlayerNumber,
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
          mainAxisAlignment: MainAxisAlignment.spaceAround,
          children: [
            Center(child: _ph()),
            Row(
              children: [_ph(), _ph()],
              mainAxisAlignment: MainAxisAlignment.spaceBetween,
            ),
            Center(
              child: BangCardWidget(
                card: EquipmentCard(
                  background: 'barrel',
                  name: 'barrel',
                  suit: Bang.Suit.Clubs,
                  value: Bang.Value.Ten,
                  type: Bang.CardType.Equipment,
                ),
                handCallback: () {},
                onTapCallback: () {},
              ),
            ),
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
                onTapCallback: () {},
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
