// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'character_dto.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

CharacterDto _$CharacterDtoFromJson(Map<String, dynamic> json) => CharacterDto(
      name: json['name'] as String,
      cardIds:
          (json['cardIds'] as List<dynamic>).map((e) => e as String).toList(),
      lifePoint: json['lifePoint'] as int,
      userId: json['userId'] as String,
      userName: json['userName'] as String,
    );

Map<String, dynamic> _$CharacterDtoToJson(CharacterDto instance) =>
    <String, dynamic>{
      'name': instance.name,
      'lifePoint': instance.lifePoint,
      'cardIds': instance.cardIds,
      'userId': instance.userId,
      'userName': instance.userName,
    };
