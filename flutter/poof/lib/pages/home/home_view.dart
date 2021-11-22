import 'package:bang/cards/widgets/button.dart';
import 'package:bang/cards/widgets/input_field.dart';
import 'package:bang/core/colors.dart';
import 'package:bang/core/constants.dart';
import 'package:bang/pages/home/home_controller.dart';
import 'package:bang/routes/routes.dart';
import 'package:flutter/material.dart';
import 'package:font_awesome_flutter/font_awesome_flutter.dart';
import 'package:get/get.dart';

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
        backgroundColor: Colors.transparent,
        body: Center(
          child: Column(
            //mainAxisAlignment: MainAxisAlignment.spaceAround,
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
                              onPressed: () => Get.toNamed(Routes.PROFILE),
                              icon: FaIcon(FontAwesomeIcons.userCircle),
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
                ],
              ),
              BangButton(
                //onPressed: () => controller.joinRoom('randomID'),
                onPressed: () async {
                  var textController = TextEditingController(text: '123456');
                  var content = Form(
                    key: _formKey,
                    child: BangInputField(
                      autofocus: true,
                      onSubmit: () {
                        if (_formKey.currentState!.validate())
                          Get.back(result: textController.text);
                        /*else
                          Fluttertoast.showToast(
                              msg: 'Nem megfelelő szoba kód!',
                              toastLength: Toast.LENGTH_LONG,
                              gravity: ToastGravity.BOTTOM,
                              timeInSecForIosWeb: 1);*/
                      },
                      hint: 'Szobakód',
                      validator: (code) {
                        if (code?.length != 6)
                          return 'Nem megfelelő hosszú a kód';
                      },
                      controller: textController,
                    ),
                  );
                  controller.roomCodeToJoin = (await Get.defaultDialog<String>(
                          title: 'Írd be a kódot!',
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
                                /*else
                                  Fluttertoast.showToast(
                                      msg: 'Nem megfelelő hosszú a kód',
                                      toastLength: Toast.LENGTH_LONG,
                                      gravity: ToastGravity.BOTTOM,
                                      timeInSecForIosWeb: 1);*/
                              }),
                          onCancel: Get.back,
                          onWillPop: () async {
                            Get.back();
                            return false;
                          },
                          onConfirm: () {
                            if (_formKey.currentState!.validate())
                              Get.back(result: textController.text);
                            /*else
                              Fluttertoast.showToast(
                                  msg: 'Nem megfelelő hosszú a kód',
                                  toastLength: Toast.LENGTH_LONG,
                                  gravity: ToastGravity.BOTTOM,
                                  timeInSecForIosWeb: 1);*/
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
              SizedBox(height: 25),
              BangButton(
                onPressed: controller.createGame,
                text: 'Szoba létrehozása',
              ),

              /*IconButton(
                          // Use the FaIcon Widget + FontAwesomeIcons class for the IconData
                          icon: FaIcon(FontAwesomeIcons.gamepad),
                          onPressed: () {
                            print("Pressed");
                          }),
                      AnimatedIconButton(
                        onPressed: () => print('all icons pressed'),
                        icons: [
                          AnimatedIconItem(
                            icon: Icon(Icons.mic_off),
                            onPressed: () => print('add pressed'),
                          ),
                          AnimatedIconItem(
                            icon: Icon(Icons.mic),
                          ),
                        ],
                      ),
                      SizedBox(
                        width: 250.0,
                        child: DefaultTextStyle(
                          style: const TextStyle(
                            fontSize: 35,
                            color: Colors.white,
                            shadows: [
                              Shadow(
                                blurRadius: 7.0,
                                color: Colors.white,
                                offset: Offset(0, 0),
                              ),
                            ],
                          ),
                          child: AnimatedTextKit(
                            repeatForever: true,
                            animatedTexts: [
                              FlickerAnimatedText('Flicker Frenzy',
                                  textStyle: TextStyle(color: Colors.amber)),
                              FlickerAnimatedText('Night Vibes On',
                                  textStyle: TextStyle(color: Colors.amber)),
                              FlickerAnimatedText("C'est La Vie !",
                                  textStyle: TextStyle(color: Colors.amber)),
                            ],
                            onTap: () {
                              print("Tap Event");
                            },
                          ),
                        ),
                      )*/
            ],
          ),
        ),
      ),
    );
  }
}
