import 'package:bang/core/app_constants.dart';
import 'package:flutter/material.dart';

class BangBackground extends StatelessWidget {
  const BangBackground({
    Key? key,
    this.onWillPop,
    required this.child,
    this.backgroundColor = Colors.transparent,
    this.resizeToAvoidBottomInset,
  }) : super(key: key);

  final Future<bool> Function()? onWillPop;
  final Widget child;
  final Color backgroundColor;
  final bool? resizeToAvoidBottomInset;

  @override
  Widget build(BuildContext context) {
    return Container(
      decoration: BoxDecoration(
          image: DecorationImage(
              image: AssetImage(
                AppAssetPaths.backgroundPath,
              ),
              fit: BoxFit.fitHeight)),
      child: WillPopScope(
        onWillPop: onWillPop,
        child: SafeArea(
          child: Scaffold(
            resizeToAvoidBottomInset: resizeToAvoidBottomInset,
            backgroundColor: backgroundColor,
            body: child,
          ),
        ),
      ),
    );
  }
}
