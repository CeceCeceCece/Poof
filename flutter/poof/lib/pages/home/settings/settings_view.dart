import 'package:bang/cards/widgets/button.dart';
import 'package:bang/core/colors.dart';
import 'package:bang/core/constants.dart';
import 'package:bang/pages/home/settings/settings_controller.dart';
import 'package:flutter/material.dart';
import 'package:get/get.dart';

class SettingsView extends GetView<SettingsController> {
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
        body: Obx(
          () => Column(
            mainAxisAlignment: MainAxisAlignment.center,
            crossAxisAlignment: CrossAxisAlignment.center,
            children: [
              Spacer(),
              Padding(
                padding: const EdgeInsets.fromLTRB(100, 0, 100, 0),
                child: Row(
                  children: [
                    Text('Zene'),
                    Spacer(),
                    Switch(
                      activeColor: BangColors.buttonColor,
                      value: controller.isMusicPlayingEnabled(),
                      onChanged: (val) => controller.changeMusicSettings(val),
                    ),
                  ],
                ),
              ),
              Padding(
                padding: const EdgeInsets.fromLTRB(100, 0, 100, 0),
                child: Row(
                  children: [
                    Text('SFX'),
                    Spacer(),
                    Switch(
                      activeColor: BangColors.buttonColor,
                      value: controller.isSFXEnabled(),
                      onChanged: (val) => controller.changeSFXSettings(val),
                    ),
                  ],
                ),
              ),
              Padding(
                padding: const EdgeInsets.fromLTRB(100, 0, 100, 0),
                child: Row(
                  children: [
                    Text('Értesítések'),
                    Spacer(),
                    Switch(
                      activeColor: BangColors.buttonColor,
                      value: controller.isNotificationRecievingEnabled(),
                      onChanged: (val) =>
                          controller.changeNotificationSettings(val),
                    ),
                  ],
                ),
              ),
              Spacer(),
              BangButton(
                text: 'Vissza',
                onPressed: Get.back,
              ),
              SizedBox(height: 20),
            ],
          ),
        ),
      ),
    );
  }
}
