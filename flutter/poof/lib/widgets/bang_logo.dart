import 'package:bang/core/app_constants.dart';
import 'package:flutter/material.dart';

class BangLogo extends StatelessWidget {
  const BangLogo({
    Key? key,
    this.size = 225,
  }) : super(key: key);
  final double size;

  @override
  Widget build(BuildContext context) {
    return Container(
      width: size,
      height: size,
      child: Image.asset(
        AppAssetPaths.bangLogo,
        fit: BoxFit.fill,
      ),
    );
  }
}
