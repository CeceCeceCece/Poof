import 'package:bang/cards/widgets/button.dart';
import 'package:bang/cards/widgets/input_field.dart';
import 'package:bang/core/colors.dart';
import 'package:bang/core/constants.dart';
import 'package:bang/pages/home/home_controller.dart';
import 'package:bang/routes/routes.dart';
import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:font_awesome_flutter/font_awesome_flutter.dart';
import 'package:get/get.dart';
import 'package:google_fonts/google_fonts.dart';

class HomeView extends GetView<HomeController> {
  final _formKey = GlobalKey<FormState>();
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
        resizeToAvoidBottomInset: false,
        backgroundColor: Colors.transparent,
        body: Center(
          child: Column(
            children: [
              Stack(
                alignment: Alignment.topCenter,
                children: [
                  Container(
                    width: 275,
                    height: 275,
                    child: Image.asset(
                      'assets/icons/bang_logo.png',
                      fit: BoxFit.fill,
                    ),
                  ),
                  Align(
                    alignment: Alignment.centerRight,
                    child: Padding(
                      padding: const EdgeInsets.all(12.0),
                      child: Row(
                        children: [
                          IconButton(
                              onPressed: () => Get.toNamed(Routes.SETTINGS),
                              icon: FaIcon(FontAwesomeIcons.cog),
                              iconSize: 28,
                              color: BangColors.background),
                          SizedBox(
                            width: 5,
                          ),
                          IconButton(
                              onPressed: () => controller.logout(),
                              icon: FaIcon(FontAwesomeIcons.signOutAlt),
                              iconSize: 28,
                              color: BangColors.background),
                        ],
                      ),
                    ),
                  ),
                  Positioned(
                    top: 235,
                    child: Text(
                      'Van lövésed, kivel vagy?',
                      style: GoogleFonts.graduate(
                        textStyle: TextStyle(
                          fontSize: 22,
                          fontWeight: FontWeight.bold,
                          color: Color(0xff4E3B42),
                        ),
                      ),
                      textAlign: TextAlign.center,
                    ),
                  )
                ],
              ),
              SizedBox(
                height: 25,
              ),
              BangButton(
                onPressed: () async {
                  var textController = TextEditingController();
                  var content = Form(
                    key: _formKey,
                    child: BangInputField(
                      autofocus: true,
                      onSubmit: () {
                        if (_formKey.currentState!.validate())
                          Get.back(result: textController.text);
                      },
                      hint: 'Szobanév',
                      validator: (code) {
                        if (code == null || code.length == 0)
                          return 'Nem lehet ilyen szobanév!';
                      },
                      controller: textController,
                    ),
                  );
                  controller.roomCodeToJoin = (await Get.defaultDialog<String>(
                          title: 'Írd be a szoba nevét!',
                          content: content,
                          cancel: BangButton(
                            text: 'Mégse',
                            isNormal: false,
                            onPressed: Get.back,
                            height: 35,
                            width: 60,
                          ),
                          confirm: BangButton(
                              text: 'Ok',
                              height: 35,
                              width: 60,
                              onPressed: () {
                                if (_formKey.currentState!.validate())
                                  Get.back(result: textController.text);
                              }),
                          onCancel: Get.back,
                          onWillPop: () async {
                            Get.back();
                            return false;
                          },
                          onConfirm: () {
                            if (_formKey.currentState!.validate())
                              Get.back(result: textController.text);
                          }))
                      .obs;
                  controller.joinRoom();
                },
                text: 'Csatlakozás kóddal',
              ),
              SizedBox(height: 10),
              BangButton(
                onPressed: controller.readQR,
                text: 'QR beolvasás',
              ),
              Spacer(),
              BangButton(
                onPressed: () async {
                  var textController = TextEditingController();
                  var content = Form(
                    key: _formKey,
                    child: BangInputField(
                      autofocus: true,
                      onSubmit: () {
                        if (_formKey.currentState!.validate())
                          Get.back(result: textController.text);
                      },
                      hint: 'Szobakód',
                      validator: (code) {
                        if (code == null || code.length == 0)
                          return 'Nem lehet ilyen név!';
                      },
                      controller: textController,
                    ),
                  );
                  var lobbyName = await Get.defaultDialog<String>(
                      title: 'Adj nevet a szobának!',
                      content: content,
                      cancel: BangButton(
                        text: 'Mégse',
                        isNormal: false,
                        onPressed: Get.back,
                        height: 35,
                        width: 60,
                      ),
                      confirm: BangButton(
                          text: 'Ok',
                          height: 35,
                          width: 60,
                          onPressed: () {
                            if (_formKey.currentState!.validate())
                              Get.back(result: textController.text);
                          }),
                      onCancel: Get.back,
                      onWillPop: () async {
                        Get.back();
                        return false;
                      },
                      onConfirm: () {
                        if (_formKey.currentState!.validate())
                          Get.back(result: textController.text);
                      });
                  controller.createGame(lobbyName);
                },
                text: 'Szoba létrehozása',
              ),
              Spacer(),
              Spacer(),
              Align(
                alignment: Alignment.bottomRight,
                child: Padding(
                  padding: const EdgeInsets.all(12.0),
                  child: BangButton(
                    width: 90,
                    height: 40,
                    onPressed: () => SystemNavigator.pop(animated: true),
                    text: 'Kilépés',
                  ),
                ),
              ),
            ],
          ),
        ),
      ),
    );
  }
}
