import 'package:bang/core/app_colors.dart';
import 'package:bang/core/helpers/card_helpers.dart';

import '../playable_card_base.dart';

class ActionCard extends PlayableCardBase {
  final int range;

  ActionCard(
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
    borderColor = AppColors.actionCardColor;
  }
}