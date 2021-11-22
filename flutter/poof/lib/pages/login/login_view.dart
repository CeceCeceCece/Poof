import 'package:bang/cards/widgets/button.dart';
import 'package:bang/cards/widgets/input_field.dart';
import 'package:bang/core/constants.dart';
import 'package:flutter/material.dart';
import 'package:get/get.dart';

import 'login_controller.dart';

class LoginView extends GetView<LoginController> {
  final _formKey = GlobalKey<FormState>();

  @override
  Widget build(BuildContext context) {
    return Form(
      key: _formKey,
      child: Container(
        decoration: BoxDecoration(
            image: DecorationImage(
                image: AssetImage(
                  Constants.backgroundPath,
                ),
                fit: BoxFit.fitHeight)),
        child: Scaffold(
          backgroundColor: Colors.transparent,
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
                        ? _buildLogin(context)
                        : _buildRegistration(context),
                  );
                }),
              ),
            ),
          ),
        ),
      ),
    );
  }

  Column _buildRegistration(BuildContext context) {
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
        Container(
          width: 225,
          height: 225,
          child: Image.asset(
            'assets/icons/bang_logo.png',
            fit: BoxFit.fill,
          ),
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
          onPressed: () {
            _formKey.currentState?.reset();
            controller.goToLogin();
            FocusScope.of(context).unfocus();
          },
          text: 'Már van fiókom...',
        ),
      ],
    );
  }

  Widget _buildLogin(BuildContext context) {
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
        onSubmit: () {
          //FocusScope.of(context).unfocus();
          passwordField.focusNode.nextFocus();
        });

    return Column(children: [
      Container(
        width: 225,
        height: 225,
        child: Image.asset(
          'assets/icons/bang_logo.png',
          fit: BoxFit.fill,
        ),
      ),
      usernameField,
      SizedBox(
        height: 10,
      ),
      passwordField,
      SizedBox(height: 80),
      BangButton(
        onPressed: () {
          if (_formKey.currentState!.validate()) controller.login();
        },
        text: 'Bejelentkezés',
      ),
      SizedBox(height: 20),
      BangButton(
        onPressed: () {
          _formKey.currentState?.reset();
          controller.goToRegister();

          FocusScope.of(context).unfocus();
        },
        text: 'Még nincs fiókom',
      ),
    ]);
  }
}
