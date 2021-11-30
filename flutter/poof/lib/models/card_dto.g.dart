// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'card_dto.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

CardDto _$CardDtoFromJson(Map<String, dynamic> json) => CardDto(
      id: json['id'] as String,
      name: json['name'] as String,
      type: $enumDecode(_$CardTypeEnumMap, json['type']),
      suite: $enumDecode(_$SuitEnumMap, json['suite']),
      value: $enumDecode(_$ValueEnumMap, json['value']),
    );

Map<String, dynamic> _$CardDtoToJson(CardDto instance) => <String, dynamic>{
      'id': instance.id,
      'name': instance.name,
      'type': _$CardTypeEnumMap[instance.type],
      'suite': _$SuitEnumMap[instance.suite],
      'value': _$ValueEnumMap[instance.value],
    };

const _$CardTypeEnumMap = {
  CardType.Equipment: 0,
  CardType.Weapon: 1,
  CardType.Action: 2,
  CardType.Back: 'Back',
  CardType.Role: 'Role',
  CardType.Character: 'Character',
};

const _$SuitEnumMap = {
  Suit.Spades: 0,
  Suit.Hearts: 1,
  Suit.Clubs: 2,
  Suit.Diamonds: 3,
};

const _$ValueEnumMap = {
  Value.Two: 0,
  Value.Three: 1,
  Value.Four: 2,
  Value.Five: 3,
  Value.Six: 4,
  Value.Seven: 5,
  Value.Eight: 6,
  Value.Nine: 7,
  Value.Ten: 8,
  Value.Jack: 9,
  Value.Queen: 10,
  Value.King: 11,
  Value.Ace: 12,
};
