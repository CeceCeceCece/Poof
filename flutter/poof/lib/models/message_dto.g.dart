// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'message_dto.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

MessageDto _$MessageDtoFromJson(Map<String, dynamic> json) => MessageDto(
      sender: json['sender'] as String,
      text: json['text'] as String,
      postedDate: DateTime.parse(json['postedDate'] as String),
    );

Map<String, dynamic> _$MessageDtoToJson(MessageDto instance) =>
    <String, dynamic>{
      'sender': instance.sender,
      'text': instance.text,
      'postedDate': instance.postedDate.toIso8601String(),
    };
