import 'dart:io';
import 'dart:math';
import 'package:share_plus/share_plus.dart';

import 'package:bang/cards/model/bang_card.dart';
import 'package:bang/cards/model/card_constants.dart';
import 'package:flutter/material.dart';
import 'package:fluttertoast/fluttertoast.dart';
import 'package:google_fonts/google_fonts.dart';
import 'package:image_gallery_saver/image_gallery_saver.dart';
import 'package:path_provider/path_provider.dart';
import 'package:screenshot/screenshot.dart';

import 'card_widget_helpers.dart';

class BangCardWidget extends StatefulWidget {
  final BangCard card;
  BangCardWidget(
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
  final _cardFlipDuration = Duration(milliseconds: 300);
  final _cardFocusingDuration = Duration(milliseconds: 100);
  bool isElevated = false;
  double angle = 0;

  final ScreenshotController screenshotController = ScreenshotController();

  void _toggleCardFocus() => setState(() {
        if (isElevated) {
          height *= 2 / 3;
          width *= 2 / 3;
          isElevated = false;
        } else {
          height *= 1.5;
          width *= 1.5;
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
      onLongPressStart: (_) => _toggleCardFocus(),
      onLongPressEnd: (_) => _toggleCardFocus(),
      onDoubleTap: _flipCard,
      onScaleEnd: _screenShot,
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
            child: Screenshot(
              controller: screenshotController,
              child: showBack
                  ? Material(
                      borderRadius: BorderRadius.circular(10),
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
            ),
          );
        },
      ),
    );
  }

  void _computeShowBack(double val) {
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
                  name: widget.card.name, type: widget.card.type),
              Align(
                alignment: Alignment.bottomLeft,
                child: _buildCorner(),
              ),
            ],
          )
        : CardWidgetHelpers.getCardBack(widget.card.type);
  }

  Widget _buildCorner() => Padding(
        padding: EdgeInsets.only(left: height / 50, bottom: height / 300),
        child: Container(
          decoration: BoxDecoration(
              borderRadius: BorderRadius.circular(height / 20),
              color: widget.card.borderColor),
          child: Padding(
              padding: EdgeInsets.fromLTRB(height / 35, height / 160, 0, 0),
              child: SizedBox(
                width: widget.card.value == Value.Ten
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
      if (image != null) {
        final directory = await getApplicationDocumentsDirectory();
        final imagePath =
            await File('${directory.path}/bang_card.png').create();
        await imagePath.writeAsBytes(image);
        await Share.shareFiles([imagePath.path]);
        ImageGallerySaver.saveImage(image, quality: 100, name: 'bang_card');
      }
    }).then((result) {
      Fluttertoast.showToast(msg: 'captured');
    });
  }
}
