import 'package:bang/cards/model/action_cards/action_card.dart';
import 'package:bang/cards/model/action_cards/equipment_card.dart';
import 'package:bang/cards/model/card_constants.dart' as Bang;
import 'package:bang/cards/model/non_playable_cards/character_card.dart';
import 'package:bang/cards/widgets/bang_card_widget.dart';
import 'package:bang/cards/widgets/non_playable_card_widget.dart';
import 'package:bang/core/app_colors.dart';
import 'package:flutter/material.dart';
import 'package:google_fonts/google_fonts.dart';

class EnemyPlayer extends StatelessWidget {
  EnemyPlayer(
      {Key? key, this.left = false, this.top = false, this.right = false})
      : super(key: key);

  final bool left;
  final bool top;
  final bool right;

  final equipmentCards = [
    BangCardWidget(
      card: EquipmentCard(
          background: 'barrel',
          name: 'barrel',
          value: Bang.Value.Ten,
          type: Bang.CardType.Equipment,
          suit: Bang.Suit.Diamonds),
      canBeFocused: true,
      scale: 0.25,
      highlightMultiplier: 1.5,
    ),
    BangCardWidget(
      card: EquipmentCard(
          background: 'barrel',
          name: 'barrel',
          value: Bang.Value.Ten,
          type: Bang.CardType.Equipment,
          suit: Bang.Suit.Diamonds),
      canBeFocused: true,
      scale: 0.25,
      highlightMultiplier: 1.5,
    ),
    BangCardWidget(
      card: EquipmentCard(
          background: 'barrel',
          name: 'barrel',
          value: Bang.Value.Ten,
          type: Bang.CardType.Equipment,
          suit: Bang.Suit.Diamonds),
      canBeFocused: true,
      scale: 0.25,
      highlightMultiplier: 1.5,
    ),
    BangCardWidget(
      card: EquipmentCard(
          background: 'barrel',
          name: 'barrel',
          value: Bang.Value.Ten,
          type: Bang.CardType.Equipment,
          suit: Bang.Suit.Diamonds),
      canBeFocused: true,
      scale: 0.25,
      highlightMultiplier: 1.5,
    ),
  ];

  final temporaryEffectCards = [
    BangCardWidget(
      card: EquipmentCard(
          background: 'dynamite',
          name: 'dynamite',
          value: Bang.Value.Ten,
          type: Bang.CardType.Equipment,
          suit: Bang.Suit.Diamonds),
      canBeFocused: true,
      scale: 0.25,
      highlightMultiplier: 1.5,
    ),
    BangCardWidget(
      card: EquipmentCard(
          background: 'jail',
          name: 'jail',
          value: Bang.Value.Ten,
          type: Bang.CardType.Equipment,
          suit: Bang.Suit.Diamonds),
      canBeFocused: true,
      scale: 0.25,
      highlightMultiplier: 1.5,
    ),
  ];

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
              //child: Container(color: left ? Colors.green : Colors.red),
            ),
            Positioned(
              top: equipmentCards.isEmpty ? 75 : 110,
              child: Container(
                width: 120,
                child: Text(
                  'Cece',
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
            for (int i = 0; i < equipmentCards.length; i++)
              Positioned(
                  top: 70,
                  right: right
                      ? ((equipmentCards.length - 1) * 23 -
                          i * 23.0 +
                          rightEquipmentCards!)
                      : null,
                  left: left
                      ? ((equipmentCards.length - 1) * 23 -
                          i * 23.0 +
                          leftEquipmentCards!)
                      : null,
                  child: equipmentCards[i]),
            for (int i = 0; i < temporaryEffectCards.length; i++)
              Positioned(
                  top: (temporaryEffectCards.length - 1) * 37 - i * 37,
                  right: rightTemporaryEffectCards,
                  left: leftTemporaryEffectCards,
                  child: temporaryEffectCards[i]),
            Positioned(
              right: rightCards,
              left: leftCards,
              child: _backWithAmount(5),
            ),
            Positioned(
              right: rightChar,
              left: leftChar,
              child: _buildCharacter(
                characterName: 'willythekid',
                health: 4,
              ),
            ),
          ]),
    );
  }

  Widget _buildCharacter({required String characterName, required int health}) {
    return Stack(
      alignment: Alignment.bottomLeft,
      children: [
        Positioned(
          child: NonPlayableCardWidget(
            scale: 0.5,
            highlightMultiplier: 1.5,
            card: CharacterCard(
                background: characterName, health: 4, name: characterName),
          ),
        ),
        Text('❤: $health', style: TextStyle(fontWeight: FontWeight.bold)),
      ],
    );
  }

  Widget _backWithAmount(int amount) {
    return Stack(
      alignment: Alignment.center,
      children: [
        BangCardWidget(
          scale: 0.5,
          showBackPermanently: true,
          canBeFocused: false,
          card: ActionCard(
            background: 'stagecoach',
            name: 'stagecoach',
            suit: Bang.Suit.Clubs,
            value: Bang.Value.Nine,
            type: Bang.CardType.Action,
            range: 0,
          ),
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
          '$amount',
          style: TextStyle(
              color: Colors.white, fontWeight: FontWeight.bold, fontSize: 20),
        ),
      ],
    );
  }
}
