import 'dart:math';

import 'package:bang/cards/widgets/card_widget_helpers.dart';
import 'package:flutter/material.dart';

class Hand extends StatelessWidget {
  final int handSize;

  final int indexOfFocusedCard;
  final bool isExpanded;
  final VoidCallback onDoubleTap;
  final List<Widget> cards;

  Hand({
    Key? key,
    required this.cards,
    required this.handSize,
    required this.indexOfFocusedCard,
    required this.isExpanded,
    required this.onDoubleTap,
  }) : super(key: key);

  @override
  Widget build(BuildContext context) {
    var width = MediaQuery.of(context).size.width * (isExpanded ? 1.0 : 0.3);
    var rotationStart = -10 * (handSize - 1) / 2;
    var rotationStep =
        rotationStart.abs() * 2 / ((handSize - 1) == 0 ? 1 : (handSize - 1));

    return GestureDetector(
      onDoubleTap: onDoubleTap,
      child: Container(
          child: Center(
              child: Stack(
        alignment: Alignment.center,
        fit: StackFit.passthrough,
        children: _buildCardStack(width, rotationStart, rotationStep),
      ))),
    );
  }

  List<Widget> _buildCardStack(
      double width, double rotationStart, double rotationStep) {
    var list = <Widget>[];

    for (int i = 0; i < handSize; i++) {
      if (i != indexOfFocusedCard)
        list.add(
          _rotatedCard(
            i,
            width,
            rotationStart: rotationStart,
            rotationStep: rotationStep,
          ),
        );
    }
    if (indexOfFocusedCard != -1)
      list.add(
        _rotatedCard(
          indexOfFocusedCard,
          width,
          scale: 1.3,
          extraBottom: 100,
          rotationStart: rotationStart,
          rotationStep: rotationStep,
        ),
      );

    return list;
  }

  Widget _rotatedCard(
    int index,
    double width, {
    double scale = 1.0,
    double extraBottom = 0,
    required double rotationStart,
    required double rotationStep,
  }) {
    return Positioned(
        child: Transform.scale(
            scale: (isExpanded ? 1.3 : 1) * scale,
            child: Transform.rotate(
                angle: extraBottom == 0
                    ? (pi / 180) *
                        (rotationStep * index + rotationStart) *
                        (isExpanded ? 1 : 0.1)
                    : 0,
                child: Material(
                  child: cards[index],
                  elevation: 2.0 * index,
                  animationDuration: Duration(milliseconds: 200),
                  borderRadius: BorderRadius.circular(10),
                  color: Colors.transparent,
                ))),
        left: (isExpanded ? 14 : 0.2) *
                (1.8 / (handSize + 2)) *
                (rotationStep * index + rotationStart) +
            width / 2 -
            (CardWidgetHelpers.cardWidth / 2) * 0.4,
        bottom: (isExpanded ? 1 : 0.3) *
                (rotationStep * index + rotationStart).abs() *
                -0.5 +
            (isExpanded ? 65 : 30) +
            extraBottom);
  }
}
