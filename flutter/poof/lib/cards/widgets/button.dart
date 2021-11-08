import 'package:bang/core/colors.dart';
import 'package:flutter/material.dart';

class BangButton extends StatefulWidget {
  final double width;
  final double height;
  final String text;

  final VoidCallback? onPressed;
  BangButton({
    this.width = 200,
    this.height = 50,
    Key? key,
    required this.text,
    this.onPressed,
  }) : super(key: key);

  @override
  _BangButtonState createState() => _BangButtonState();
}

class _BangButtonState extends State<BangButton> {
  @override
  Widget build(BuildContext context) {
    return ElevatedButton(
      onPressed: widget.onPressed,
      style: ElevatedButton.styleFrom(
          padding: EdgeInsets.zero,
          elevation: 5,
          shadowColor: BangColors.buttonShadowColor,
          shape:
              RoundedRectangleBorder(borderRadius: BorderRadius.circular(200))),
      child: Ink(
        decoration: BoxDecoration(
            gradient: widget.onPressed != null
                ? BangColors.buttonGradient
                : BangColors.disabledButtonGradient,
            borderRadius: BorderRadius.circular(200)),
        child: Container(
          width: widget.width,
          height: widget.height,
          alignment: Alignment.center,
          child: Text(
            widget.text,
          ),
        ),
      ),
    );
  }
}
