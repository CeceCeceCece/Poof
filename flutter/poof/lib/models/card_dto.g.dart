// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'card_dto.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

CardDto _$CardDtoFromJson(Map<String, dynamic> json) => CardDto(
      id: json['id'] as String,
      name: json['name'] as String,
      type: $enumDecode(_$CardTypeEnumMap, json['type']),
      suite: json['suite'],
      value: json['value'],
    );

Map<String, dynamic> _$CardDtoToJson(CardDto instance) => <String, dynamic>{
      'id': instance.id,
      'name': instance.name,
      'type': _$CardTypeEnumMap[instance.type],
      'suite': instance.suite,
      'value': instance.value,
    };

const _$CardTypeEnumMap = {
  CardType.Equipment: 0,
  CardType.Weapon: 1,
  CardType.Action: 2,
  CardType.Back: 'Back',
  CardType.Role: 'Role',
  CardType.Character: 'Character',
};
