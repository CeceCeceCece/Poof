import 'package:bang/cards/model/bang_card.dart';
import 'package:bang/cards/model/card_constants.dart';
import 'package:bang/cards/widgets/controllers/card_controller_base.dart';
import 'package:flutter/material.dart';
import 'package:google_fonts/google_fonts.dart';

import '../card_widget_helpers.dart';

class BangCardController extends CardControllerBase {
  late final BangCard card;

  late final void Function() handCallback;

  String get valueString => CardWidgetHelpers.cardValueToString(card.value);

  String get suitString => CardWidgetHelpers.cardSuitToString(card.suit);

  Color get suitColor => CardWidgetHelpers.cardSuitColor(card.suit);

  @override
  Widget render({bool showBack = false}) {
    return !showBack
        ? Stack(
            children: [
              CardWidgetHelpers.getAsset(name: card.name, type: card.type),
              Align(
                alignment: Alignment.bottomLeft,
                child: _buildCorner(),
              ),
            ],
          )
        : CardWidgetHelpers.getCardBack(card.type);
  }

  Widget _buildLeftCornerData() {
    var text = Text(
      valueString,
      style: GoogleFonts.specialElite(
        textStyle: TextStyle(
            fontSize: height / 13,
            foreground: Paint()
              ..style = PaintingStyle.stroke
              ..strokeWidth = 2.5
              ..color = Colors.white),
      ),
    );
    var textOutline = Text(valueString,
        style: GoogleFonts.specialElite(
            textStyle: TextStyle(
                fontSize: height / 13,
                fontWeight: FontWeight.bold,
                color: Colors.black)));

    var suit = Text(
      suitString,
      style: TextStyle(
          fontSize: height / 18,
          fontWeight: FontWeight.bold,
          foreground: Paint()
            ..style = PaintingStyle.fill
            ..strokeWidth = 3
            ..color = suitColor),
    );

    return Padding(
      padding: EdgeInsets.only(top: height / 70),
      child: Row(
        crossAxisAlignment: CrossAxisAlignment.center,
        children: [
          Stack(
            children: [text, textOutline],
          ),
          Padding(
            padding: EdgeInsets.only(bottom: height / 55),
            child: suit,
          )
        ],
      ),
    );
  }

  Widget _buildCorner() {
    return Padding(
      padding: EdgeInsets.only(left: height / 50, bottom: height / 300),
      child: Container(
        decoration: BoxDecoration(
            borderRadius: BorderRadius.circular(height / 20),
            color: card.borderColor),
        child: Padding(
            padding: EdgeInsets.fromLTRB(height / 35, height / 160, 0, 0),
            child: SizedBox(
              width: card.value == Value.Ten ? height / 6 + 1 : height / 8 + 1,
              height: height / 8,
              child: _buildLeftCornerData(),
            )),
      ),
    );
  }
}
