import 'dart:math';

import 'package:bang/cards/model/bang_card.dart';
import 'package:bang/cards/model/card_constants.dart' as Bang;
import 'package:bang/services/game_service.dart';
import 'package:flutter/material.dart';
import 'package:fluttertoast/fluttertoast.dart';
import 'package:get/get.dart';
import 'package:google_fonts/google_fonts.dart';
import 'package:screenshot/screenshot.dart';

import 'card_widget_helpers.dart';

class BangCardWidget extends StatefulWidget {
  final BangCard card;

  final double scale;
  final bool canBeFocused;
  final bool canBeDragged;
  final bool extraScaleAtHighlight;
  late final bool isInsideHand;

  final bool showBackPermanently;
  BangCardWidget({
    Key? key,
    required this.card,
    this.canBeDragged = false,
    this.canBeFocused = true,
    this.onDragSuccessCallback,
    this.handCallback,
    this.scale = 1.0,
    this.extraScaleAtHighlight = false,
    this.showBackPermanently = false,
  }) : super(key: key) {
    this.isInsideHand = handCallback != null;
  }

  final void Function()? onDragSuccessCallback;
  final void Function()? handCallback;

  @override
  _BangCardWidgetState createState() => _BangCardWidgetState();
}

class _BangCardWidgetState extends State<BangCardWidget>
    with TickerProviderStateMixin {
  bool showBack = false;
  double downSizeRatio = 0.4;
  late double height =
      CardWidgetHelpers.cardHeight * downSizeRatio * widget.scale;
  late double width =
      CardWidgetHelpers.cardWidth * downSizeRatio * widget.scale;
  final _cardFlipDuration = Duration(milliseconds: 300);
  final _cardFocusingDuration = Duration(milliseconds: 100);
  bool isElevated = false;
  double angle = 0;

  final ScreenshotController screenshotController = ScreenshotController();

  void _toggleCardFocus() {
    setState(() {
      if (isElevated) {
        if (!widget.isInsideHand) {
          height *= 2 / 3;
          width *= 2 / 3;
          if (widget.extraScaleAtHighlight) {
            height *= 5 / 6.3;
            width *= 5 / 6.3;
          }
        }
        Get.find<GameService>().highlight(-1);
        isElevated = false;
      } else {
        if (!widget.isInsideHand) {
          height *= 1.5;
          width *= 1.5;
          if (widget.extraScaleAtHighlight) {
            height *= 6.3 / 5;
            width *= 6.3 / 5;
          }
        }
        isElevated = true;
        widget.handCallback?.call();
      }
    });
  }

  void _flipCard() => setState(() {
        angle = (angle + pi) % (2 * pi);
      });

  @override
  Widget build(BuildContext context) {
    return GestureDetector(
      onLongPressStart: (_) => widget.canBeFocused ? _toggleCardFocus() : {},
      onLongPressEnd: (_) => widget.canBeFocused ? _toggleCardFocus() : {},
      //onDoubleTap: _flipCard,
      //onScaleEnd: _screenShot,
      child: TweenAnimationBuilder(
        tween: Tween<double>(begin: 0, end: angle),
        duration: _cardFlipDuration,
        builder: (BuildContext context, double val, __) {
          _computeShowBack(val);
          var card = showBack
              ? Material(
                  borderRadius: BorderRadius.circular(10 * widget.scale),
                  elevation: isElevated && !widget.isInsideHand ? 40 : 0,
                  child: AnimatedContainer(
                    height: height,
                    width: width,
                    duration: _cardFocusingDuration,
                    child: render(),
                  ),
                )
              : Material(
                  borderRadius: BorderRadius.circular(10 * widget.scale),
                  elevation: isElevated && !widget.isInsideHand ? 40 : 0,
                  child: Transform(
                    alignment: Alignment.center,
                    transform: Matrix4.identity()
                      ..rotateY(pi), // it will flip horizontally the container
                    child: AnimatedContainer(
                        height: height,
                        width: width,
                        duration: _cardFocusingDuration,
                        child: render(showBack: true)),
                  ),
                );
          return Transform(
            alignment: Alignment.center,
            transform: Matrix4.identity()
              ..setEntry(3, 2, 0.001)
              ..rotateY(val),
            child: Screenshot(
                controller: screenshotController,
                child: widget.canBeDragged
                    ? Draggable<String>(
                        onDragCompleted: widget.onDragSuccessCallback,
                        onDragEnd: (DraggableDetails details) =>
                            !details.wasAccepted
                                ? Fluttertoast.showToast(
                                    msg: 'Ez nem egy valid célpont!')
                                : {},
                        data: widget.card.toString(),
                        feedback: Image.asset('assets/icons/aim.png',
                            width: 50, height: 50),
                        childWhenDragging: ColorFiltered(
                          child: AnimatedOpacity(
                            opacity: 0.8,
                            child: card,
                            duration: Duration(milliseconds: 500),
                          ),
                          colorFilter: ColorFilter.mode(
                              Colors.red.shade100, BlendMode.modulate),
                        ),
                        child: card)
                    : card),
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

  String get _valueString =>
      CardWidgetHelpers.cardValueToString(widget.card.value);

  String get _suitString =>
      CardWidgetHelpers.cardSuitToString(widget.card.suit);

  Color get _suitColor => CardWidgetHelpers.cardSuitColor(widget.card.suit);

  Widget _buildLeftCornerData() {
    var text = Text(
      _valueString,
      style: GoogleFonts.specialElite(
        textStyle: TextStyle(
            fontSize: height / 13,
            foreground: Paint()
              ..style = PaintingStyle.stroke
              ..strokeWidth = 2.5
              ..color = Colors.white),
      ),
    );
    var textOutline = Text(_valueString,
        style: GoogleFonts.specialElite(
            textStyle: TextStyle(
                fontSize: height / 13,
                fontWeight: FontWeight.bold,
                color: Colors.black)));

    var suit = Text(
      _suitString,
      style: TextStyle(
          fontSize: height / 18,
          fontWeight: FontWeight.bold,
          foreground: Paint()
            ..style = PaintingStyle.fill
            ..strokeWidth = 3
            ..color = _suitColor),
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

  Widget render({bool showBack = false}) {
    return !showBack
        ? Stack(
            children: [
              CardWidgetHelpers.getAsset(
                  name: widget.card.name,
                  type: widget.card.type,
                  scale: widget.scale),
              Align(
                alignment: Alignment.bottomLeft,
                child: _buildCorner(),
              ),
            ],
          )
        : CardWidgetHelpers.getCardBack(widget.card.type, widget.scale);
  }

  Widget _buildCorner() => Padding(
        padding: EdgeInsets.only(left: height / 50, bottom: height / 300),
        child: Container(
          decoration: BoxDecoration(
              borderRadius: BorderRadius.circular(height / 20) * widget.scale,
              color: widget.card.borderColor),
          child: Padding(
              padding: EdgeInsets.fromLTRB(height / 35, height / 160, 0, 0),
              child: SizedBox(
                width: widget.card.value == Bang.Value.Ten
                    ? height / 6 + 1
                    : height / 8 + 1,
                height: height / 8,
                child: _buildLeftCornerData(),
              )),
        ),
      );
  void _screenShot(ScaleEndDetails details) async {
    screenshotController
        .capture(pixelRatio: MediaQuery.of(context).devicePixelRatio)
        .then((image) async {
      CardWidgetHelpers.saveAndShareImage(image);
    }).then((result) {
      Fluttertoast.showToast(msg: 'captured');
    });
  }
}
