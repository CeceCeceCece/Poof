import 'package:bang/core/app_animations.dart';
import 'package:bang/core/app_colors.dart';
import 'package:bang/pages/splash/splash_controller.dart';
import 'package:bang/widgets/bang_background.dart';
import 'package:flutter/material.dart';
import 'package:get/get.dart';
import 'package:lottie/lottie.dart';

class SplashView extends GetView<SplashController> {
  @override
  Widget build(BuildContext context) {
    return BangBackground(
      backgroundColor: AppColors.background,
      child: Center(
        child: Lottie.asset(AppAnimations.splash,
            repeat: controller.shouldRepeat,
            frameRate: FrameRate(controller.frameRate)),
      ),
    );
  }
}
