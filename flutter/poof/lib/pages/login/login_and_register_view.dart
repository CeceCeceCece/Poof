import 'package:bang/core/helpers/app_validators.dart';
import 'package:bang/core/lang/app_strings.dart';
import 'package:bang/widgets/bang_background.dart';
import 'package:bang/widgets/bang_button.dart';
import 'package:bang/widgets/bang_input_field.dart';
import 'package:bang/widgets/bang_logo.dart';
import 'package:flutter/material.dart';
import 'package:get/get.dart';

import 'login_and_register_controller.dart';

class LoginAndRegisterView extends GetView<LoginAndRegisterController> {
  final _formKey = GlobalKey<FormState>();
  final _passwordNode = FocusNode();
  final _confirmPasswordNode = FocusNode();

  @override
  Widget build(BuildContext context) {
    return Form(
      key: _formKey,
      child: GestureDetector(
        onTap: () => FocusScope.of(context).unfocus(),
        child: BangBackground(
          child: SingleChildScrollView(
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
      focusNode: _confirmPasswordNode,
      isPassword: true,
      onSubmit: _register,
      validator: (value) =>
          AppValidators.passwords(value, controller.passwordC.text),
    );

    var passwordField = BangInputField(
      controller: controller.passwordC,
      nextNode: confirmPasswordField.focusNode,
      focusNode: _passwordNode,
      hint: AppStrings.password.tr,
      validator: (value) =>
          AppValidators.passwords(value, controller.confirmPasswordC.text),
      onSubmit: () => _confirmPasswordNode.nextFocus(),
      isPassword: true,
    );

    var usernameField = BangInputField(
      controller: controller.usernameC,
      hint: AppStrings.username.tr,
      nextNode: passwordField.focusNode,
      onSubmit: () => _passwordNode.nextFocus(),
    );
    return Column(
      children: [
        BangLogo(),
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
          onPressed: _register,
          text: AppStrings.registration.tr,
        ),
        SizedBox(height: 20),
        BangButton(
          onPressed: () => _goToLogin(context),
          isLoading: controller.loading(),
          text: AppStrings.already_have_account.tr,
        ),
      ],
    );
  }

  void _register() {
    if (_formKey.currentState!.validate()) {
      controller.register();
    }
  }

  void _goToLogin(BuildContext context) {
    _formKey.currentState?.reset();
    controller.goToLogin();
    FocusScope.of(context).unfocus();
  }

  void _goToRegister(BuildContext context) {
    _formKey.currentState?.reset();
    controller.goToRegister();

    FocusScope.of(context).unfocus();
  }

  void _login() {
    if (_formKey.currentState!.validate()) controller.login();
  }

  Widget _buildLogin(BuildContext context) {
    var passwordField = BangInputField(
      controller: controller.passwordC,
      focusNode: _passwordNode,
      hint: AppStrings.password.tr,
      onSubmit: _login,
      isPassword: true,
    );

    var usernameField = BangInputField(
      controller: controller.usernameC,
      hint: AppStrings.username.tr,
      nextNode: passwordField.focusNode,
      onSubmit: _passwordNode.nextFocus,
    );

    return Column(children: [
      BangLogo(),
      usernameField,
      SizedBox(
        height: 10,
      ),
      passwordField,
      SizedBox(height: 80),
      BangButton(
        onPressed: _login,
        text: AppStrings.login.tr,
        isLoading: controller.loading(),
      ),
      SizedBox(height: 20),
      BangButton(
        isLoading: controller.loading(),
        onPressed: () => _goToRegister(context),
        text: AppStrings.dont_have_an_account.tr,
      ),
    ]);
  }
}
