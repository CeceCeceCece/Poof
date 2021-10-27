import 'dart:math';

import 'package:bang/cards/widgets/card_widget_helpers.dart';
import 'package:bang/services/game_service.dart';
import 'package:flutter/material.dart';
import 'package:get/get.dart';

class Hand extends StatefulWidget {
  const Hand({Key? key}) : super(key: key);

  @override
  _HandState createState() => _HandState();
}

class _HandState extends State<Hand> {
  final controller = Get.find<GameService>();
  late int handSize;
  late double rotationStart;
  late double rotationStep;
  int? indexOfFocusedCard;

  @override
  void initState() {
    super.initState();
  }

  @override
  Widget build(BuildContext context) {
    var width = MediaQuery.of(context).size.width *
        (controller.expandedHandView() ? 1.0 : 0.3);
    return GestureDetector(
      onDoubleTap: () {
        controller.expandedHandView.value = !controller.expandedHandView();
        controller.expandedEquipmentView.value = false;
      },
      child: Container(
        child: Center(
          child: Obx(() {
            handSize = controller.handWidgets.length;
            rotationStart = -10 * (handSize - 1) / 2;
            rotationStep = rotationStart.abs() *
                2 /
                ((handSize - 1) == 0 ? 1 : (handSize - 1));

            var highlighted = controller.highlightedIndex();
            return Stack(
                alignment: Alignment.center,
                fit: StackFit.passthrough,
                children: _buildCardStack(handSize, highlighted, width));
          }),
        ),
      ),
    );
  }

  List<Widget> _buildCardStack(int handSize, int highlighted, double width) {
    var list = <Widget>[];

    for (int i = 0; i < handSize; i++) {
      list.add(_rotatedCard(
        i,
        width,
      ));
    }
    if (highlighted != -1)
      list.add(_rotatedCard(highlighted, width, scale: 1.3, extraBottom: 100));

    return list;
  }

  Widget _rotatedCard(int i, double width,
      {double scale = 1.0, double extraBottom = 0}) {
    var isExpanded = controller.expandedHandView();
    return Positioned(
        child: Transform.scale(
            scale: (isExpanded ? 1.3 : 1) * scale,
            child: Transform.rotate(
                angle: extraBottom == 0
                    ? (pi / 180) *
                        (rotationStep * i + rotationStart) *
                        (isExpanded ? 1 : 0.1)
                    : 0,
                child: Material(
                  child: controller.handWidgets[i],
                  elevation: 2.0 * i,
                  animationDuration: Duration(milliseconds: 200),
                  borderRadius: BorderRadius.circular(10),
                  color: Colors.transparent,
                ))),
        left: (isExpanded ? 14 : 0.2) *
                (1.8 / (handSize + 2)) *
                (rotationStep * i + rotationStart) +
            width / 2 -
            (CardWidgetHelpers.cardWidth / 2) * 0.4,
        bottom: (isExpanded ? 1 : 0.3) *
                (rotationStep * i + rotationStart).abs() *
                -0.5 +
            (isExpanded ? 65 : 30) +
            extraBottom);
  }
}
