import 'dart:math';

import 'package:animated_icon_button/animated_icon_button.dart';
import 'package:bang/cards/model/non_playable_cards/character_card.dart';
import 'package:bang/cards/model/non_playable_cards/role_card.dart';
import 'package:bang/cards/widgets/bang_card_widget.dart';
import 'package:bang/cards/widgets/card_widget_helpers.dart';
import 'package:bang/cards/widgets/non_playable_card_widget.dart';
import 'package:bang/core/app_colors.dart';
import 'package:bang/pages/game/widgets/hand.dart';
import 'package:bang/services/game_service.dart';
import 'package:flutter/material.dart';
import 'package:get/get.dart';

class PlayerView extends StatelessWidget {
  final CharacterCard characterCard;
  final RoleCard roleCard;
  final pixelPerHealth = (CardWidgetHelpers.cardHeight * 0.4 - 2 * 8) / 5;
  PlayerView({required this.characterCard, required this.roleCard});
  final service = Get.find<GameService>();
  final random = Random();

  @override
  Widget build(BuildContext context) {
    var health = 2;
    return Obx(
      () => AnimatedContainer(
        duration: Duration(milliseconds: 150),
        height: service.expandedHandView() ? 370 : 310,
        child: Stack(
          children: [
            ..._buildEquipmentView(
                !service.expandedHandView() && service.expandedEquipmentView(),
                157),
            Column(
              mainAxisAlignment: MainAxisAlignment.end,
              children: [
                Container(
                  height: service.expandedHandView() ? 350 : 310,
                  child: Row(
                    crossAxisAlignment: CrossAxisAlignment.end,
                    children: [
                      Expanded(
                        child: Container(
                          child: service.expandedHandView()
                              ? null
                              : Stack(
                                  alignment: Alignment.centerRight,
                                  fit: StackFit.passthrough,
                                  clipBehavior: Clip.antiAliasWithSaveLayer,
                                  children: [
                                      Positioned(
                                        bottom: 20,
                                        right: 2 *
                                                CardWidgetHelpers.cardWidth *
                                                0.4 +
                                            14 -
                                            health -
                                            (5 - health) * pixelPerHealth -
                                            85,
                                        child: Transform.rotate(
                                          angle: pi / 2,
                                          child: NonPlayableCardWidget(
                                            scale: 0.9,
                                            showBackPermanently: true,
                                            card: characterCard,
                                            canBeFocused: false,
                                          ),
                                        ),
                                      ),
                                      Positioned(
                                        bottom: 20,
                                        child: NonPlayableCardWidget(
                                          scale: 0.95,
                                          card: characterCard,
                                        ),
                                      ),
                                      Positioned(
                                        bottom: 20,
                                        left: 10,
                                        child: NonPlayableCardWidget(
                                          card: roleCard,
                                          scale: 0.95,
                                        ),
                                      ),
                                      Positioned(
                                        bottom: 15,
                                        left: 10,
                                        child: Material(
                                            elevation: 20,
                                            shape: CircleBorder(),
                                            clipBehavior: Clip.hardEdge,
                                            color: Colors.transparent,
                                            child: Ink(
                                              decoration: BoxDecoration(
                                                  border: Border.all(
                                                      width: 1,
                                                      color:
                                                          AppColors.darkBrown),
                                                  gradient:
                                                      AppColors.buttonGradient,
                                                  borderRadius:
                                                      BorderRadius.circular(
                                                          200)),
                                              child: Center(
                                                child: AnimatedIconButton(
                                                    size: 18,
                                                    duration: Duration(
                                                        milliseconds: 400),
                                                    onPressed: () => service
                                                            .expandedEquipmentView
                                                            .value =
                                                        !service
                                                            .expandedEquipmentView(),
                                                    icons: [
                                                      AnimatedIconItem(
                                                          icon: Icon(
                                                        Icons.arrow_upward,
                                                        size: 20,
                                                        color: Colors.white,
                                                      )),
                                                      AnimatedIconItem(
                                                          icon: Icon(
                                                              Icons
                                                                  .arrow_downward,
                                                              size: 20,
                                                              color: Colors
                                                                  .white)),
                                                    ]),
                                              ),
                                            )),
                                      )
                                    ]),
                        ),
                        flex: service.expandedHandView() ? 0 : 22,
                      ),
                      Expanded(
                        child: Stack(
                          children: [
                            ..._buildEquipmentView(
                                service.expandedEquipmentView() &&
                                    service.expandedHandView(),
                                208),
                            Obx(
                              () => Hand(
                                indexOfFocusedCard: service.highlightedIndex(),
                                cards: service.handWidgets(),
                                isExpanded: service.expandedHandView(),
                                handSize: service.handWidgets.length,
                                onDoubleTap: () {
                                  service.expandedHandView.value =
                                      !service.expandedHandView();
                                  service.expandedEquipmentView.value = false;
                                },
                              ),
                            ),
                            service.expandedHandView()
                                ? Positioned(
                                    bottom: 15,
                                    left: 10,
                                    child: Material(
                                      elevation: 20,
                                      clipBehavior: Clip.hardEdge,
                                      shape: CircleBorder(),
                                      color: Colors.transparent,
                                      child: Ink(
                                        decoration: BoxDecoration(
                                            border: Border.all(
                                                width: 1,
                                                color: AppColors.darkBrown),
                                            gradient: AppColors.buttonGradient,
                                            borderRadius:
                                                BorderRadius.circular(200)),
                                        child: Center(
                                          child: AnimatedIconButton(
                                              size: 18,
                                              duration:
                                                  Duration(milliseconds: 400),
                                              onPressed: () => service
                                                      .expandedEquipmentView
                                                      .value =
                                                  !service
                                                      .expandedEquipmentView(),
                                              icons: [
                                                AnimatedIconItem(
                                                    icon: Icon(
                                                  Icons.arrow_upward,
                                                  size: 20,
                                                  color: Colors.white,
                                                )),
                                                AnimatedIconItem(
                                                    icon: Icon(
                                                        Icons.arrow_downward,
                                                        size: 20,
                                                        color: Colors.white)),
                                              ]),
                                        ),
                                      ),
                                    ),
                                  )
                                : Container(),
                          ],
                        ),
                        flex: service.expandedHandView() ? 30 : 8,
                      )
                    ],
                  ),
                ),
              ],
            ),
          ],
        ),
      ),
    );
  }

  List<Widget> _buildEquipmentView(bool shouldShow, double bottomOffset) {
    return shouldShow
        ? [
            SizedBox(height: 140, width: 1),
            for (int i = 0; i < service.temporaryEffectList.length; i++)
              Positioned(
                left: (service.temporaryEffectList.length - 1) * 54 -
                    54 * i.toDouble() +
                    5,
                bottom: bottomOffset,
                child: Transform.rotate(
                  angle: (pi / 180) * (random.nextInt(7) - 3),
                  child: BangCardWidget(
                    card: service.temporaryEffectList[i],
                    scale: 0.55,
                    highlightMultiplier: 6.3 / 5,
                  ),
                ),
              ),
            for (int i = 0; i < service.equipmentList.length; i++)
              Positioned(
                bottom: bottomOffset,
                right: (service.equipmentList.length - 1) * 54 -
                    54 * i.toDouble() +
                    5,
                child: Transform.rotate(
                  angle: (pi / 180) * (random.nextInt(7) - 3),
                  child: BangCardWidget(
                    card: service.equipmentList[i],
                    scale: 0.55,
                    highlightMultiplier: 6.3 / 5,
                  ),
                ),
              )
          ]
        : [];
  }
}
