import 'dart:async';

import 'package:bang/core/animations.dart';
import 'package:connectivity_plus/connectivity_plus.dart';
import 'package:get/get.dart';
import 'package:lottie/lottie.dart';

import 'service_base.dart';

class ConnectivityService extends GetxService implements ServiceBase {
  late StreamSubscription _internetState;
  var _result = ConnectivityResult.wifi.obs;
  bool _isDialogOpen = false;
  Timer? _noInternetChecker;

  @override
  Future<void> init() async {
    debounce(_result, (ConnectivityResult value) => _openDialogIfNeeded(value),
        time: Duration(milliseconds: 500));

    _result.value = await (Connectivity().checkConnectivity());

    _internetState = Connectivity()
        .onConnectivityChanged
        .listen((ConnectivityResult result) {
      _result.value = result;
    });
  }

  @override
  void onClose() {
    _internetState.cancel();
    super.onClose();
  }

  void _openDialogIfNeeded(ConnectivityResult value) {
    if (_isDialogOpen && value == ConnectivityResult.none) return;
    if (value == ConnectivityResult.none) {
      _openDialog();
    } else if (_isDialogOpen) {
      Get.back();
      _closeDialog();
    }
    _log(value);
  }

  void _closeDialog() {
    if (_result() != ConnectivityResult.none) {
      _noInternetChecker?.cancel();
      _noInternetChecker = null;
    }
    _isDialogOpen = false;
    print("closed");
  }

  void _log(ConnectivityResult value) =>
      print('INTERNET STATE: ${value.toString().split('.')[1]}');

  void _openDialog() {
    Get.defaultDialog(
            onCancel: _closeDialog,
            content: Lottie.asset(BangAnimations.noInternet,
                height: 300, width: 300))
        .whenComplete(_closeDialog);
    _isDialogOpen = true;
    _noInternetChecker = Timer.periodic(
        Duration(seconds: 10), (timer) => _openDialogIfNeeded(_result()));
  }
}
