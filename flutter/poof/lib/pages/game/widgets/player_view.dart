import 'dart:math';

import 'package:bang/cards/model/non_playable_cards/character_card.dart';
import 'package:bang/cards/model/non_playable_cards/non_playable_card_base.dart';
import 'package:bang/cards/model/non_playable_cards/role_card.dart';
import 'package:bang/cards/widgets/card_widget_helpers.dart';
import 'package:bang/cards/widgets/non_playable_card_widget.dart';
import 'package:bang/pages/game/widgets/hand_of_cards.dart';
import 'package:bang/services/game_service.dart';
import 'package:flutter/material.dart';
import 'package:get/get.dart';

class PlayerView extends StatelessWidget {
  final CharacterCard characterCard;
  final RoleCard roleCard;
  final pixelPerHealth = (CardWidgetHelpers.cardHeight * 0.4 - 2 * 8) / 5;
  PlayerView({required this.characterCard, required this.roleCard});
  final controller = Get.find<GameService>();

  @override
  Widget build(BuildContext context) {
    var health = 4;
    return Obx(() => AnimatedContainer(
          duration: Duration(milliseconds: 150),
          height: controller.expandedHandView() ? 275 : 150,
          child: Row(
            children: [
              Expanded(
                child: Container(
                  child: controller.expandedHandView()
                      ? null
                      : Stack(alignment: Alignment.centerRight, children: [
                          Positioned(
                            right: 2 * CardWidgetHelpers.cardWidth * 0.4 +
                                14 -
                                health -
                                (5 - health) * pixelPerHealth,
                            child: Transform.rotate(
                              angle: pi / 2,
                              child: NonPlayableCardWidget(
                                  scale: 0.9,
                                  showBackPermanently: true,
                                  card: characterCard,
                                  onTapCallback: () {}),
                            ),
                          ),
                          Row(
                            mainAxisAlignment: MainAxisAlignment.end,
                            children: [
                              NonPlayableCardWidget(
                                  scale: 0.95,
                                  card: characterCard,
                                  onTapCallback: () {}),
                              NonPlayableCardWidget(
                                card: roleCard,
                                scale: 0.95,
                                onTapCallback: () {},
                              ),
                            ],
                          ),
                        ]),
                ),
                flex: controller.expandedHandView() ? 0 : 7,
              ),
              Expanded(
                child: Hand(),
                flex: controller.expandedHandView() ? 10 : 3,
              )
            ],
          ),
        ));
  }
}
