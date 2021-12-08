import 'package:bang/core/app_colors.dart';
import 'package:bang/models/cards/non_playable_cards/character_card.dart';
import 'package:bang/models/role_type.dart';
import 'package:bang/widgets/non_playable_card_widget.dart';
import 'package:bang/widgets/playable_card.dart';
import 'package:flutter/material.dart';
import 'package:font_awesome_flutter/font_awesome_flutter.dart';
import 'package:google_fonts/google_fonts.dart';

class EnemyPlayer extends StatelessWidget {
  EnemyPlayer({
    Key? key,
    this.left = false,
    this.top = false,
    this.right = false,
    this.isDead = false,
    required this.playerName,
    required this.cardAmount,
    required this.health,
    this.cardIds = const [],
    this.isSheriff = false,
    this.playerId = '',
    required this.characterName,
    required this.temporaryEffects,
    required this.equipment,
    this.isTakingNextAction = false,
    this.canBeTargeted = false,
    this.role,
    this.dragOnCharacterAccept,
    this.dragOnCharacterWillAccept,
    this.hasTargetableCard = false,
    this.currentlyHasRound = false,
  }) : super(key: key);

  final bool left;
  final bool top;
  final bool right;
  final RoleType? role;
  final bool isDead;
  final String playerId;
  final bool isSheriff;
  final List<String> cardIds;
  final String playerName;
  final int cardAmount;
  final int health;
  final String characterName;
  final bool Function(String?)? dragOnCharacterWillAccept;
  final void Function(String)? dragOnCharacterAccept;

  final bool isTakingNextAction;
  final bool canBeTargeted;
  final bool currentlyHasRound;
  final bool hasTargetableCard;

  final List<PlayableCard> temporaryEffects;
  final List<PlayableCard> equipment;

  @override
  Widget build(BuildContext context) {
    double? rightChar = top ? (right ? 102.5 : null) : (right ? 45 : null);
    double? leftChar = top ? (left ? 102.5 : null) : (left ? 45 : null);
    double? rightCards = top ? (left ? 102.5 : null) : null;
    double? leftCards = top ? (right ? 102.5 : null) : null;

    double? rightTemporaryEffectCards =
        top ? (right ? 147.5 : null) : (right ? 90 : null);
    double? leftTemporaryEffectCards =
        top ? (left ? 147.5 : null) : (left ? 90 : null);
    double? leftEquipmentCards = top ? (left ? 55 : null) : (left ? 0 : null);
    double? rightEquipmentCards =
        top ? (right ? 55 : null) : (right ? 0 : null);
    return Container(
      child: Stack(
          alignment: top
              ? Alignment.topCenter
              : (left ? Alignment.topLeft : Alignment.topRight),
          children: [
            SizedBox(
              height: 200,
              width: 200,
            ),
            Positioned(
              top: equipment.isEmpty ? 75 : 110,
              child: Container(
                width: 120,
                child: Text(
                  playerName,
                  overflow: TextOverflow.ellipsis,
                  maxLines: 1,
                  style: GoogleFonts.graduate(
                    textStyle: TextStyle(
                        fontSize: 15,
                        fontWeight: FontWeight.bold,
                        color: AppColors.background),
                  ),
                  textAlign: TextAlign.center,
                ),
              ),
            ),
            for (int i = 0; i < equipment.length; i++)
              Positioned(
                  top: 70,
                  right: right
                      ? ((equipment.length - 1) * 23 -
                          i * 23.0 +
                          rightEquipmentCards!)
                      : null,
                  left: left
                      ? ((equipment.length - 1) * 23 -
                          i * 23.0 +
                          leftEquipmentCards!)
                      : null,
                  child: equipment[i]),
            for (int i = 0; i < temporaryEffects.length; i++)
              Positioned(
                  top: (temporaryEffects.length - 1) * 37 - i * 37,
                  right: rightTemporaryEffectCards,
                  left: leftTemporaryEffectCards,
                  child: temporaryEffects[i]),
            Positioned(
              right: rightCards,
              left: leftCards,
              child: _backWithAmount(cardAmount != 0),
            ),
            Positioned(
              right: rightChar,
              left: leftChar,
              child: _buildCharacter(),
            ),
          ]),
    );
  }

  Widget _buildCharacter() {
    return DragTarget(
        onWillAccept: (_) => dragOnCharacterWillAccept?.call(playerId) ?? false,
        onAccept: (data) => dragOnCharacterAccept?.call(playerId),
        builder: (context, candidateData, rejectedData) => Stack(
              children: [
                Positioned(
                  child: NonPlayableCard(
                    scale: 0.5,
                    isEnemyPlayer: true,
                    isDead: isDead,
                    role: role,
                    nextActionGlow: isTakingNextAction,
                    targetGlow: canBeTargeted,
                    currentRoundGlow: currentlyHasRound,
                    highlightMultiplier: 1.5,
                    card: CharacterCard(
                        background: characterName,
                        health: 4,
                        name: characterName),
                  ),
                ),
                Positioned(
                  bottom: 0,
                  left: 0,
                  child: Stack(
                    children: [
                      FaIcon(
                        FontAwesomeIcons.solidHeart,
                        color: AppColors.buttonShadowColor,
                        size: 20,
                      ),
                      Positioned(
                        bottom: 5,
                        left: 6.5,
                        child: Text(
                          health.toString(),
                          style: TextStyle(
                              color: Colors.white,
                              fontWeight: FontWeight.bold,
                              fontSize: 12),
                        ),
                      ),
                    ],
                  ),
                ),
                isSheriff
                    ? Positioned(
                        top: 0,
                        right: 0,
                        child: FaIcon(FontAwesomeIcons.certificate,
                            color: AppColors.buttonShadowColor, size: 20),
                      )
                    : Container()
              ],
            ));
  }

  Widget _backWithAmount(bool anyCards) {
    return DragTarget(
        onWillAccept: (_) => dragOnCharacterWillAccept?.call(playerId) ?? false,
        onAccept: (data) => dragOnCharacterAccept?.call(playerId),
        builder: (context, candidateData, rejectedData) => anyCards
            ? Stack(
                alignment: Alignment.center,
                children: [
                  PlayableCard.back(canBeTargeted: hasTargetableCard),
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
                    cardAmount.toString(),
                    style: TextStyle(
                        color: Colors.white,
                        fontWeight: FontWeight.bold,
                        fontSize: 20),
                  ),
                ],
              )
            : Container(height: 70, width: 45, color: Colors.transparent));
  }
}
