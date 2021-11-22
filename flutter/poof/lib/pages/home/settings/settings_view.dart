import 'package:bang/cards/widgets/button.dart';
import 'package:bang/core/colors.dart';
import 'package:bang/core/constants.dart';
import 'package:bang/pages/home/settings/settings_controller.dart';
import 'package:flutter/material.dart';
import 'package:get/get.dart';
import 'package:google_fonts/google_fonts.dart';

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
            crossAxisAlignment: CrossAxisAlignment.center,
            children: [
              Container(
                width: 225,
                height: 225,
                child: Image.asset(
                  'assets/icons/bang_logo.png',
                  fit: BoxFit.fill,
                ),
              ),
              Padding(
                padding: EdgeInsets.only(left: 50, right: 50),
                child: Container(
                  height: 60,
                  width: 300,
                  decoration: BoxDecoration(
                      color: Colors.white38,
                      border: Border.all(color: Colors.white, width: 1.5),
                      borderRadius: BorderRadius.circular(30)),
                  child: Padding(
                    padding: const EdgeInsets.all(8.0),
                    child: Text(
                      'Beállítások',
                      style: GoogleFonts.graduate(
                        textStyle: TextStyle(
                            fontSize: 25,
                            fontWeight: FontWeight.bold,
                            color: Colors.brown),
                      ),
                      textAlign: TextAlign.center,
                    ),
                  ),
                ),
              ),
              Spacer(),
              Padding(
                padding: EdgeInsets.only(left: 25, right: 25),
                child: Container(
                  decoration: BoxDecoration(
                      color: Colors.white38,
                      border: Border.all(color: Colors.white, width: 1.5),
                      borderRadius: BorderRadius.circular(30)),
                  child: Padding(
                    padding: const EdgeInsets.all(15.0),
                    child: Column(
                      children: [
                        Padding(
                          padding: const EdgeInsets.fromLTRB(25, 0, 25, 0),
                          child: Row(
                            children: [
                              Text(
                                'Zene',
                                style: GoogleFonts.graduate(
                                  textStyle: TextStyle(
                                      fontSize: 15,
                                      fontWeight: FontWeight.bold,
                                      color: Colors.brown),
                                ),
                                textAlign: TextAlign.center,
                              ),
                              Spacer(),
                              Switch(
                                activeColor: BangColors.buttonColor,
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
                                'SFX',
                                style: GoogleFonts.graduate(
                                  textStyle: TextStyle(
                                      fontSize: 15,
                                      fontWeight: FontWeight.bold,
                                      color: Colors.brown),
                                ),
                                textAlign: TextAlign.center,
                              ),
                              Spacer(),
                              Switch(
                                activeColor: BangColors.buttonColor,
                                value: controller.isSFXEnabled(),
                                onChanged: (val) =>
                                    controller.changeSFXSettings(val),
                              ),
                            ],
                          ),
                        ),
                        Padding(
                          padding: const EdgeInsets.fromLTRB(25, 0, 25, 0),
                          child: Row(
                            children: [
                              Text(
                                'Értesítések',
                                style: GoogleFonts.graduate(
                                  textStyle: TextStyle(
                                      fontSize: 15,
                                      fontWeight: FontWeight.bold,
                                      color: Colors.brown),
                                ),
                                textAlign: TextAlign.center,
                              ),
                              Spacer(),
                              Switch(
                                activeColor: BangColors.buttonColor,
                                value:
                                    controller.isNotificationRecievingEnabled(),
                                onChanged: (val) =>
                                    controller.changeNotificationSettings(val),
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
                  text: 'Vissza',
                  onPressed: Get.back,
                ),
              ),
            ],
          ),
        ),
      ),
    );
  }
}
