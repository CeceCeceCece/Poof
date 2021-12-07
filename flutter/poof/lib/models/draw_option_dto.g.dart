// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'draw_option_dto.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

DrawOptionDto _$DrawOptionDtoFromJson(Map<String, dynamic> json) =>
    DrawOptionDto(
      userIdRequired: json['userIdRequired'] as bool,
      cards: (json['cards'] as List<dynamic>)
          .map((e) => CardDto.fromJson(e as Map<String, dynamic>))
          .toList(),
    );

Map<String, dynamic> _$DrawOptionDtoToJson(DrawOptionDto instance) =>
    <String, dynamic>{
      'userIdRequired': instance.userIdRequired,
      'cards': instance.cards,
    };
