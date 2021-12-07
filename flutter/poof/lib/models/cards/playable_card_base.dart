import 'package:bang/core/helpers/card_helpers.dart';
import 'package:flutter/material.dart';

import 'card_base.dart';

abstract class PlayableCardBase extends CardBase {
  CardValue value;
  CardSuit suit;
  Color borderColor = Colors.white;
  CardType type;
  String id;

  PlayableCardBase(
      {required String background,
      required String name,
      required this.suit,
      required this.value,
      required this.type,
      required this.id,
      bool showBack = false})
      : super(background: background, name: name, showBack: showBack);

  @override
  String toString() {
    return '$name${value.toString()}of${suit.toString()}';
  }
}
