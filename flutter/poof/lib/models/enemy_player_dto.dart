import 'package:bang/models/cards/playable_cards/equipment_card.dart';
import 'package:bang/models/role_type.dart';

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
    this.isDead = false,
    this.role,
  });
  List<String> cardIds;
  String playerId;
  String playerName;
  bool isSheriff;
  int health;
  bool isDead;
  RoleType? role;
  String characterName;
  List<EquipmentCard> equipment;
  List<EquipmentCard> temporaryEffects;

  static EnemyPlayerDto get dummy => EnemyPlayerDto(
        cardIds: [],
        characterName: '',
        equipment: [],
        health: 0,
        isSheriff: false,
        playerId: '',
        playerName: '',
        temporaryEffects: [],
      );
}
