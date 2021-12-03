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
}
