// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'player_died_dto.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

PlayerDiedDto _$PlayerDiedDtoFromJson(Map<String, dynamic> json) =>
    PlayerDiedDto(
      userId: json['userId'] as String,
      role: $enumDecode(_$RoleTypeEnumMap, json['role']),
    );

Map<String, dynamic> _$PlayerDiedDtoToJson(PlayerDiedDto instance) =>
    <String, dynamic>{
      'userId': instance.userId,
      'role': _$RoleTypeEnumMap[instance.role],
    };

const _$RoleTypeEnumMap = {
  RoleType.None: 0,
  RoleType.Outlaw: 1,
  RoleType.Renegade: 2,
  RoleType.Sheriff: 3,
  RoleType.DeputySheriff: 4,
};
