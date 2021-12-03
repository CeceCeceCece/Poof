import 'package:flutter/material.dart';
import 'package:get/get.dart';

import 'hu_HU.dart';

class AppTranslations extends Translations {
  static Locale? get locale => Get.deviceLocale;
  static final fallbackLocale = Locale('hu', 'HU');
  @override
  Map<String, Map<String, String>> get keys => {'hu_HU': hu_HU};
}
