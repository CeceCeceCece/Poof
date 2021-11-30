// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'life_point_dto.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

LifePointDto _$LifePointDtoFromJson(Map<String, dynamic> json) => LifePointDto(
      lifePoint: json['lifePoint'] as int,
      characterId: json['characterId'] as String,
    );

Map<String, dynamic> _$LifePointDtoToJson(LifePointDto instance) =>
    <String, dynamic>{
      'lifePoint': instance.lifePoint,
      'characterId': instance.characterId,
    };
