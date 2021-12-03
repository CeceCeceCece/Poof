import 'package:bang/core/colors.dart';
import 'package:bang/core/constants.dart';
import 'package:bang/core/helpers/validators.dart';
import 'package:flutter/material.dart';

class BangInputField extends StatefulWidget {
  final String hint;
  final TextEditingController? controller;
  final bool isPassword;
  final Color color;
  final Color hintColor;
  final void Function(String) onChanged;
  final String? Function(String?) validator;
  final VoidCallback? onSubmit;
  final bool autofocus;

  final focusNode = FocusNode();

  final FocusNode? nextNode;

  BangInputField(
      {required this.hint,
      this.controller,
      this.autofocus = false,
      this.isPassword = false,
      this.color = Colors.white,
      this.hintColor = BangColors.hintColor,
      this.onChanged = Constants.defaultOnChanged,
      this.validator = Validators.defaultValidator,
      Key? key,
      this.onSubmit,
      this.nextNode})
      : super(key: key);

  @override
  _BangInputFieldState createState() => _BangInputFieldState();
}

class _BangInputFieldState extends State<BangInputField> {
  @override
  Widget build(BuildContext context) {
    return Stack(children: [
      Container(
        height: 48,
        decoration: BoxDecoration(
            borderRadius: BorderRadius.circular(32.0), color: Colors.white),
        //child:
      ),
      TextFormField(
        cursorColor: BangColors.buttonGradientColors.first,
        showCursor: true,
        cursorRadius: Radius.zero,
        validator: widget.validator,
        autofocus: widget.autofocus,
        autovalidateMode: AutovalidateMode.disabled,
        controller: widget.controller,
        obscureText: widget.isPassword,
        textInputAction: widget.nextNode != null
            ? TextInputAction.next
            : TextInputAction.done,
        onFieldSubmitted: (_) {
          widget.focusNode.unfocus();
          widget.onSubmit?.call();
        },

        // style: UnderseaStyles.inputTextStyle,
        onChanged: widget.onChanged,
        decoration: InputDecoration(
            focusedErrorBorder: OutlineInputBorder(
                borderRadius: BorderRadius.circular(32.0),
                borderSide:
                    BorderSide(color: BangColors.buttonShadowColor, width: 3)),
            focusedBorder: OutlineInputBorder(
                borderRadius: BorderRadius.circular(32.0),
                borderSide: BorderSide(
                    color: BangColors.buttonGradientColors.first, width: 3)),
            errorStyle:
                TextStyle(color: BangColors.buttonShadowColor, fontSize: 14),
            errorBorder: OutlineInputBorder(
                borderRadius: BorderRadius.circular(32.0),
                borderSide:
                    BorderSide(color: BangColors.buttonShadowColor, width: 3)),
            contentPadding: EdgeInsets.fromLTRB(20.0, 15.0, 20.0, 15.0),
            hintText: widget.hint,
            hintStyle: TextStyle(color: BangColors.hintColor),
            border: InputBorder.none),
      )
    ]);
  }
}
