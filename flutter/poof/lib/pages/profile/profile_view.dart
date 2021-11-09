import 'package:bang/cards/widgets/button.dart';
import 'package:bang/core/constants.dart';
import 'package:bang/pages/profile/profile_controller.dart';
import 'package:flutter/material.dart';
import 'package:get/get.dart';

class ProfileView extends GetView<ProfileController> {
  @override
  Widget build(BuildContext context) {
    return Container(
      decoration: BoxDecoration(
          image: DecorationImage(
              image: AssetImage(
                Constants.backgroundPath,
              ),
              fit: BoxFit.fitHeight)),
      child: Scaffold(
        backgroundColor: Colors.transparent,
        body: Padding(
          padding: const EdgeInsets.all(20.0),
          child: Column(
              // mainAxisAlignment: MainAxisAlignment.center,
              crossAxisAlignment: CrossAxisAlignment.center,
              children: [
                Align(
                  alignment: Alignment.topCenter,
                  child: ClipRRect(
                    child: Image.asset(
                      'assets/cards/character/rosedoolan.png',
                    ),
                    clipBehavior: Clip.hardEdge,
                    clipper: Clipper(),
                  ),
                ),
                Text('Név'),
                Row(
                  children: [Text('Valami statisztika'), Spacer(), Text('25')],
                ),
                Row(
                  children: [Text('Valami statisztika'), Spacer(), Text('25')],
                ),
                Row(
                  children: [Text('Valami statisztika'), Spacer(), Text('25')],
                ),
                Spacer(),
                BangButton(
                  text: 'Vissza',
                  onPressed: Get.back,
                ),
              ]),
        ),
      ),
    );
  }
}

class Clipper extends CustomClipper<RRect> {
  RRect getClip(Size size) {
    return RRect.fromLTRBR(55, 80, 200, 225, Radius.circular(20));
  }

  bool shouldReclip(oldClipper) {
    return false;
  }
}
