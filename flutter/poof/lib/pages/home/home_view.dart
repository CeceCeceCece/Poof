import 'package:bang/cards/widgets/button.dart';
import 'package:bang/cards/widgets/input_field.dart';
import 'package:bang/core/colors.dart';
import 'package:bang/pages/home/home_controller.dart';
import 'package:bang/routes/routes.dart';
import 'package:flutter/material.dart';
import 'package:fluttertoast/fluttertoast.dart';
import 'package:font_awesome_flutter/font_awesome_flutter.dart';
import 'package:get/get.dart';

class HomeView extends GetView<HomeController> {
  final _formKey = GlobalKey<FormState>();
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      backgroundColor: BangColors.background,
      body: Center(
        child: Column(
          //mainAxisAlignment: MainAxisAlignment.spaceAround,
          children: [
            SizedBox(
              height: 100,
            ),
            Text('HOMEPAGE'),
            SizedBox(height: 50),
            BangButton(
              //onPressed: () => controller.joinRoom('randomID'),
              onPressed: () async {
                var textController = TextEditingController();
                var content = Form(
                  key: _formKey,
                  child: BangInputField(
                    hint: 'Lobby code',
                    validator: (code) {
                      if (code?.length != 6)
                        return 'Nem megfelelő hosszú a kód';
                    },
                    controller: textController,
                  ),
                );
                controller.roomCodeToJoin = (await Get.defaultDialog<String>(
                        title: 'Enter lobby code:',
                        content: content,
                        onCancel: Get.back,
                        onWillPop: () async {
                          Get.back();
                          return false;
                        },
                        onConfirm: () {
                          if (_formKey.currentState!.validate())
                            Get.back(result: textController.text);
                          else
                            Fluttertoast.showToast(
                                msg: 'Invalid format of lobby code!',
                                toastLength: Toast.LENGTH_LONG,
                                gravity: ToastGravity.BOTTOM,
                                timeInSecForIosWeb: 1);
                        }))
                    .obs;
                controller.joinRoom();
              },

              text: 'Join with code',
            ),
            SizedBox(height: 10),
            BangButton(
              onPressed: controller.readQR,
              text: 'Join with QR',
            ),
            SizedBox(height: 10),
            /* BangButton(
              onPressed: controller.showQR,
              text: 'show QR',
            ),*/
            BangButton(
              onPressed: () => controller.showQR('123456'),
              text: 'Create a game!',
            ),
            SizedBox(height: 10),
            BangButton(
              onPressed: () => Get.toNamed(Routes.PROFILE),
              text: 'Profile...',
            ),
            Expanded(
              child: Align(
                alignment: Alignment.bottomRight,
                child: Padding(
                  padding: const EdgeInsets.all(12.0),
                  child: Column(
                    children: [
                      Spacer(),
                      IconButton(
                          onPressed: () => Get.toNamed(Routes.SETTINGS),
                          icon: FaIcon(FontAwesomeIcons.cogs),
                          color: BangColors.buttonGradientColors.last),
                      BangButton(
                        width: 70,
                        height: 40,
                        onPressed: () => controller.logout(),
                        text: 'Logout',
                      ),
                    ],
                  ),
                ),
              ),
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
    );
  }
}
