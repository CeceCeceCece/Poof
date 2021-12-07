import 'package:bang/widgets/playable_card.dart';

class EnemyPlayerDto {
  EnemyPlayerDto({
    required this.cardIds,
    required this.isSheriff,
    required this.health,
    required this.playerName,
    required this.playerId,
    required this.characterName,
    this.equipment = const [],
    this.temporaryEffects = const [],
  });
  List<String> cardIds;
  String playerId;
  String playerName;
  bool isSheriff;
  int health;
  String characterName;
  List<PlayableCard> equipment;
  List<PlayableCard> temporaryEffects;
}
