import 'dart:math';
import 'package:bang/cards/model/non_playable_cards/non_playable_card_base.dart';
import 'package:bang/cards/widgets/controllers/non_playable_card_controller.dart';
import 'package:flutter/material.dart';
import 'package:get/get.dart';

class NonPlayableCardWidget extends GetView<NonPlayableCardController> {
  NonPlayableCardWidget({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return GestureDetector(
      onTap: controller.onTapCallback,
      onLongPressStart: (_) => controller.toggleCardFocus(),
      onLongPressEnd: (_) => controller.toggleCardFocus(),
      onDoubleTap: controller.flipCard,
      child: TweenAnimationBuilder(
        tween: Tween<double>(begin: 0, end: controller.angle),
        duration: controller.cardFlipDuration,
        builder: (BuildContext context, double val, __) {
          controller.computeShowBack(val);
          return Transform(
            alignment: Alignment.center,
            transform: Matrix4.identity()
              ..setEntry(3, 2, 0.001)
              ..rotateY(val),
            child: controller.showBack
                ? Material(
                    borderRadius: BorderRadius.circular(10),
                    elevation: controller.isElevated ? 40 : 0,
                    child: AnimatedContainer(
                      height: controller.height,
                      width: controller.width,
                      duration: controller.cardFocusingDuration,
                      child: controller.render(),
                    ),
                  )
                : Material(
                    borderRadius: BorderRadius.circular(10),
                    elevation: controller.isElevated ? 40 : 0,
                    child: Transform(
                      alignment: Alignment.center,
                      transform: Matrix4.identity()
                        ..rotateY(
                            pi), // it will flip horizontally the container
                      child: AnimatedContainer(
                          height: controller.height,
                          width: controller.width,
                          duration: controller.cardFocusingDuration,
                          child: controller.render(showBack: true)),
                    ),
                  ),
          );
        },
      ),
    );
  }
}
