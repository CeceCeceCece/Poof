import 'dart:math';

import 'package:bang/cards/model/bang_card.dart';
import 'package:bang/cards/model/card_constants.dart';
import 'package:flutter/material.dart';
import 'package:google_fonts/google_fonts.dart';

import 'card_widget_helpers.dart';

class BangCardWidget extends StatefulWidget {
  final BangCard card;
  const BangCardWidget(
      {Key? key,
      required this.card,
      required this.onTapCallback,
      required this.handCallback})
      : super(key: key);

  final void Function() onTapCallback;
  final void Function() handCallback;

  @override
  _BangCardWidgetState createState() => _BangCardWidgetState();
}

class _BangCardWidgetState extends State<BangCardWidget>
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
          widget.handCallback();
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

  String get _valueString =>
      CardWidgetHelpers.cardValueToString(widget.card.value);

  String get _suitString =>
      CardWidgetHelpers.cardSuitToString(widget.card.suit);

  Color get _suitColor => CardWidgetHelpers.cardSuitColor(widget.card.suit);

  Widget buildLeftCornerData(double height) {
    return Padding(
      padding: EdgeInsets.only(top: height / 70),
      child: Row(
        crossAxisAlignment: CrossAxisAlignment.center,
        children: [
          Stack(
            children: [
              Text(
                _valueString,
                style: GoogleFonts.specialElite(
                  textStyle: TextStyle(
                      fontSize: height / 13,
                      foreground: Paint()
                        ..style = PaintingStyle.stroke
                        ..strokeWidth = 2.5
                        ..color = Colors.white),
                ),
              ),
              Text(_valueString,
                  style: GoogleFonts.specialElite(
                      textStyle: TextStyle(
                          fontSize: height / 13,
                          fontWeight: FontWeight.bold,
                          color: Colors.black))),
            ],
          ),
          Padding(
              padding: EdgeInsets.only(bottom: height / 55),
              child: Text(
                _suitString,
                style: TextStyle(
                    fontSize: height / 18,
                    fontWeight: FontWeight.bold,
                    foreground: Paint()
                      ..style = PaintingStyle.fill
                      ..strokeWidth = 3
                      ..color = _suitColor),
              )),
        ],
      ),
    );
  }

  Widget render(double height, bool showBack) {
    return !showBack
        ? Stack(
            children: [
              CardWidgetHelpers.getAsset(
                  name: widget.card.name, type: widget.card.type),
              Align(
                alignment: Alignment.bottomLeft,
                child: Padding(
                  padding:
                      EdgeInsets.only(left: height / 50, bottom: height / 300),
                  child: Container(
                    decoration: BoxDecoration(
                        borderRadius: BorderRadius.circular(height / 20),
                        color: widget.card.color),
                    child: Padding(
                        padding: EdgeInsets.fromLTRB(
                            height / 35, height / 160, 0, 0),
                        child: SizedBox(
                          width: widget.card.value == Value.Ten
                              ? height / 6 + 1
                              : height / 8 + 1,
                          height: height / 8,
                          child: buildLeftCornerData(height),
                        )),
                  ),
                ),
              ),
            ],
          )
        : CardWidgetHelpers.getCardBack(widget.card.type);
  }
}
