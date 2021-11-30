// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'game_event_dto.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

GameEventDto _$GameEventDtoFromJson(Map<String, dynamic> json) => GameEventDto(
      card: CardDto.fromJson(json['card'] as Map<String, dynamic>),
      characterId: json['characterId'] as String,
      event: $enumDecode(_$GameEventEnumMap, json['event']),
    );

Map<String, dynamic> _$GameEventDtoToJson(GameEventDto instance) =>
    <String, dynamic>{
      'card': instance.card,
      'event': _$GameEventEnumMap[instance.event],
      'characterId': instance.characterId,
    };

const _$GameEventEnumMap = {
  GameEvent.None: 0,
  GameEvent.Draw: 1,
  GameEvent.SingleReact: 2,
  GameEvent.CallerReact: 3,
  GameEvent.AllReact: 4,
};
