import 'package:bang/pages/home/home_controller.dart';
import 'package:flutter/material.dart';
import 'package:get/get.dart';

class HomeView extends GetView<HomeController> {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: Center(
        child: Column(
          mainAxisAlignment: MainAxisAlignment.spaceAround,
          children: [
            Text('home'),
            ElevatedButton(
              onPressed: () => controller.joinRoom('randomID'),
              child: Text('Play!'),
            ),
            ElevatedButton(
              onPressed: () => controller.logout(),
              child: Text('Logout'),
            )
          ],
        ),
      ),
    );
  }
}
