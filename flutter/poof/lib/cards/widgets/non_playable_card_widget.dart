import 'dart:io';
import 'dart:math';
import 'package:bang/cards/model/non_playable_cards/non_playable_card_base.dart';
import 'package:flutter/material.dart';
import 'package:fluttertoast/fluttertoast.dart';
import 'package:image_gallery_saver/image_gallery_saver.dart';
import 'package:path_provider/path_provider.dart';
import 'package:screenshot/screenshot.dart';
import 'package:share/share.dart';

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
  final _cardFlipDuration = Duration(milliseconds: 300);
  final _cardFocusingDuration = Duration(milliseconds: 100);
  bool isElevated = false;
  double angle = 0;

  final screenShotController = ScreenshotController();

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

  void _screenShot(ScaleEndDetails details) async {
    screenShotController
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

  @override
  Widget build(BuildContext context) {
    return GestureDetector(
      onTap: widget.onTapCallback,
      onLongPressStart: (_) => _toggleCardFocus(),
      onLongPressEnd: (_) => _toggleCardFocus(),
      onScaleEnd: _screenShot,
      onDoubleTap: _flipCard,
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
              controller: screenShotController,
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

  Widget render({bool showBack = false}) {
    return !showBack
        ? CardWidgetHelpers.getAsset(
            name: widget.card.name, type: widget.card.type)
        : CardWidgetHelpers.getCardBack(widget.card.type);
  }
}
