import 'package:bang/core/colors.dart';
import 'package:flutter/material.dart';

class BangButton extends StatefulWidget {
  final double width;
  final double height;
  final String text;
  final bool isNormal;

  final VoidCallback? onPressed;
  BangButton({
    this.isNormal = true,
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
          primary: BangColors.buttonGradientColors.last,
          elevation: widget.isNormal ? 10 : 0,
          shadowColor: widget.isNormal ? null : BangColors.buttonShadowColor,
          shape:
              RoundedRectangleBorder(borderRadius: BorderRadius.circular(200))),
      child: Ink(
        decoration: BoxDecoration(
            gradient: widget.onPressed != null
                ? BangColors.buttonGradient
                : BangColors.disabledButtonGradient,
            borderRadius: BorderRadius.circular(200)),
        child: Container(
          width: widget.width + 4,
          height: widget.height + 1,
          alignment: Alignment.center,
          decoration: widget.isNormal
              ? BoxDecoration(
                  borderRadius: BorderRadius.circular(200),
                  //color: Colors.white,
                  border: Border.all(width: 1, color: BangColors.darkBrown))
              : BoxDecoration(
                  borderRadius: BorderRadius.circular(200),
                  color: Colors.white,
                  border: Border.all(
                      width: 3, color: BangColors.buttonGradientColors.first)),
          child: Text(
            widget.text,
            style: widget.isNormal
                ? null
                : TextStyle(color: BangColors.buttonShadowColor),
          ),
        ),
      ),
    );
  }
}
