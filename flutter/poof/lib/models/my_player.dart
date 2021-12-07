import 'package:bang/models/role_type.dart';
import 'package:bang/widgets/playable_card.dart';

import 'cards/playable_card_base.dart';

class MyPlayer {
  MyPlayer({
    required this.cards,
    required this.role,
    required this.health,
    required this.characterName,
    required this.id,
    this.equipment = const [],
    this.temporaryEffects = const [],
  });
  List<PlayableCardBase> cards;
  String characterName;
  RoleType role;
  int health;
  String id;
  List<PlayableCard> equipment;
  List<PlayableCard> temporaryEffects;
}
