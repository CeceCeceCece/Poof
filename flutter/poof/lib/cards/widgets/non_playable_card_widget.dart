import 'dart:math';
import 'package:bang/cards/model/non_playable_cards/non_playable_card_base.dart';
import 'package:flutter/material.dart';

import 'card_widget_helpers.dart';

class NonPlayableCardWidget extends StatefulWidget {
  final NonPlayableCardBase card;
  const NonPlayableCardWidget(
      {Key? key, required this.card, required this.onTapCallback})
      : super(key: key);

  final void Function() onTapCallback;

  @override
  _BangCardWidgetState createState() => _BangCardWidgetState();
}

class _BangCardWidgetState extends State<NonPlayableCardWidget>
    with TickerProviderStateMixin {
  bool showBack = false;
  double downSizeRatio = 0.4;
  late double height = CardWidgetHelpers.cardHeight * downSizeRatio;
  late double width = CardWidgetHelpers.cardWidth * downSizeRatio;

  late final AnimationController _controller;
  bool isElevated = false;
  double angle = 0;

  void _flip() {
    setState(() {
      angle = (angle + pi) % (2 * pi);
    });
  }

  @override
  void initState() {
    super.initState();
    height = CardWidgetHelpers.cardHeight * downSizeRatio;
    width = CardWidgetHelpers.cardWidth * downSizeRatio;
    _controller = AnimationController(
        vsync: this, duration: const Duration(milliseconds: 200));
  }

  @override
  void dispose() {
    _controller.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    return GestureDetector(
        onTap: () {
          widget.onTapCallback();
        },
        onLongPressStart: (details) {
          setState(() {
            height *= 1.5;
            width *= 1.5;
            isElevated = true;
          });
        },
        onLongPressEnd: (details) {
          setState(() {
            height *= 2 / 3;
            width *= 2 / 3;
            isElevated = false;
          });
        },
        onDoubleTap: _flip,
        child: TweenAnimationBuilder(
            tween: Tween<double>(begin: 0, end: angle),
            duration: Duration(milliseconds: 300),
            builder: (BuildContext context, double val, __) {
              //here we will change the isBack val so we can change the content of the card
              if (val >= (pi / 2)) {
                showBack = false;
              } else {
                showBack = true;
              }
              return (Transform(
                //let's make the card flip by it's center
                alignment: Alignment.center,
                transform: Matrix4.identity()
                  ..setEntry(3, 2, 0.001)
                  ..rotateY(val),
                child: showBack
                    ? Material(
                        borderRadius: BorderRadius.circular(10),
                        elevation: isElevated ? 40 : 0,
                        child: AnimatedContainer(
                            height: height,
                            width: width,
                            duration: Duration(milliseconds: 100),
                            child: render(height, false)))
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
                              duration: Duration(milliseconds: 100),
                              child: render(height, true)),
                        ),
                      ),
              ));
            }));
  }

  Widget render(double height, bool showBack) {
    return !showBack
        ? CardWidgetHelpers.getAsset(
            name: widget.card.name, type: widget.card.type)
        : CardWidgetHelpers.getCardBack(widget.card.type);
  }
}
