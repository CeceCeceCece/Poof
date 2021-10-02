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
                child: Column(
                  children: [
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
                      onPressed: controller.showQR,
                      style: ElevatedButton.styleFrom(),
                      child: Text('show QR'),
                    ),
                    ElevatedButton(
                      onPressed: controller.readQR,
                      style: ElevatedButton.styleFrom(),
                      child: Text('read QR'),
                    ),
                  ],
                ),
              ),
            ),
          ),
        ));
  }
}
