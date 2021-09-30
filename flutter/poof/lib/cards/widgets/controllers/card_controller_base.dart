import 'dart:math';

import 'package:flutter/material.dart';
import 'package:get/get.dart';

import '../card_widget_helpers.dart';

abstract class CardControllerBase extends GetxController
    with SingleGetTickerProviderMixin {
  late final void Function() onTapCallback;
  late final void Function() handCallback;

  bool showBack = false;
  double downSizeRatio = 0.4;
  late double height = CardWidgetHelpers.cardHeight * downSizeRatio;
  late double width = CardWidgetHelpers.cardWidth * downSizeRatio;
  final cardFlipDuration = Duration(milliseconds: 300);
  final cardFocusingDuration = Duration(milliseconds: 100);
  bool isElevated = false;
  double angle = 0;

  void computeShowBack(double val) {
    if (val >= (pi / 2)) {
      showBack = false;
    } else {
      showBack = true;
    }
  }

  void toggleCardFocus() {
    if (isElevated) {
      height *= 2 / 3;
      width *= 2 / 3;
      isElevated = false;
    } else {
      height *= 1.5;
      width *= 1.5;
      isElevated = true;
    }
  }

  void flipCard() => angle = (angle + pi) % (2 * pi);

  Widget render({bool showBack = false});
}
