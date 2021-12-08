import 'dart:math';

import 'package:bang/core/helpers/card_helpers.dart';
import 'package:bang/models/cards/non_playable_cards/non_playable_card_base.dart';
import 'package:bang/models/role_type.dart';
import 'package:flutter/material.dart';

class NonPlayableCard extends StatefulWidget {
  final NonPlayableCardBase card;

  final double scale;
  final double highlightMultiplier;
  final bool canBeFocused;
  final bool currentRoundGlow;
  final bool nextActionGlow;
  final bool targetGlow;
  final bool showBackPermanently;
  final bool Function(String?)? dragOnWillAccept;
  final void Function(String?)? dragOnAccept;
  final bool isEnemyPlayer;
  final RoleType? role;
  final bool isDead;
  const NonPlayableCard({
    Key? key,
    required this.card,
    this.dragOnWillAccept,
    this.role,
    this.dragOnAccept,
    this.canBeFocused = true,
    this.onTapCallback,
    this.isDead = false,
    this.isEnemyPlayer = false,
    this.scale = 1.0,
    this.highlightMultiplier = 1.0,
    this.showBackPermanently = false,
    this.currentRoundGlow = false,
    this.nextActionGlow = false,
    this.targetGlow = false,
  }) : super(key: key);

  final void Function()? onTapCallback;

  @override
  _BangCardWidgetState createState() => _BangCardWidgetState();
}

class _BangCardWidgetState extends State<NonPlayableCard>
    with TickerProviderStateMixin {
  bool showBack = false;
  double downSizeRatio = 0.4;
  late double height = CardHelpers.cardHeight * downSizeRatio * widget.scale;
  late double width = CardHelpers.cardWidth * downSizeRatio * widget.scale;
  final _cardFlipDuration = Duration(milliseconds: 300);
  final _cardFocusingDuration = Duration(milliseconds: 100);
  bool isElevated = false;
  double angle = 0;
  String? assetName;

  void _toggleCardFocus() => setState(() {
        if (isElevated) {
          height *= 2 / 3 / widget.highlightMultiplier;
          width *= 2 / 3 / widget.highlightMultiplier;
          isElevated = false;
        } else {
          height *= 1.5 * widget.highlightMultiplier;
          width *= 1.5 * widget.highlightMultiplier;
          isElevated = true;
        }
      });

  void _flipCard() => setState(() {
        angle = (angle + pi) % (2 * pi);
      });

  List<BoxShadow> _setGlow() {
    if (isElevated) return [];
    if (widget.targetGlow)
      return [
        BoxShadow(
          color: Colors.red.shade700,
          spreadRadius: 1,
          blurRadius: 5,
        ),
        BoxShadow(
          color: Colors.red.shade700,
          spreadRadius: -1,
          blurRadius: 5,
        )
      ];
    if (widget.nextActionGlow)
      return [
        BoxShadow(
          color: Colors.amber.shade500,
          spreadRadius: 1,
          blurRadius: 5,
        ),
        BoxShadow(
          color: Colors.amber.shade500,
          spreadRadius: -1,
          blurRadius: 5,
        )
      ];
    if (widget.currentRoundGlow)
      return [
        BoxShadow(
          color: Colors.amber.shade100,
          spreadRadius: 4,
          blurRadius: 10,
        ),
        BoxShadow(
          color: Colors.amber.shade100,
          spreadRadius: -4,
          blurRadius: 5,
        )
      ];
    return [];
  }

  @override
  Widget build(BuildContext context) {
    angle = widget.isDead ? pi : 0;
    return GestureDetector(
      onTap: widget.onTapCallback,
      onLongPressStart: (_) => widget.canBeFocused ? _toggleCardFocus() : {},
      onLongPressEnd: (_) => widget.canBeFocused ? _toggleCardFocus() : {},
      //onDoubleTap: _flipCard,
      child: TweenAnimationBuilder(
        tween: Tween<double>(begin: 0, end: angle),
        duration: _cardFlipDuration,
        builder: (BuildContext context, double val, __) {
          _computeShowBack(val);
          return Transform(
            alignment: Alignment.center,
            transform: Matrix4.identity()
              ..setEntry(3, 2, 0.001)
              ..rotateY(val),
            child: DragTarget(
              onWillAccept: (_) => widget.dragOnWillAccept?.call(null) ?? false,
              onAccept: (data) => widget.dragOnAccept?.call(null),
              builder: (
                BuildContext context,
                List<dynamic> accepted,
                List<dynamic> rejected,
              ) {
                return showBack
                    ? Material(
                        borderRadius: BorderRadius.circular(10 * widget.scale),
                        elevation: isElevated ? 40 : 0,
                        child: AnimatedContainer(
                          height: height,
                          width: width,
                          decoration: BoxDecoration(
                              borderRadius:
                                  BorderRadius.circular(10 * widget.scale),
                              boxShadow: _setGlow()),
                          duration: _cardFocusingDuration,
                          child: render(),
                        ),
                      )
                    : Material(
                        borderRadius: BorderRadius.circular(10),
                        elevation: isElevated ? 40 : 0,
                        child: Transform(
                          alignment: Alignment.center,
                          transform: Matrix4.identity()..rotateY(pi),
                          child: AnimatedContainer(
                              decoration: BoxDecoration(
                                  borderRadius:
                                      BorderRadius.circular(10 * widget.scale),
                                  boxShadow: _setGlow()),
                              height: height,
                              width: width,
                              duration: _cardFocusingDuration,
                              child: render(showBack: true)),
                        ),
                      );
              },
            ),
          );
        },
      ),
    );
  }

  void _computeShowBack(double val) {
    if (widget.showBackPermanently) {
      showBack = false;
      return;
    }
    if (val >= (pi / 2)) {
      showBack = false;
      assetName = widget.card.name;
    } else {
      showBack = true;
      assetName = widget.role?.asString;
    }
  }

  Widget render({bool showBack = false}) {
    return !widget.showBackPermanently
        ? (!showBack
            ? CardHelpers.getAsset(
                name: widget.card.name,
                type: widget.card.type,
              )
            : CardHelpers.getAsset(
                name: widget.role?.asString ?? 'sheriff', type: CardType.Role))
        : CardHelpers.getCardBack(widget.card.type, widget.scale);
  }
}
