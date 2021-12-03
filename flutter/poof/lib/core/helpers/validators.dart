import 'package:bang/core/lang/strings.dart';
import 'package:get/get_utils/src/extensions/internacionalization.dart';
import 'package:get/get_utils/src/extensions/string_extensions.dart';

abstract class Validators {
  static String? passwords(String? value, String otherValue) {
    if (value == null || value.isEmpty) {
      return AppStrings.field_cannot_be_empty.tr;
    }
    if (value.removeAllWhitespace != value) {
      return AppStrings.field_cannot_be_empty.tr;
    }
    if (value != otherValue) {
      return AppStrings.passwords_dont_match.tr;
    }
  }

  static String? defaultValidator(String? value) {
    if (value == null || value.isEmpty) {
      return AppStrings.field_cannot_be_empty.tr;
    }
    if (value.removeAllWhitespace != value)
      return AppStrings.field_cannot_be_empty.tr;
  }
}
