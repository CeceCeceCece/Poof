import 'package:bang/cards/widgets/button.dart';
import 'package:bang/cards/widgets/input_field.dart';
import 'package:bang/core/constants.dart';
import 'package:bang/core/helpers/validators.dart';
import 'package:bang/core/lang/strings.dart';
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
                  AssetPaths.backgroundPath,
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
      hint: AppStrings.password_again.tr,
      isPassword: true,
      onSubmit: () {
        if (_formKey.currentState!.validate()) {
          controller.register();
        }
      },
      validator: (value) =>
          Validators.passwords(value, controller.passwordC.text),
    );

    var passwordField = BangInputField(
      controller: controller.passwordC,
      nextNode: confirmPasswordField.focusNode,
      hint: AppStrings.password.tr,
      validator: (value) =>
          Validators.passwords(value, controller.confirmPasswordC.text),
      onSubmit: () => confirmPasswordField.focusNode.nextFocus(),
      isPassword: true,
    );

    var usernameField = BangInputField(
      controller: controller.usernameC,
      hint: AppStrings.username.tr,
      nextNode: passwordField.focusNode,
      onSubmit: () => passwordField.focusNode.nextFocus(),
    );
    return Column(
      children: [
        Container(
          width: 225,
          height: 225,
          child: Image.asset(
            AssetPaths.bangLogo,
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
          isLoading: controller.loading(),
          onPressed: () {
            if (_formKey.currentState!.validate()) {
              controller.register();
            }
          },
          text: AppStrings.registration.tr,
        ),
        SizedBox(height: 20),
        BangButton(
          onPressed: () {
            _formKey.currentState?.reset();
            controller.goToLogin();
            FocusScope.of(context).unfocus();
          },
          isLoading: controller.loading(),
          text: AppStrings.already_have_account.tr,
        ),
      ],
    );
  }

  Widget _buildLogin(BuildContext context) {
    var passwordField = BangInputField(
      controller: controller.passwordC,
      hint: AppStrings.password.tr,
      onSubmit: () {
        if (_formKey.currentState!.validate()) controller.login();
      },
      isPassword: true,
    );

    var usernameField = BangInputField(
        controller: controller.usernameC,
        hint: AppStrings.username.tr,
        nextNode: passwordField.focusNode,
        onSubmit: () {
          passwordField.focusNode.nextFocus();
        });

    return Column(children: [
      Container(
        width: 225,
        height: 225,
        child: Image.asset(
          AssetPaths.bangLogo,
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
        text: AppStrings.login.tr,
        isLoading: controller.loading(),
      ),
      SizedBox(height: 20),
      BangButton(
        isLoading: controller.loading(),
        onPressed: () {
          _formKey.currentState?.reset();
          controller.goToRegister();

          FocusScope.of(context).unfocus();
        },
        text: AppStrings.dont_have_an_account.tr,
      ),
    ]);
  }
}
