import 'package:bang/cards/model/action_cards/equipment_card.dart';
import 'package:bang/cards/model/card_constants.dart' as Bang;
import 'package:bang/cards/model/non_playable_cards/character_card.dart';
import 'package:bang/cards/model/non_playable_cards/role_card.dart';
import 'package:bang/cards/widgets/bang_card_widget.dart';
import 'package:bang/core/app_colors.dart';
import 'package:bang/core/app_constants.dart';
import 'package:bang/pages/game/game_controller.dart';
import 'package:bang/pages/game/widgets/player_view.dart';
import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:get/get.dart';

import 'widgets/other_player_view.dart';

class GameView extends GetView<GameController> {
  @override
  Widget build(BuildContext context) {
    var height = MediaQuery.of(context).size.height;
    var width = MediaQuery.of(context).size.width;
    return Container(
      decoration: BoxDecoration(
          image: DecorationImage(
              image: AssetImage(
                AppAssetPaths.backgroundPath,
              ),
              fit: BoxFit.fitHeight)),
      child: WillPopScope(
        onWillPop: controller.showBackPopupForResult,
        child: SafeArea(
          child: Scaffold(
            backgroundColor: Colors.transparent,
            body: Center(
              child: Obx(
                () => Stack(
                  alignment: Alignment.center,
                  fit: StackFit.passthrough,
                  children: [
                    Positioned(
                      bottom: height / 2 + 45,
                      child: BangCardWidget(
                        card: EquipmentCard(
                            background: 'barrel',
                            name: 'barrel',
                            value: Bang.Value.Ten,
                            type: Bang.CardType.Equipment,
                            suit: Bang.Suit.Diamonds),
                        showBackPermanently: true,
                        canBeFocused: false,
                        scale: 0.55,
                      ),
                    ),
                    Positioned(
                      bottom: height / 2 - 35,
                      child: BangCardWidget(
                        card: EquipmentCard(
                            background: 'barrel',
                            name: 'barrel',
                            value: Bang.Value.Ten,
                            type: Bang.CardType.Equipment,
                            suit: Bang.Suit.Diamonds),
                        canBeFocused: true,
                        scale: 0.55,
                      ),
                    ),
                    ..._buildLayout(height, width),
                    Positioned(
                      top: 5,
                      left: 20,
                      child: IconButton(
                        iconSize: 28,
                        onPressed: () {
                          Get.bottomSheet(
                            Container(
                                height: 200,
                                child: Column(
                                  children: [
                                    Text('Hii 1', textScaleFactor: 2),
                                    Text('Hii 2', textScaleFactor: 2),
                                    Text('Hii 3', textScaleFactor: 2),
                                    Text('Hii 4', textScaleFactor: 2),
                                  ],
                                )),
                            barrierColor: Colors.transparent,
                            isDismissible: true,
                            backgroundColor: Colors.white,
                            shape: RoundedRectangleBorder(
                              borderRadius: BorderRadius.circular(35),
                              /*side: BorderSide(
                                            width: 1, color: Colors.black)*/
                            ),
                            enableDrag: true,
                            enterBottomSheetDuration:
                                Duration(milliseconds: 300),
                            exitBottomSheetDuration:
                                Duration(milliseconds: 300),
                          );
                        },
                        icon: Icon(
                          Icons.chat,
                          color: AppColors.background,
                        ),
                      ),
                    ),
                    Positioned(
                      top: 5,
                      right: 20,
                      child: IconButton(
                        iconSize: 28,
                        onPressed: () async {
                          var shouldClose =
                              await controller.showBackPopupForResult();
                          if (shouldClose) {
                            Get.back();
                          }
                        },
                        icon: Icon(
                          Icons.close,
                          color: AppColors.background,
                        ),
                      ),
                    ),
                  ],
                ),
              ),
            ),
          ),
        ),
      ),
    );
  }

  List<Widget> _buildLayout(double height, double width) {
    switch (controller.playerNumber()) {
      case 4:
        return [
          Positioned(
              child: EnemyPlayer(
                top: true,
                left: true,
              ),
              top: 40,
              left: width / 2 - 100),
          Positioned(
            child: EnemyPlayer(
              left: true,
            ),
            top: height * 0.37,
            left: 10,
          ),
          Positioned(
            child: EnemyPlayer(
              right: true,
            ),
            top: height * 0.37,
            right: 10,
          ),
          Align(
              alignment: Alignment.bottomCenter,
              child: PlayerView(
                characterCard: CharacterCard(
                    background: 'willythekid', health: 4, name: 'willythekid'),
                roleCard: RoleCard(name: 'sheriff', background: 'sheriff'),
              )),
        ];
      case 5:
        return [
          Positioned(
              child: EnemyPlayer(
                left: true,
              ),
              top: height * 0.38,
              left: 10),
          Positioned(
              child: EnemyPlayer(
                right: true,
              ),
              top: height * 0.38,
              right: 10),
          Positioned(
            child: EnemyPlayer(
              top: true,
              right: true,
            ),
            top: height * 0.1,
            left: width * 0.76 - 100,
          ),
          Positioned(
              child: EnemyPlayer(
                top: true,
                left: true,
              ),
              top: height * 0.1,
              left: width * 0.24 - 100),
          Align(
              alignment: Alignment.bottomCenter,
              child: PlayerView(
                characterCard: CharacterCard(
                    background: 'willythekid', health: 4, name: 'willythekid'),
                roleCard: RoleCard(name: 'sheriff', background: 'sheriff'),
              )),
        ];
      case 6:
        return [
          Positioned(
              child: EnemyPlayer(left: true), top: height * 0.45, left: 5),
          Positioned(
              child: EnemyPlayer(right: true), top: height * 0.45, right: 5),
          Positioned(
              child: EnemyPlayer(right: true), top: height * 0.24, right: 20),
          Positioned(
              child: EnemyPlayer(left: true), top: height * 0.24, left: 20),
          Positioned(
            child: EnemyPlayer(top: true, left: true),
            top: height * 0.05,
            left: width / 2 - 100,
          ),
          Align(
              alignment: Alignment.bottomCenter,
              child: PlayerView(
                characterCard: CharacterCard(
                    background: 'willythekid', health: 4, name: 'willythekid'),
                roleCard: RoleCard(name: 'sheriff', background: 'sheriff'),
              )),
        ];
      case 7:
        return [
          Positioned(
              child: EnemyPlayer(left: true), top: height * 0.48, left: 5),
          Positioned(
              child: EnemyPlayer(right: true), top: height * 0.48, right: 5),
          Positioned(
              child: EnemyPlayer(right: true), top: height * 0.27, right: 20),
          Positioned(
              child: EnemyPlayer(left: true), top: height * 0.27, left: 20),
          Positioned(
            child: EnemyPlayer(top: true, right: true),
            top: height * 0.07,
            right: width / 3 - 125,
          ),
          Positioned(
            child: EnemyPlayer(top: true, left: true),
            top: height * 0.07,
            left: width / 3 - 125,
          ),
          Align(
              alignment: Alignment.bottomCenter,
              child: PlayerView(
                characterCard: CharacterCard(
                    background: 'willythekid', health: 4, name: 'willythekid'),
                roleCard: RoleCard(name: 'sheriff', background: 'sheriff'),
              )),
        ];
      default:
        return [
          Container(),
        ];
    }
  }
}
