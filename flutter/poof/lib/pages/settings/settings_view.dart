import 'package:bang/core/app_colors.dart';
import 'package:bang/core/app_theme.dart';
import 'package:bang/core/lang/app_strings.dart';
import 'package:bang/pages/settings/settings_controller.dart';
import 'package:bang/widgets/bang_background.dart';
import 'package:bang/widgets/bang_button.dart';
import 'package:bang/widgets/bang_logo.dart';
import 'package:flutter/material.dart';
import 'package:get/get.dart';

class SettingsView extends GetView<SettingsController> {
  @override
  Widget build(BuildContext context) {
    return BangBackground(
      child: Obx(
        () => Column(
          crossAxisAlignment: CrossAxisAlignment.center,
          children: [
            BangLogo(),
            Padding(
              padding: EdgeInsets.only(left: 50, right: 50),
              child: Container(
                height: 60,
                width: 300,
                decoration: AppTheme.whiteBackgroundAndBorder,
                child: Padding(
                  padding: const EdgeInsets.all(8.0),
                  child: Center(
                    child: Text(
                      AppStrings.settings.tr,
                      style: AppTheme.bigBrown,
                      textAlign: TextAlign.center,
                    ),
                  ),
                ),
              ),
            ),
            Spacer(),
            Padding(
              padding: EdgeInsets.only(left: 25, right: 25),
              child: Container(
                decoration: AppTheme.whiteBackgroundAndBorder,
                child: Padding(
                  padding: const EdgeInsets.all(15.0),
                  child: Column(
                    children: [
                      Padding(
                        padding: const EdgeInsets.fromLTRB(25, 0, 25, 0),
                        child: Row(
                          children: [
                            Text(AppStrings.music.tr,
                                style: AppTheme.smallerBrown),
                            Spacer(),
                            Switch(
                              activeColor: AppColors.buttonColor,
                              value: controller.isMusicPlayingEnabled(),
                              onChanged: (val) =>
                                  controller.changeMusicSettings(val),
                            ),
                          ],
                        ),
                      ),
                      Padding(
                        padding: const EdgeInsets.fromLTRB(25, 0, 25, 0),
                        child: Row(
                          children: [
                            Text(
                              AppStrings.sfx.tr,
                              style: AppTheme.smallerBrown,
                              textAlign: TextAlign.center,
                            ),
                            Spacer(),
                            Switch(
                              activeColor: AppColors.buttonColor,
                              value: controller.isSFXEnabled(),
                              onChanged: (val) =>
                                  controller.changeSFXSettings(val),
                            ),
                          ],
                        ),
                      ),
                    ],
                  ),
                ),
              ),
            ),
            Spacer(),
            Padding(
              padding: const EdgeInsets.only(bottom: 20),
              child: BangButton(
                height: 40,
                width: 90,
                text: AppStrings.back.tr,
                onPressed: Get.back,
              ),
            ),
          ],
        ),
      ),
    );
  }
}
