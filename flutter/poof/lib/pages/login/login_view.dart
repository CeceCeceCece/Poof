import 'package:flutter/material.dart';
import 'package:get/get.dart';

import 'login_controller.dart';

class LoginView extends GetView<LoginController> {
  final _formKey = GlobalKey<FormState>();

  @override
  Widget build(BuildContext context) {
    final userField = TextField(controller: controller.usernameC);
    final passwordField = TextField(
      controller: controller.passwordC,
      obscureText: true,
    );

    return Form(
        key: _formKey,
        child: Scaffold(
          body: SingleChildScrollView(
            child: Center(
              child: Container(
                width: MediaQuery.of(context).size.width - 60,
                child: Obx(
                  () => controller.isLoginPage()
                      ? Column(children: [
                          SizedBox(height: 150),
                          Text('BANG!'),
                          SizedBox(
                            height: 10,
                          ),
                          userField,
                          SizedBox(
                            height: 10,
                          ),
                          passwordField,
                          SizedBox(height: 20),
                          ElevatedButton(
                            onPressed: controller.login,
                            style: ElevatedButton.styleFrom(),
                            child: Text('Login'),
                          ),
                          ElevatedButton(
                            onPressed: controller.goToRegister,
                            style: ElevatedButton.styleFrom(),
                            child: Text('Register'),
                          )
                        ])
                      : Column(
                          children: [
                            SizedBox(height: 150),
                            Text('BANG! REGISTRATION'),
                            SizedBox(
                              height: 10,
                            ),
                            userField,
                            SizedBox(
                              height: 10,
                            ),
                            passwordField,
                            SizedBox(height: 20),
                            ElevatedButton(
                              onPressed: controller.register,
                              style: ElevatedButton.styleFrom(),
                              child: Text('Register'),
                            ),
                            ElevatedButton(
                              onPressed: controller.goToLogin,
                              style: ElevatedButton.styleFrom(),
                              child: Text('Already have an account?'),
                              /* ElevatedButton(
                      onPressed: controller.showQR,
                      style: ElevatedButton.styleFrom(),
                      child: Text('show QR'),
                    ),
                    ElevatedButton(
                      onPressed: controller.readQR,
                      style: ElevatedButton.styleFrom(),
                      child: Text('read QR'),
                    ),
                    IconButton(
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
                            ),
                          ],
                        ),
                ),
              ),
            ),
          ),
        ));
  }
}
