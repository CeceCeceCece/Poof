import 'package:bang/core/app_colors.dart';

import '../bang_card.dart';
import '../card_constants.dart';

class WeaponCard extends BangCard {
  final int range;

  WeaponCard(
      {required this.range,
      required String background,
      required String name,
      required CardValue value,
      required CardType type,
      required CardSuit suit})
      : super(
          background: background,
          suit: suit,
          value: value,
          name: name,
          type: type,
        ) {
    borderColor = AppColors.equipmentCardColor;
  }
}
