import 'package:flutter/material.dart';
import 'card_constants.dart';
import 'card_base.dart';

abstract class BangCard extends CardBase {
  Value value;
  Suit suit;
  Color borderColor = Colors.white;
  CardType type;

  BangCard(
      {required String background,
      required String name,
      required this.suit,
      required this.value,
      required this.type,
      bool showBack = false})
      : super(background: background, name: name, showBack: showBack);

  @override
  String toString() {
    return '$name${value.toString()}of${suit.toString()}';
  }
}