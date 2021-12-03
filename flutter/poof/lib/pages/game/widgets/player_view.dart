import 'dart:math';

import 'package:animated_icon_button/animated_icon_button.dart';
import 'package:bang/cards/model/bang_card.dart';
import 'package:bang/cards/model/non_playable_cards/character_card.dart';
import 'package:bang/cards/model/non_playable_cards/role_card.dart';
import 'package:bang/cards/widgets/bang_card_widget.dart';
import 'package:bang/cards/widgets/card_widget_helpers.dart';
import 'package:bang/cards/widgets/non_playable_card_widget.dart';
import 'package:bang/core/app_colors.dart';
import 'package:bang/pages/game/widgets/hand.dart';
import 'package:flutter/material.dart';

class PlayerView extends StatelessWidget {
  final CharacterCard characterCard;
  final RoleCard roleCard;
  final pixelPerHealth = (CardWidgetHelpers.cardHeight * 0.4 - 2 * 8) / 5;
  PlayerView({
    required this.characterCard,
    required this.roleCard,
    required this.isHandViewExpanded,
    required this.isEquipmentViewExpanded,
    required this.toggleEquipmentView,
    required this.handDoubleTap,
    required this.equipment,
    required this.temporaryEffects,
    required this.cardsInHand,
    required this.highlightedIndexInHand,
    required this.health,
  });
  final random = Random();
  final bool isHandViewExpanded;
  final bool isEquipmentViewExpanded;
  final VoidCallback toggleEquipmentView;
  final VoidCallback handDoubleTap;
  final List<BangCard> equipment;
  final List<BangCard> temporaryEffects;
  final List<Widget> cardsInHand;
  final int highlightedIndexInHand;
  final int health;

  @override
  Widget build(BuildContext context) {
    return AnimatedContainer(
      duration: Duration(milliseconds: 150),
      height: isHandViewExpanded ? 370 : 310,
      child: Stack(
        children: [
          ..._buildEquipmentView(
              !isHandViewExpanded && isEquipmentViewExpanded, 157),
          Column(
            mainAxisAlignment: MainAxisAlignment.end,
            children: [
              Container(
                height: isHandViewExpanded ? 350 : 310,
                child: Row(
                  crossAxisAlignment: CrossAxisAlignment.end,
                  children: [
                    _buildRoleHealthCharacter(),
                    _buildHand(),
                  ],
                ),
              ),
            ],
          ),
        ],
      ),
    );
  }

  Widget _buildRoleHealthCharacter() => Expanded(
        child: Container(
          child: isHandViewExpanded
              ? null
              : Stack(
                  alignment: Alignment.centerRight,
                  fit: StackFit.passthrough,
                  clipBehavior: Clip.antiAliasWithSaveLayer,
                  children: [
                      AnimatedPositioned(
                        curve: Curves.bounceInOut,
                        duration: Duration(
                          milliseconds: 250,
                        ),
                        bottom: 20,
                        right: 2 * CardWidgetHelpers.cardWidth * 0.4 +
                            14 -
                            health -
                            (5 - health) * pixelPerHealth -
                            85,
                        child: Transform.rotate(
                          angle: pi / 2,
                          child: NonPlayableCardWidget(
                            scale: 0.9,
                            showBackPermanently: true,
                            card: characterCard,
                            canBeFocused: false,
                          ),
                        ),
                      ),
                      Positioned(
                        bottom: 20,
                        child: NonPlayableCardWidget(
                          scale: 0.95,
                          card: characterCard,
                        ),
                      ),
                      Positioned(
                        bottom: 20,
                        left: 10,
                        child: NonPlayableCardWidget(
                          card: roleCard,
                          scale: 0.95,
                        ),
                      ),
                      _buildEquipmentViewTogglerButton()
                    ]),
        ),
        flex: isHandViewExpanded ? 0 : 22,
      );

  Widget _buildHand() => Expanded(
        child: Stack(
          children: [
            ..._buildEquipmentView(
                isEquipmentViewExpanded && isHandViewExpanded, 208),
            Hand(
              indexOfFocusedCard: highlightedIndexInHand,
              cards: cardsInHand,
              isExpanded: isHandViewExpanded,
              handSize: cardsInHand.length,
              onDoubleTap: handDoubleTap,
            ),
            isHandViewExpanded
                ? _buildEquipmentViewTogglerButton()
                : Container(),
          ],
        ),
        flex: isHandViewExpanded ? 30 : 8,
      );

  Widget _buildEquipmentViewTogglerButton() => Positioned(
        bottom: 15,
        left: 10,
        child: Material(
          elevation: 20,
          clipBehavior: Clip.hardEdge,
          shape: CircleBorder(),
          color: Colors.transparent,
          child: Ink(
            decoration: BoxDecoration(
                border: Border.all(width: 1, color: AppColors.darkBrown),
                gradient: AppColors.buttonGradient,
                borderRadius: BorderRadius.circular(200)),
            child: Center(
              child: AnimatedIconButton(
                  size: 18,
                  duration: Duration(milliseconds: 400),
                  onPressed: toggleEquipmentView,
                  icons: [
                    AnimatedIconItem(
                        icon: Icon(
                      Icons.arrow_upward,
                      size: 20,
                      color: Colors.white,
                    )),
                    AnimatedIconItem(
                        icon: Icon(Icons.arrow_downward,
                            size: 20, color: Colors.white)),
                  ]),
            ),
          ),
        ),
      );

  List<Widget> _buildEquipmentView(bool shouldShow, double bottomOffset) {
    return shouldShow
        ? [
            SizedBox(height: 140, width: 1),
            for (int i = 0; i < temporaryEffects.length; i++)
              Positioned(
                left:
                    (temporaryEffects.length - 1) * 54 - 54 * i.toDouble() + 5,
                bottom: bottomOffset,
                child: Transform.rotate(
                  angle: (pi / 180) * (random.nextInt(7) - 3),
                  child: BangCardWidget(
                    card: temporaryEffects[i],
                    scale: 0.55,
                    highlightMultiplier: 6.3 / 5,
                  ),
                ),
              ),
            for (int i = 0; i < equipment.length; i++)
              Positioned(
                bottom: bottomOffset,
                right: (equipment.length - 1) * 54 - 54 * i.toDouble() + 5,
                child: Transform.rotate(
                  angle: (pi / 180) * (random.nextInt(7) - 3),
                  child: BangCardWidget(
                    card: equipment[i],
                    scale: 0.55,
                    highlightMultiplier: 6.3 / 5,
                  ),
                ),
              )
          ]
        : [];
  }
}
