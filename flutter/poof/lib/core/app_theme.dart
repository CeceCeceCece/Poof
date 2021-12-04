import 'package:bang/core/app_colors.dart';
import 'package:flutter/material.dart';
import 'package:google_fonts/google_fonts.dart';

abstract class AppTheme {
  static ThemeData get basic => ThemeData(
        primarySwatch: Colors.brown,
        primaryColor: AppColors.buttonGradientColors.first,
        backgroundColor: AppColors.background,
        scaffoldBackgroundColor: AppColors.background,
        elevatedButtonTheme: ElevatedButtonThemeData(
          style: ButtonStyle(
            textStyle: MaterialStateProperty.all<TextStyle>(
                GoogleFonts.graduate(
                    textStyle: TextStyle(
                        fontSize: 15,
                        fontWeight: FontWeight.bold,
                        color: AppColors.buttonShadowColor))),
            foregroundColor:
                MaterialStateProperty.all<Color>(AppColors.background),
          ),
        ),
      );

  static TextStyle get bigBrown => GoogleFonts.graduate(
        textStyle: TextStyle(
            fontSize: 25, fontWeight: FontWeight.bold, color: Colors.brown),
      );

  static TextStyle get smallerBrown => GoogleFonts.graduate(
        textStyle: TextStyle(
            fontSize: 15, fontWeight: FontWeight.bold, color: Colors.brown),
      );

  static BoxDecoration get whiteBackgroundAndBorder => BoxDecoration(
      color: Colors.white38,
      border: Border.all(color: Colors.white, width: 1.5),
      borderRadius: BorderRadius.circular(30));

  static TextStyle get slogan => GoogleFonts.graduate(
        textStyle: TextStyle(
          fontSize: 22,
          fontWeight: FontWeight.bold,
          color: AppColors.darkBrown,
        ),
      );
}
