// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'winner_is_dto.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

WinnerIsDto _$WinnerIsDtoFromJson(Map<String, dynamic> json) => WinnerIsDto(
      winner: $enumDecode(_$RoleTypeEnumMap, json['winner']),
    );

Map<String, dynamic> _$WinnerIsDtoToJson(WinnerIsDto instance) =>
    <String, dynamic>{
      'winner': _$RoleTypeEnumMap[instance.winner],
    };

const _$RoleTypeEnumMap = {
  RoleType.None: 0,
  RoleType.Outlaw: 1,
  RoleType.Renegade: 2,
  RoleType.Sheriff: 3,
  RoleType.DeputySheriff: 4,
};
