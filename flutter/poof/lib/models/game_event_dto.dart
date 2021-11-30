import 'package:json_annotation/json_annotation.dart';

import 'card_dto.dart';
import 'game_event.dart';

part 'game_event_dto.g.dart';

@JsonSerializable()
class GameEventDto {
  final CardDto card;
  final GameEvent event;
  final String characterId;

  GameEventDto({
    required this.card,
    required this.characterId,
    required this.event,
  });

  factory GameEventDto.fromJson(Map<String, dynamic> json) =>
      _$GameEventDtoFromJson(json);

  Map<String, dynamic> toJson() => _$GameEventDtoToJson(this);
}
