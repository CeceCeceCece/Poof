// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'card_dto.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

CardDto _$CardDtoFromJson(Map<String, dynamic> json) => CardDto(
      id: json['id'] as String,
      name: json['name'] as String,
      type: $enumDecode(_$CardTypeEnumMap, json['type']),
      suite: $enumDecode(_$CardSuitEnumMap, json['suite']),
      value: $enumDecode(_$CardValueEnumMap, json['value']),
    );

Map<String, dynamic> _$CardDtoToJson(CardDto instance) => <String, dynamic>{
      'id': instance.id,
      'name': instance.name,
      'type': _$CardTypeEnumMap[instance.type],
      'suite': _$CardSuitEnumMap[instance.suite],
      'value': _$CardValueEnumMap[instance.value],
    };

const _$CardTypeEnumMap = {
  CardType.Equipment: 0,
  CardType.Weapon: 1,
  CardType.Action: 2,
  CardType.Back: 'Back',
  CardType.Role: 'Role',
  CardType.Character: 'Character',
};

const _$CardSuitEnumMap = {
  CardSuit.Spades: 0,
  CardSuit.Hearts: 1,
  CardSuit.Clubs: 2,
  CardSuit.Diamonds: 3,
};

const _$CardValueEnumMap = {
  CardValue.Two: 0,
  CardValue.Three: 1,
  CardValue.Four: 2,
  CardValue.Five: 3,
  CardValue.Six: 4,
  CardValue.Seven: 5,
  CardValue.Eight: 6,
  CardValue.Nine: 7,
  CardValue.Ten: 8,
  CardValue.Jack: 9,
  CardValue.Queen: 10,
  CardValue.King: 11,
  CardValue.Ace: 12,
};
