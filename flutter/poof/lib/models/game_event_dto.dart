import 'package:json_annotation/json_annotation.dart';

import 'card_dto.dart';

part 'game_event_dto.g.dart';

@JsonSerializable()
class GameEventDto {
  final CardDto? card;

  final String characterId;

  GameEventDto({
    this.card,
    required this.characterId,
  });

  factory GameEventDto.fromJson(Map<String, dynamic> json) =>
      _$GameEventDtoFromJson(json);

  Map<String, dynamic> toJson() => _$GameEventDtoToJson(this);
}
