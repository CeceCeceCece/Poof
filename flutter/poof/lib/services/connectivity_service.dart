import 'dart:async';
import 'dart:developer';

import 'package:bang/cards/widgets/button.dart';
import 'package:bang/core/animations.dart';
import 'package:bang/core/lang/strings.dart';
import 'package:connectivity_plus/connectivity_plus.dart';
import 'package:get/get.dart';
import 'package:lottie/lottie.dart';

import 'service_base.dart';

class ConnectivityService extends ServiceBase {
  late StreamSubscription _internetStateStream;
  var _internetState = ConnectivityResult.wifi.obs;
  bool _isDialogOpen = false;
  Timer? _noInternetChecker;
  int _debounceInMillis = 1000;
  int _offlineStateCheckerInterval = 10; // seconds

  @override
  Future<void> init() async {
    debounce(_internetState,
        (ConnectivityResult value) => _openDialogIfNeeded(value),
        time: Duration(milliseconds: _debounceInMillis));

    _internetState.value = await (Connectivity().checkConnectivity());

    _internetStateStream = Connectivity()
        .onConnectivityChanged
        .listen((ConnectivityResult result) {
      _internetState.value = result;
    });

    log('SERVICES: CONNECTIVITY_SERVICE INITIALIZED');
  }

  @override
  void onClose() {
    _internetStateStream.cancel();
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
    if (_internetState() != ConnectivityResult.none) {
      _noInternetChecker?.cancel();
      _noInternetChecker = null;
    }
    _isDialogOpen = false;
    print("closed");
  }

  void _log(ConnectivityResult value) =>
      log('INTERNET STATE: ${value.toString().split('.')[1]}');

  void _openDialog() {
    Get.defaultDialog(
            cancel: BangButton(
              width: 60,
              height: 35,
              text: AppStrings.ok.tr,
              isNormal: false,
              onPressed: Get.back,
            ),
            title: AppStrings.no_internet.tr,
            onCancel: _closeDialog,
            content: Lottie.asset(BangAnimations.noInternet,
                height: 200, width: 250))
        .whenComplete(_closeDialog);
    _isDialogOpen = true;
    _noInternetChecker = Timer.periodic(
        Duration(seconds: _offlineStateCheckerInterval),
        (timer) => _openDialogIfNeeded(_internetState()));
  }
}
