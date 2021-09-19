import 'package:bang/core/colors.dart';
import 'package:flutter/material.dart';

class App extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      debugShowCheckedModeBanner: false,
      title: 'Bang!',
      home: Scaffold(
        backgroundColor: BangColors.background,
      ),
    );
  }
}
