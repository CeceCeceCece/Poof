import 'package:bang/routes/routes.dart';
import 'package:bang/services/auth_service.dart';
import 'package:flutter/material.dart';
import 'package:fluttertoast/fluttertoast.dart';
import 'package:get/get.dart';
import 'package:qr_code_scanner/qr_code_scanner.dart';
import 'package:qr_flutter/qr_flutter.dart';

class LoginController extends GetxController {
  final usernameC = TextEditingController(text: 'Cece');
  final passwordC = TextEditingController(text: 'admin');
  final confirmPasswordC = TextEditingController(text: 'admin');

  var isLoginPage = true.obs;

  final GlobalKey _qrKey = GlobalKey(debugLabel: 'QR');
  QRViewController? controller;

  void login() {
    Get.find<AuthService>().login(usernameC.text, passwordC.text);
    Get.offAndToNamed(Routes.HOME);
  }

  void register() {
    Get.find<AuthService>().register(usernameC.text, passwordC.text);
  }

  void goToRegister() {
    isLoginPage.value = false;
    usernameC.clear();
    passwordC.clear();
    confirmPasswordC.clear();
  }

  void goToLogin() {
    isLoginPage.value = true;
    usernameC.clear();
    passwordC.clear();
    confirmPasswordC.clear();
  }

  void readQR() {
    Get.defaultDialog(
        content: Container(
      width: 300,
      height: 300,
      child: QRView(
        key: _qrKey,
        onQRViewCreated: _onQRViewCreated,
      ),
    ));
  }

  void showQR() {
    Get.defaultDialog(
      content: Container(
        width: 250,
        height: 250,
        child: QrImage(
          data: "QR READ COMPLETED!",
          version: QrVersions.auto,
          errorCorrectionLevel: QrErrorCorrectLevel.Q,
          size: 100.0,
          embeddedImage: AssetImage(
            'assets/icons/bang_logo.png',
          ),
          embeddedImageStyle: QrEmbeddedImageStyle(size: Size(30, 30)),
        ),
      ),
    );
  }

  void _onQRViewCreated(QRViewController controller) {
    this.controller = controller;
    controller.scannedDataStream
        .listen((scanData) => _onQRReadSuccessfully(scanData.code));
  }

  void _onQRReadSuccessfully(String qrValue) {
    Get.back();
    controller?.dispose();
    Fluttertoast.showToast(
        msg: "QR Read: $qrValue",
        toastLength: Toast.LENGTH_LONG,
        gravity: ToastGravity.BOTTOM,
        timeInSecForIosWeb: 1,
        backgroundColor: Colors.red,
        textColor: Colors.white,
        fontSize: 16.0);
  }
}
