// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'game_start_dto.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

GameStartDto _$GameStartDtoFromJson(Map<String, dynamic> json) => GameStartDto(
      name: json['name'] as String,
      selfId: json['selfId'] as String,
      cards: (json['cards'] as List<dynamic>)
          .map((e) => CardDto.fromJson(e as Map<String, dynamic>))
          .toList(),
      sheriffId: json['sheriffId'] as String,
      role: $enumDecode(_$RoleTypeEnumMap, json['role']),
      characters: (json['characters'] as List<dynamic>)
          .map((e) => CharacterDto.fromJson(e as Map<String, dynamic>))
          .toList(),
      lifePoint: json['lifePoint'] as int,
    );

Map<String, dynamic> _$GameStartDtoToJson(GameStartDto instance) =>
    <String, dynamic>{
      'name': instance.name,
      'role': _$RoleTypeEnumMap[instance.role],
      'selfId': instance.selfId,
      'sheriffId': instance.sheriffId,
      'cards': instance.cards,
      'characters': instance.characters,
      'lifePoint': instance.lifePoint,
    };

const _$RoleTypeEnumMap = {
  RoleType.None: 0,
  RoleType.Outlaw: 1,
  RoleType.Renegade: 2,
  RoleType.Sheriff: 3,
  RoleType.DeputySheriff: 4,
};
