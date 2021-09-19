import 'package:bang/core/colors.dart';
import 'package:bang/pages/splash/splash_controller.dart';
import 'package:flutter/material.dart';
import 'package:get/get.dart';
import 'package:lottie/lottie.dart';

class SplashView extends GetView<SplashController> {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      backgroundColor: BangColors.background,
      body: Center(
        child: Lottie.asset('assets/lottie-boom.json',
            repeat: controller.shouldRepeat,
            frameRate: FrameRate(controller.frameRate)),
      ),
    );
  }
}
