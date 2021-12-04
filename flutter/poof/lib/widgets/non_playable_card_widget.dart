import 'dart:math';

import 'package:bang/cards/model/non_playable_cards/non_playable_card_base.dart';
import 'package:bang/core/helpers/card_helpers.dart';
import 'package:flutter/material.dart';

class NonPlayableCard extends StatefulWidget {
  final NonPlayableCardBase card;

  final double scale;
  final double highlightMultiplier;
  final bool canBeFocused;

  final bool showBackPermanently;
  const NonPlayableCard({
    Key? key,
    required this.card,
    this.canBeFocused = true,
    this.onTapCallback,
    this.scale = 1.0,
    this.highlightMultiplier = 1.0,
    this.showBackPermanently = false,
  }) : super(key: key);

  final void Function()? onTapCallback;

  @override
  _BangCardWidgetState createState() => _BangCardWidgetState();
}

class _BangCardWidgetState extends State<NonPlayableCard>
    with TickerProviderStateMixin {
  bool showBack = false;
  double downSizeRatio = 0.4;
  late double height = CardHelpers.cardHeight * downSizeRatio * widget.scale;
  late double width = CardHelpers.cardWidth * downSizeRatio * widget.scale;
  final _cardFlipDuration = Duration(milliseconds: 300);
  final _cardFocusingDuration = Duration(milliseconds: 100);
  bool isElevated = false;
  double angle = 0;

  void _toggleCardFocus() => setState(() {
        if (isElevated) {
          height *= 2 / 3 / widget.highlightMultiplier;
          width *= 2 / 3 / widget.highlightMultiplier;
          isElevated = false;
        } else {
          height *= 1.5 * widget.highlightMultiplier;
          width *= 1.5 * widget.highlightMultiplier;
          isElevated = true;
        }
      });

  void _flipCard() => setState(() {
        angle = (angle + pi) % (2 * pi);
      });

  @override
  Widget build(BuildContext context) {
    return GestureDetector(
      onTap: widget.onTapCallback,
      onLongPressStart: (_) => widget.canBeFocused ? _toggleCardFocus() : {},
      onLongPressEnd: (_) => widget.canBeFocused ? _toggleCardFocus() : {},
      //onDoubleTap: _flipCard,
      child: TweenAnimationBuilder(
        tween: Tween<double>(begin: 0, end: angle),
        duration: _cardFlipDuration,
        builder: (BuildContext context, double val, __) {
          _computeShowBack(val);
          return Transform(
            alignment: Alignment.center,
            transform: Matrix4.identity()
              ..setEntry(3, 2, 0.001)
              ..rotateY(val),
            child: showBack
                ? Material(
                    borderRadius: BorderRadius.circular(10 * widget.scale),
                    elevation: isElevated ? 40 : 0,
                    child: AnimatedContainer(
                      height: height,
                      width: width,
                      duration: _cardFocusingDuration,
                      child: render(),
                    ),
                  )
                : Material(
                    borderRadius: BorderRadius.circular(10),
                    elevation: isElevated ? 40 : 0,
                    child: Transform(
                      alignment: Alignment.center,
                      transform: Matrix4.identity()
                        ..rotateY(
                            pi), // it will flip horizontally the container
                      child: AnimatedContainer(
                          height: height,
                          width: width,
                          duration: _cardFocusingDuration,
                          child: render(showBack: true)),
                    ),
                  ),
          );
        },
      ),
    );
  }

  void _computeShowBack(double val) {
    if (widget.showBackPermanently) {
      showBack = false;
      return;
    }
    if (val >= (pi / 2)) {
      showBack = false;
    } else {
      showBack = true;
    }
  }

  Widget render({bool showBack = false}) {
    return !showBack
        ? CardHelpers.getAsset(
            name: widget.card.name, type: widget.card.type, scale: widget.scale)
        : CardHelpers.getCardBack(widget.card.type, widget.scale);
  }
}
