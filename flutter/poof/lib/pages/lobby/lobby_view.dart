import 'package:bang/cards/widgets/button.dart';
import 'package:bang/core/colors.dart';
import 'package:bang/pages/lobby/lobby_controller.dart';
import 'package:flutter/material.dart';
import 'package:get/get.dart';

class LobbyView extends GetView<LobbyController> {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
        backgroundColor: BangColors.background,
        body: Center(
          child: Column(
            children: [
              Text('LOBBY: ${controller.roomID}'),
              BangButton(
                text: 'Enter game',
                onPressed: controller.join,
              ),
            ],
          ),
        ));
  }
}
