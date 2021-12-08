import 'dart:math';

import 'package:animated_icon_button/animated_icon_button.dart';
import 'package:bang/core/app_colors.dart';
import 'package:bang/core/helpers/card_helpers.dart';
import 'package:bang/models/cards/non_playable_cards/character_card.dart';
import 'package:bang/models/cards/non_playable_cards/role_card.dart';
import 'package:bang/models/cards/playable_card_base.dart';
import 'package:bang/pages/game/widgets/hand.dart';
import 'package:bang/widgets/non_playable_card_widget.dart';
import 'package:bang/widgets/playable_card.dart';
import 'package:flutter/material.dart';
import 'package:font_awesome_flutter/font_awesome_flutter.dart';

class Player extends StatelessWidget {
  final CharacterCard characterCard;
  final RoleCard roleCard;
  final pixelPerHealth = (CardHelpers.cardHeight * 0.4 - 2 * 8) / 5;
  Player({
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
    required this.nextTurn,
    required this.health,
    required this.discard,
    this.reactionCallback,
    this.dragOnEquipmentAccept,
    this.dragOnEquipmentWillAccept,
    this.dragOnWillAccept,
    this.dragOnAccept,
    this.currentRoundGlow = false,
    this.nextActionGlow = false,
    this.targetGlow = false,
  });
  final random = Random();
  final bool currentRoundGlow;
  final bool nextActionGlow;
  final bool targetGlow;
  final bool isHandViewExpanded;
  final bool isEquipmentViewExpanded;
  final VoidCallback toggleEquipmentView;
  final VoidCallback handDoubleTap;
  final VoidCallback discard;
  final VoidCallback nextTurn;
  final List<PlayableCardBase> equipment;
  final List<PlayableCardBase> temporaryEffects;
  final List<Widget> cardsInHand;
  final int highlightedIndexInHand;
  final int health;
  final bool Function(String?)? dragOnWillAccept;
  final void Function(String?)? dragOnAccept;
  final void Function(String?)? reactionCallback;

  final bool Function(String?)? dragOnEquipmentWillAccept;
  final void Function(String)? dragOnEquipmentAccept;

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

  Widget _buildSmallCharacterCard() => isHandViewExpanded
      ? Positioned(
          top: 150,
          right: 10,
          child: NonPlayableCard(
            scale: 0.4,
            card: characterCard,
            targetGlow: targetGlow,
            nextActionGlow: nextActionGlow,
            currentRoundGlow: currentRoundGlow,
            canBeFocused: false,
            dragOnWillAccept: dragOnWillAccept,
            dragOnAccept: dragOnAccept,
          ),
        )
      : Container();

  Widget _buildRoleHealthCharacter() => Expanded(
        child: Container(
          child: isHandViewExpanded
              ? null
              : Stack(
                  alignment: Alignment.centerRight,
                  fit: StackFit.passthrough,
                  clipBehavior: Clip.antiAliasWithSaveLayer,
                  children: [
                      _buildNextPlayerButton(),
                      AnimatedPositioned(
                        curve: Curves.bounceInOut,
                        duration: Duration(
                          milliseconds: 250,
                        ),
                        bottom: 20,
                        right: 2 * CardHelpers.cardWidth * 0.4 +
                            14 -
                            health -
                            (5 - health) * pixelPerHealth -
                            85,
                        child: Transform.rotate(
                          angle: pi / 2,
                          child: NonPlayableCard(
                            scale: 0.9,
                            showBackPermanently: true,
                            card: characterCard,
                            canBeFocused: false,
                          ),
                        ),
                      ),
                      Positioned(
                        bottom: 20,
                        child: NonPlayableCard(
                          scale: 0.95,
                          targetGlow: targetGlow,
                          nextActionGlow: nextActionGlow,
                          currentRoundGlow: currentRoundGlow,
                          card: characterCard,
                        ),
                      ),
                      Positioned(
                        bottom: 20,
                        left: 10,
                        child: NonPlayableCard(
                          card: roleCard,
                          scale: 0.95,
                        ),
                      ),
                      _buildEquipmentViewTogglerButton()
                    ]),
        ),
        flex: isHandViewExpanded ? 0 : 22,
      );

  Widget _buildDiscardButton() => Positioned(
        bottom: 15,
        right: 10,
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
                child: SizedBox(
                  height: 38,
                  width: 38,
                  child: IconButton(
                      icon: FaIcon(
                        FontAwesomeIcons.trashAlt,
                      ),
                      iconSize: 18,
                      color: Colors.white,
                      onPressed: discard),
                ),
              )),
        ),
      );
  Widget _buildReactionButton() => Positioned(
        bottom: 15,
        right: 60,
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
                child: SizedBox(
                  height: 38,
                  width: 38,
                  child: IconButton(
                      icon: FaIcon(
                        FontAwesomeIcons.heartbeat,
                      ),
                      iconSize: 18,
                      color: Colors.white,
                      onPressed: () => reactionCallback?.call(null)),
                ),
              )),
        ),
      );

  Widget _buildHand() => Expanded(
        child: Stack(
          children: [
            ..._buildEquipmentView(
                isEquipmentViewExpanded && isHandViewExpanded, 208),
            _buildSmallCharacterCard(),
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
            _buildDiscardButton(),
            isHandViewExpanded && reactionCallback != null
                ? _buildReactionButton()
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
  Widget _buildNextPlayerButton() => AnimatedPositioned(
        top: isEquipmentViewExpanded ? 35 : 115,
        duration: Duration(milliseconds: 200),
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
                child: SizedBox(
                  height: 38,
                  width: 38,
                  child: IconButton(
                      icon: FaIcon(
                        FontAwesomeIcons.handPointUp,
                      ),
                      iconSize: 18,
                      color: Colors.white,
                      onPressed: nextTurn),
                ),
              )),
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
                  child: PlayableCard(
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
                  child: DragTarget(
                    onWillAccept: (_) =>
                        dragOnEquipmentWillAccept?.call(equipment[i].id) ??
                        false,
                    onAccept: (data) =>
                        dragOnEquipmentAccept?.call(equipment[i].id),
                    builder: (context, candidateData, rejectedData) =>
                        PlayableCard(
                      card: equipment[i],
                      scale: 0.55,
                      highlightMultiplier: 6.3 / 5,
                    ),
                  ),
                ),
              )
          ]
        : [];
  }
}
