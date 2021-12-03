import 'package:bang/core/colors.dart';
import 'package:flutter/material.dart';
import 'package:google_fonts/google_fonts.dart';

abstract class AppTheme {
  static ThemeData get basic => ThemeData(
        primarySwatch: Colors.brown,
        primaryColor: BangColors.buttonGradientColors.first,
        backgroundColor: BangColors.background,
        scaffoldBackgroundColor: BangColors.background,
        elevatedButtonTheme: ElevatedButtonThemeData(
          style: ButtonStyle(
            textStyle: MaterialStateProperty.all<TextStyle>(GoogleFonts.graduate(
                textStyle: TextStyle(fontSize: 15, fontWeight: FontWeight.bold, color: BangColors.buttonShadowColor))),
            foregroundColor: MaterialStateProperty.all<Color>(BangColors.background),
          ),
        ),
      );
}
