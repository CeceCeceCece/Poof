import 'dart:math';

import 'package:bang/cards/model/playable_card_base.dart';
import 'package:bang/cards/model/playable_cards/action_card.dart';
import 'package:bang/core/app_constants.dart';
import 'package:bang/core/helpers/card_helpers.dart';
import 'package:bang/core/lang/app_strings.dart';
import 'package:flutter/material.dart';
import 'package:fluttertoast/fluttertoast.dart';
import 'package:get/get_utils/src/extensions/internacionalization.dart';
import 'package:google_fonts/google_fonts.dart';

class PlayableCard extends StatefulWidget {
  final PlayableCardBase card;

  final double scale;
  final bool canBeFocused;
  final double extraElevation;
  final bool canBeDragged;
  final double highlightMultiplier;
  late final bool isInsideHand;

  final bool showBackPermanently;

  PlayableCard({
    Key? key,
    required this.card,
    this.canBeDragged = false,
    this.canBeFocused = true,
    this.onDragSuccessCallback,
    this.handCallback,
    this.extraElevation = 0,
    this.scale = 1.0,
    this.highlightMultiplier = 1.0,
    this.showBackPermanently = false,
    this.handCallbackInverse,
  }) : super(key: key) {
    this.isInsideHand = handCallback != null;
  }

  final VoidCallback? onDragSuccessCallback;
  final VoidCallback? handCallback;
  final VoidCallback? handCallbackInverse;

  @override
  _PlayableCardState createState() => _PlayableCardState();

  static back({bool isDrawPile = false, double extraElevation = 3}) =>
      PlayableCard(
        scale: 0.5,
        showBackPermanently: true,
        canBeFocused: false,
        extraElevation: extraElevation,
        card: ActionCard(
          background: 'dummy',
          name: 'dummy',
          suit: CardSuit.Clubs,
          value: CardValue.Nine,
          type: CardType.Action,
          range: 0,
        ),
      );
}

class _PlayableCardState extends State<PlayableCard>
    with TickerProviderStateMixin {
  bool showBack = false;
  double downSizeRatio = 0.4;
  late double height = CardHelpers.cardHeight * downSizeRatio * widget.scale;
  late double width = CardHelpers.cardWidth * downSizeRatio * widget.scale;
  final _cardFlipDuration = Duration(milliseconds: 300);
  final _cardFocusingDuration = Duration(milliseconds: 100);
  bool isElevated = false;
  double angle = 0;

  void _toggleCardFocus() {
    setState(() {
      if (isElevated) {
        if (!widget.isInsideHand) {
          height *= 2 / 3 / widget.highlightMultiplier;
          width *= 2 / 3 / widget.highlightMultiplier;
        }

        isElevated = false;
        widget.handCallbackInverse?.call();
      } else {
        if (!widget.isInsideHand) {
          height *= 1.5 * widget.highlightMultiplier;
          width *= 1.5 * widget.highlightMultiplier;
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
                  elevation: isElevated && !widget.isInsideHand
                      ? 40
                      : widget.extraElevation,
                  child: Transform(
                    alignment: Alignment.center,
                    transform: Matrix4.identity()..rotateY(pi),
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
            child: widget.canBeDragged
                ? Draggable<String>(
                    onDragCompleted: widget.onDragSuccessCallback,
                    onDragEnd: (DraggableDetails details) =>
                        !details.wasAccepted
                            ? Fluttertoast.showToast(
                                msg: AppStrings.not_valid_target.tr)
                            : {},
                    data: widget.card.toString(),
                    feedback: Image.asset(AppAssetPaths.crossHairPath,
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
                : card,
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

  String get _valueString => CardHelpers.cardValueToString(widget.card.value);

  String get _suitString => CardHelpers.cardSuitToString(widget.card.suit);

  Color get _suitColor => CardHelpers.cardSuitColor(widget.card.suit);

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
              CardHelpers.getAsset(
                  name: widget.card.name,
                  type: widget.card.type,
                  scale: widget.scale),
              Align(
                alignment: Alignment.bottomLeft,
                child: _buildCorner(),
              ),
            ],
          )
        : CardHelpers.getCardBack(widget.card.type, widget.scale);
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
                width: widget.card.value == CardValue.Ten
                    ? height / 6 + 1
                    : height / 8 + 1,
                height: height / 8,
                child: _buildLeftCornerData(),
              )),
        ),
      );
}
