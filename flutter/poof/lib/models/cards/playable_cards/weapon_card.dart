import 'package:bang/core/app_colors.dart';
import 'package:bang/core/helpers/card_helpers.dart';

import '../playable_card_base.dart';

class WeaponCard extends PlayableCardBase {
  final int range;

  WeaponCard(
      {required this.range,
      required String background,
      required String name,
      required CardValue value,
      required CardType type,
      String id = '',
      required CardSuit suit})
      : super(
          background: background,
          suit: suit,
          value: value,
          id: id,
          name: name,
          type: type,
        ) {
    borderColor = AppColors.equipmentCardColor;
  }
}
