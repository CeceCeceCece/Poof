import 'package:bang/cards/model/card_constants.dart';
import 'non_playable_card_base.dart';

class CharacterCard extends NonPlayableCardBase {
  int health;
  CharacterCard({
    required this.health,
    required String background,
    required String name,
  }) : super(background: background, name: name, type: CardType.Character);
}