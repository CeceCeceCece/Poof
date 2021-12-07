import 'package:bang/models/cards/playable_cards/equipment_card.dart';

class EnemyPlayerDto {
  EnemyPlayerDto({
    required this.cardIds,
    required this.isSheriff,
    required this.health,
    required this.playerName,
    required this.playerId,
    required this.characterName,
    required this.equipment,
    required this.temporaryEffects,
  });
  List<String> cardIds;
  String playerId;
  String playerName;
  bool isSheriff;
  int health;
  String characterName;
  List<EquipmentCard> equipment;
  List<EquipmentCard> temporaryEffects;
}
