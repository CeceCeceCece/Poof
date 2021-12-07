// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'option_command.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

OptionCommand _$OptionCommandFromJson(Map<String, dynamic> json) =>
    OptionCommand(
      userId: json['userId'] as String,
      cardIds:
          (json['cardIds'] as List<dynamic>).map((e) => e as String).toList(),
    );

Map<String, dynamic> _$OptionCommandToJson(OptionCommand instance) =>
    <String, dynamic>{
      'userId': instance.userId,
      'cardIds': instance.cardIds,
    };
