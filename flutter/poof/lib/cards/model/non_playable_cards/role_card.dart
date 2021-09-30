import 'package:bang/cards/model/card_constants.dart';
import 'non_playable_card_base.dart';

class RoleCard extends NonPlayableCardBase {
  RoleCard({
    required String background,
    required String name,
  }) : super(background: background, name: name, type: CardType.Role);
}
