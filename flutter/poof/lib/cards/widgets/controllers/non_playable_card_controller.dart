import 'package:bang/cards/model/non_playable_cards/non_playable_card_base.dart';
import 'package:bang/cards/widgets/controllers/card_controller_base.dart';
import 'package:flutter/material.dart';

import '../card_widget_helpers.dart';

class NonPlayableCardController extends CardControllerBase {
  late final NonPlayableCardBase card;

  @override
  Widget render({bool showBack = false}) {
    return !showBack
        ? CardWidgetHelpers.getAsset(name: card.name, type: card.type)
        : CardWidgetHelpers.getCardBack(card.type);
  }
}
