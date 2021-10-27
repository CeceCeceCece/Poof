import 'dart:math';

import 'package:animated_icon_button/animated_icon_button.dart';
import 'package:bang/cards/model/non_playable_cards/character_card.dart';
import 'package:bang/cards/model/non_playable_cards/role_card.dart';
import 'package:bang/cards/widgets/bang_card_widget.dart';
import 'package:bang/cards/widgets/card_widget_helpers.dart';
import 'package:bang/cards/widgets/non_playable_card_widget.dart';
import 'package:bang/pages/game/widgets/hand_of_cards.dart';
import 'package:bang/services/game_service.dart';
import 'package:flutter/material.dart';
import 'package:get/get.dart';

class PlayerView extends StatelessWidget {
  final CharacterCard characterCard;
  final RoleCard roleCard;
  final pixelPerHealth = (CardWidgetHelpers.cardHeight * 0.4 - 2 * 8) / 5;
  PlayerView({required this.characterCard, required this.roleCard});
  final service = Get.find<GameService>();

  @override
  Widget build(BuildContext context) {
    var health = 5;
    return Obx(
      () => AnimatedContainer(
        duration: Duration(milliseconds: 150),
        height: service.expandedHandView() ? 310 : 230,
        child: Column(
          mainAxisAlignment: MainAxisAlignment.end,
          children: [
            !service.expandedHandView()
                ? (service.expandedEquipmentView()
                    ? Stack(
                        children: [
                          Container(
                            padding: const EdgeInsets.only(left: 10, right: 10),
                            child: Stack(
                              children: [
                                Positioned(
                                  child: Row(
                                      mainAxisAlignment:
                                          MainAxisAlignment.center,
                                      children: [
                                        ...service.temporaryEffectList
                                            .map((card) => Padding(
                                                  padding: EdgeInsets.only(
                                                      left: 3, right: 3),
                                                  child: BangCardWidget(
                                                      card: card,
                                                      scale: 0.6,
                                                      onDragSuccessCallback:
                                                          () {},
                                                      handCallback: () {}),
                                                ))
                                            .toList(),
                                        Spacer(),
                                        ...service.equipmentList
                                            .map((card) => Padding(
                                                  padding: EdgeInsets.only(
                                                      left: 3, right: 3),
                                                  child: BangCardWidget(
                                                      card: card,
                                                      scale: 0.6,
                                                      onDragSuccessCallback:
                                                          () {},
                                                      handCallback: () {}),
                                                ))
                                            .toList(),
                                      ]),
                                ),
                              ],
                            ),
                          ),
                        ],
                      )
                    : Container())
                : Container(),
            Container(
              height: service.expandedHandView()
                  ? 290
                  : (service.expandedEquipmentView() ? 150 : 230),
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
                                    right:
                                        2 * CardWidgetHelpers.cardWidth * 0.4 +
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
                                          onTapCallback: () {}),
                                    ),
                                  ),
                                  /*Row(
                                    mainAxisAlignment: MainAxisAlignment.end,
                                    children: [*/
                                  Positioned(
                                      bottom: 20,
                                      child: NonPlayableCardWidget(
                                          scale: 0.95,
                                          card: characterCard,
                                          onTapCallback: () {})),
                                  Positioned(
                                      bottom: 20,
                                      left: 10,
                                      child: NonPlayableCardWidget(
                                        card: roleCard,
                                        scale: 0.95,
                                        onTapCallback: () {},
                                      )),
                                  Positioned(
                                      bottom: 15,
                                      left: 10,
                                      child: Material(
                                        shape: CircleBorder(),
                                        color: Colors.brown,
                                        elevation: 20,
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
                                      )),
                                ]),
                      //)),
                    ),
                    flex: service.expandedHandView() ? 0 : 22,
                  ),
                  Expanded(
                    child: Stack(
                      children: [
                        service.expandedEquipmentView() &&
                                service.expandedHandView()
                            ? Container(
                                padding:
                                    const EdgeInsets.only(left: 10, right: 10),
                                child: Stack(
                                  overflow: Overflow.visible,
                                  children: [
                                    Row(
                                        mainAxisAlignment:
                                            MainAxisAlignment.center,
                                        children: [
                                          ...service.temporaryEffectList
                                              .map((card) => Container(
                                                    padding: EdgeInsets.only(
                                                        left: 3, right: 3),
                                                    child: BangCardWidget(
                                                        card: card,
                                                        scale: 0.6,
                                                        onDragSuccessCallback:
                                                            () {},
                                                        handCallback: () {}),
                                                  ))
                                              .toList(),
                                          Spacer(),
                                          ...service.equipmentList
                                              .map((card) => Container(
                                                    padding: EdgeInsets.only(
                                                        left: 3, right: 3),
                                                    child: BangCardWidget(
                                                        card: card,
                                                        scale: 0.6,
                                                        onDragSuccessCallback:
                                                            () {},
                                                        handCallback: () {}),
                                                  ))
                                              .toList(),
                                        ]),
                                  ],
                                ),
                              )
                            : Container(),
                        Hand(),
                        service.expandedHandView()
                            ? Positioned(
                                bottom: 15,
                                left: 10,
                                child: Material(
                                  shape: CircleBorder(),
                                  color: Colors.brown,
                                  elevation: 20,
                                  child: Center(
                                    child: AnimatedIconButton(
                                        size: 18,
                                        duration: Duration(milliseconds: 400),
                                        onPressed: () => service
                                                .expandedEquipmentView.value =
                                            !service.expandedEquipmentView(),
                                        icons: [
                                          AnimatedIconItem(
                                              icon: Icon(
                                            Icons.arrow_upward,
                                            size: 20,
                                            color: Colors.white,
                                          )),
                                          AnimatedIconItem(
                                              icon: Icon(Icons.arrow_downward,
                                                  size: 20,
                                                  color: Colors.white)),
                                        ]),
                                  ),
                                ))
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
      ),
    );
  }
}
