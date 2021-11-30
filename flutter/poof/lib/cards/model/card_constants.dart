import 'package:json_annotation/json_annotation.dart';

enum Value {
  @JsonValue(0)
  Two,
  @JsonValue(1)
  Three,
  @JsonValue(2)
  Four,
  @JsonValue(3)
  Five,
  @JsonValue(4)
  Six,
  @JsonValue(5)
  Seven,
  @JsonValue(6)
  Eight,
  @JsonValue(7)
  Nine,
  @JsonValue(8)
  Ten,
  @JsonValue(9)
  Jack,
  @JsonValue(10)
  Queen,
  @JsonValue(11)
  King,
  @JsonValue(12)
  Ace,
}

enum Suit {
  @JsonValue(0)
  Spades,
  @JsonValue(1)
  Hearts,
  @JsonValue(2)
  Clubs,
  @JsonValue(3)
  Diamonds,
}

enum CardType {
  @JsonValue(0)
  Equipment,
  @JsonValue(1)
  Weapon,
  @JsonValue(2)
  Action,
  Back,
  Role,
  Character,
}
