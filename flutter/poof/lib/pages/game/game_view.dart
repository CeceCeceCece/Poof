import 'package:bang/pages/game/game_controller.dart';
import 'package:flutter/material.dart';
import 'package:get/get.dart';

class GameView extends GetView<GameController> {
  @override
  Widget build(BuildContext context) {
    return WillPopScope(
      onWillPop: controller.showBackPopupForResult,
      child: Scaffold(
        body: Center(
          child: Obx(
            () => Stack(
              alignment: Alignment.center,
              children: [
                _buildLayout(),
                Column(
                  mainAxisAlignment: MainAxisAlignment.end,
                  children: [
                    ElevatedButton(
                        onPressed: controller.randomizePlayerNumber,
                        child: Icon(Icons.replay_outlined)),
                  ],
                ),
              ],
            ),
          ),
        ),
      ),
    );
  }

  Widget _ph([Color? color]) => Container(
        height: 20,
        width: 20,
        decoration: BoxDecoration(color: color ?? Colors.red),
      );

  Widget _buildLayout() {
    switch (controller.playerNumber()) {
      case 4:
        return Column(
          mainAxisAlignment: MainAxisAlignment.spaceAround,
          children: [
            Center(child: _ph()),
            Row(
              children: [_ph(), _ph()],
              mainAxisAlignment: MainAxisAlignment.spaceBetween,
            ),
            Center(child: _ph(Colors.blue)),
          ],
        );
      case 5:
        return Column(
          mainAxisAlignment: MainAxisAlignment.spaceAround,
          children: [
            Row(
              children: [_ph(), _ph()],
              mainAxisAlignment: MainAxisAlignment.spaceAround,
            ),
            Row(
              children: [_ph(), _ph()],
              mainAxisAlignment: MainAxisAlignment.spaceBetween,
            ),
            Center(child: _ph(Colors.blue)),
          ],
        );
      case 6:
        return Column(
            mainAxisAlignment: MainAxisAlignment.spaceAround,
            children: [
              Center(child: _ph()),
              Row(
                children: [_ph(), _ph()],
                mainAxisAlignment: MainAxisAlignment.spaceBetween,
              ),
              Row(
                children: [_ph(), _ph()],
                mainAxisAlignment: MainAxisAlignment.spaceBetween,
              ),
              Center(child: _ph(Colors.blue)),
            ]);
      case 7:
        return Column(
            mainAxisAlignment: MainAxisAlignment.spaceAround,
            children: [
              Row(
                children: [_ph(), _ph()],
                mainAxisAlignment: MainAxisAlignment.spaceAround,
              ),
              Row(
                children: [_ph(), _ph()],
                mainAxisAlignment: MainAxisAlignment.spaceBetween,
              ),
              Row(
                children: [_ph(), _ph()],
                mainAxisAlignment: MainAxisAlignment.spaceBetween,
              ),
              Center(child: _ph(Colors.blue)),
            ]);
      default:
        return Text('default');
    }
  }
}
