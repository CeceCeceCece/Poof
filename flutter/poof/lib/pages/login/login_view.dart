import 'package:bang/cards/widgets/button.dart';
import 'package:bang/cards/widgets/input_field.dart';
import 'package:bang/core/colors.dart';
import 'package:flutter/material.dart';
import 'package:get/get.dart';

import 'login_controller.dart';

class LoginView extends GetView<LoginController> {
  final _formKey = GlobalKey<FormState>();

  @override
  Widget build(BuildContext context) {
    return Form(
        key: _formKey,
        child: Scaffold(
          backgroundColor: BangColors.background,
          body: SingleChildScrollView(
            child: Center(
              child: Container(
                width: MediaQuery.of(context).size.width - 60,
                child: Obx(() {
                  return WillPopScope(
                    onWillPop: () async {
                      if (controller.isLoginPage()) return true;
                      controller.goToLogin();
                      return false;
                    },
                    child: controller.isLoginPage()
                        ? _buildLogin()
                        : _buildRegistration(),
                  );
                }),
              ),
            ),
          ),
        ));
  }

  Column _buildRegistration() {
    var confirmPasswordField = BangInputField(
      controller: controller.confirmPasswordC,
      hint: 'Jelszó újra',
      isPassword: true,
      onSubmit: () {
        if (_formKey.currentState!.validate()) {
          controller.register();
          controller.login();
        }
      },
      validator: (value) {
        if (value == null || value.isEmpty) {
          return 'Ez a mező nem lehet üres';
        }
        if (value.removeAllWhitespace != value) {
          return 'Spacekkel sem versz át!';
        }
        if (controller.confirmPasswordC.text != controller.passwordC.text) {
          return 'A két jelszó nem egyezik!';
        }
      },
    );

    var passwordField = BangInputField(
      controller: controller.passwordC,
      nextNode: confirmPasswordField.focusNode,
      hint: 'Jelszó',
      validator: (value) {
        if (value == null || value.isEmpty) {
          return 'Ez a mező nem lehet üres';
        }
        if (value.removeAllWhitespace != value) {
          return 'Spacekkel sem versz át!';
        }
        if (controller.confirmPasswordC.text != controller.passwordC.text) {
          return 'A két jelszó nem egyezik!';
        }
      },
      onSubmit: () => confirmPasswordField.focusNode.nextFocus(),
      isPassword: true,
    );

    var usernameField = BangInputField(
      controller: controller.usernameC,
      hint: 'Felhasználónév',
      nextNode: passwordField.focusNode,
      onSubmit: () => passwordField.focusNode.nextFocus(),
    );
    return Column(
      children: [
        SizedBox(height: 150),
        Text('Bang Regisztráció'),
        SizedBox(
          height: 60,
        ),
        usernameField,
        SizedBox(
          height: 10,
        ),
        passwordField,
        SizedBox(
          height: 10,
        ),
        confirmPasswordField,
        SizedBox(height: 20),
        BangButton(
          onPressed: () {
            if (_formKey.currentState!.validate()) {
              controller.register();
              controller.login();
            }
          },
          text: 'Regisztráció!',
        ),
        SizedBox(height: 20),
        BangButton(
          onPressed: controller.goToLogin,
          text: 'Már van fiókom...',
        ),
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
      ],
    );
  }

  Widget _buildLogin() {
    var passwordField = BangInputField(
      controller: controller.passwordC,
      hint: 'Jelszó',
      onSubmit: () {
        if (_formKey.currentState!.validate()) controller.login();
      },
      isPassword: true,
    );

    var usernameField = BangInputField(
      controller: controller.usernameC,
      hint: 'Felhasználónév',
      nextNode: passwordField.focusNode,
      onSubmit: () => passwordField.focusNode.nextFocus(),
    );

    return Column(children: [
      SizedBox(height: 150),
      Text('BANG'),
      SizedBox(
        height: 60,
      ),
      usernameField,
      SizedBox(
        height: 10,
      ),
      passwordField,
      SizedBox(height: 20),
      BangButton(
        onPressed: () {
          if (_formKey.currentState!.validate()) controller.login();
        },
        text: 'Bejelentkezés',
      ),
      SizedBox(height: 20),
      BangButton(
        onPressed: controller.goToRegister,
        text: 'Még nincs fiókom',
      ),
    ]);
  }
}
