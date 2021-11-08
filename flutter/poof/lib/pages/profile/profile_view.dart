import 'package:bang/core/colors.dart';
import 'package:bang/pages/profile/profile_controller.dart';
import 'package:flutter/material.dart';
import 'package:get/get.dart';

class ProfileView extends GetView<ProfileController> {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
        backgroundColor: BangColors.background, body: Text('Profile Page'));
  }
}
