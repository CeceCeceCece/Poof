import 'package:bang/core/colors.dart';
import '../bang_card.dart';
import '../card_constants.dart';

class EquipmentCard extends BangCard {
  EquipmentCard(
      {required String background,
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
