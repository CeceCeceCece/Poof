import 'package:bang/cards/model/card_constants.dart';

import '../card_base.dart';

class RoleCard extends CardBase {
  CardType type = CardType.Role;
  RoleCard({
    required background,
    required name,
  }) : super(background: background, name: name);
}
