// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'game_event_dto.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

GameEventDto _$GameEventDtoFromJson(Map<String, dynamic> json) => GameEventDto(
      card: json['card'] == null
          ? null
          : CardDto.fromJson(json['card'] as Map<String, dynamic>),
      characterId: json['characterId'] as String,
    );

Map<String, dynamic> _$GameEventDtoToJson(GameEventDto instance) =>
    <String, dynamic>{
      'card': instance.card,
      'characterId': instance.characterId,
    };
