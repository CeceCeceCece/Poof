import 'package:bang/core/colors.dart';
import 'package:bang/pages/lobby_creation/lobby_creation_controller.dart';
import 'package:flutter/material.dart';
import 'package:get/get.dart';

class LobbyCreationView extends GetView<LobbyCreationController> {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
        backgroundColor: BangColors.background,
        body: Center(
          child: Text('LOBBY CREATION'),
        ));
  }
}
