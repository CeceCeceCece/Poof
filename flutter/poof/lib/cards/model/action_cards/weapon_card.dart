import 'package:bang/core/colors.dart';
import '../bang_card.dart';
import '../card_constants.dart';

class WeaponCard extends BangCard {
  final int range;

  WeaponCard(
      {required this.range,
      required String background,
      required String name,
      required Value value,
      required CardType type,
      required Suit suit})
      : super(
          background: background,
          suit: suit,
          value: value,
          name: name,
          type: type,
        ) {
    color = BangColors.equipmentCardColor;
  }
}
