import 'package:flutter/material.dart';

class BangColors {
  static const background = Color(0xffFFEBC9);
  static const equipmentCardColor = Color(0xff6b8cc8);
  static const actionCardColor = Color(0xffd1aa63);
  static const buttonColor = Colors.brown;
  static const buttonShadowColor = Color(0xff753422);
  static const darkBrown = Color(0xff4E3B42);
  static const buttonGradientColors = [
    Color(0xffD79771),
    Colors.brown,
  ];
  static const disabledButtonGradientColors = [
    Color(0x88D79771),
    Color(0x88795548),
  ];
  static const buttonGradient = LinearGradient(
      begin: Alignment.topCenter,
      end: Alignment.bottomCenter,
      colors: buttonGradientColors);

  static const disabledButtonGradient = LinearGradient(
      begin: Alignment.topCenter,
      end: Alignment.bottomCenter,
      colors: disabledButtonGradientColors);

  static const hintColor = Color(0x88795548);
}
