import 'package:bang/core/helpers/card_helpers.dart';
import 'package:bang/models/role_type.dart';

import 'non_playable_card_base.dart';

class RoleCard extends NonPlayableCardBase {
  final RoleType role;

  RoleCard({
    required this.role,
  }) : super(
            background: CardHelpers.roleToString(role),
            name: CardHelpers.roleToString(role),
            type: CardType.Role);
}
