import 'package:bang/core/helpers/card_helpers.dart';

import '../card_base.dart';

abstract class NonPlayableCardBase extends CardBase {
  final CardType type;
  NonPlayableCardBase({
    required String background,
    required String name,
    required this.type,
  }) : super(background: background, name: name);
}
