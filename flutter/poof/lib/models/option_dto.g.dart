// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'option_dto.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

OptionDto _$OptionDtoFromJson(Map<String, dynamic> json) => OptionDto(
      description: json['description'] as String,
      possibleTargets: (json['possibleTargets'] as List<dynamic>)
          .map((e) => e as String)
          .toList(),
      numberOfCards: json['numberOfCards'] as int,
      requireAnswear: json['requireAnswear'] as bool?,
      requireCards: json['requireCards'] as bool?,
    );

Map<String, dynamic> _$OptionDtoToJson(OptionDto instance) => <String, dynamic>{
      'description': instance.description,
      'possibleTargets': instance.possibleTargets,
      'requireCards': instance.requireCards,
      'numberOfCards': instance.numberOfCards,
      'requireAnswear': instance.requireAnswear,
    };
