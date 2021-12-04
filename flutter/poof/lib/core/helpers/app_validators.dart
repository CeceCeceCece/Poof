import 'package:bang/core/lang/app_strings.dart';
import 'package:get/get_utils/src/extensions/internacionalization.dart';
import 'package:get/get_utils/src/extensions/string_extensions.dart';

abstract class AppValidators {
  static String? passwords(String? value, String otherValue) {
    if (value != otherValue) {
      return AppStrings.passwords_dont_match.tr;
    }
    return defaultValidator(value);
  }

  static String? defaultValidator(String? value) {
    if (value == null || value.isEmpty) {
      return AppStrings.field_cannot_be_empty.tr;
    }
    if (value.removeAllWhitespace != value)
      return AppStrings.field_cannot_be_empty.tr;
  }
}
