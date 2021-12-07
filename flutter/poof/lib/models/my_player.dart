import 'package:bang/models/role_type.dart';

import 'cards/playable_card_base.dart';
import 'cards/playable_cards/equipment_card.dart';

class MyPlayer {
  MyPlayer({
    required this.cards,
    required this.role,
    required this.health,
    required this.characterName,
    required this.id,
    required this.equipment,
    required this.temporaryEffects,
  });
  List<PlayableCardBase> cards;
  String characterName;
  RoleType role;
  int health;
  String id;
  List<EquipmentCard> equipment;
  List<EquipmentCard> temporaryEffects;
}
