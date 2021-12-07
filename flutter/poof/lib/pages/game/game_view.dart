import 'dart:math';

import 'package:bang/core/app_colors.dart';
import 'package:bang/core/helpers/card_helpers.dart';
import 'package:bang/models/cards/non_playable_cards/character_card.dart';
import 'package:bang/models/cards/non_playable_cards/role_card.dart';
import 'package:bang/models/cards/playable_cards/equipment_card.dart';
import 'package:bang/pages/game/game_controller.dart';
import 'package:bang/pages/game/widgets/player.dart';
import 'package:bang/widgets/bang_background.dart';
import 'package:bang/widgets/bang_chat.dart';
import 'package:bang/widgets/playable_card.dart';
import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:get/get.dart';

import 'widgets/enemy_player.dart';

class GameView extends GetView<GameController> {
  @override
  Widget build(BuildContext context) {
    var height = MediaQuery.of(context).size.height;
    var width = MediaQuery.of(context).size.width;
    return BangBackground(
      onWillPop: controller.showBackPopupForResult,
      resizeToAvoidBottomInset: false,
      child: Center(
        child: Stack(
          alignment: Alignment.center,
          fit: StackFit.passthrough,
          children: [
            _buildDrawPile(height),
            _buildDiscardPile(height),
            ..._buildLayout(height, width),
            _buildChatButton(context),
            _buildCloseButton(),
          ],
        ),
      ),
    );
  }

  Widget _buildDrawPile(double height) => Positioned(
        bottom: height / 2 + 45,
        child: Obx(() => Stack(
              alignment: Alignment.center,
              children: [
                for (int i = 0; i < controller.drawPileAmount() / 5; i++)
                  _buildDummyCardBack(),
                PlayableCard.back(
                    isDrawPile: true,
                    extraElevation: 2,
                    canBeTargeted: false), //TODO can be targeted
                Container(
                  height: 30,
                  width: 30,
                  decoration: BoxDecoration(
                      border: Border.all(color: Colors.white, width: 1.5),
                      shape: BoxShape.circle),
                  child: Opacity(
                    opacity: 0.6,
                    child: Material(
                      shape: CircleBorder(),
                    ),
                  ),
                ),
                Text(
                  controller.drawPileAmount().toString(),
                  style: TextStyle(
                      color: Colors.white,
                      fontWeight: FontWeight.bold,
                      fontSize: 20),
                ),
              ],
            )),
      );

  Widget _buildDummyCardBack() => Transform.translate(
        offset: Offset(
            controller.random.nextInt(5) - 2, controller.random.nextInt(5) - 2),
        child: Transform.rotate(
          angle: (pi / 180) * (controller.random.nextInt(7) - 3),
          child: PlayableCard.back(
              isDrawPile: true, extraElevation: 2, canBeTargeted: false),
        ),
      );

  Widget _buildDiscardPile(double height) => Positioned(
        bottom: height / 2 - 35,
        child: Obx(() => Stack(
              children: [
                for (int i = 0; i < controller.discardedPileAmount() / 5; i++)
                  _buildDummyCardBack(),
                PlayableCard(
                  card: EquipmentCard(
                      background: 'barrel',
                      name: 'barrel',
                      value: CardValue.Ten,
                      type: CardType.Equipment,
                      suit: CardSuit.Diamonds),
                  canBeFocused: true,
                  scale: 0.5,
                  highlightMultiplier: 1.3,
                ),
              ],
            )),
      );

  Widget _buildChatButton(BuildContext context) => Positioned(
        top: 5,
        left: 20,
        child: IconButton(
          iconSize: 28,
          onPressed: () => _buildChat(context),
          icon: Icon(
            Icons.chat,
            color: AppColors.background,
          ),
        ),
      );

  Widget _buildCloseButton() => Positioned(
        top: 5,
        right: 20,
        child: IconButton(
          iconSize: 28,
          onPressed: () async {
            var shouldClose = await controller.showBackPopupForResult();
            if (shouldClose) {
              Get.back();
            }
          },
          icon: Icon(
            Icons.close,
            color: AppColors.background,
          ),
        ),
      );

  List<Widget> _buildLayout(double height, double width) {
    switch (controller.playerNumber()) {
      case 1:
        return [
          _buildPlayer(),
        ];
      case 2:
        return [
          Positioned(
              child: EnemyPlayer(
                cardIds: controller.enemyPlayers()[0].cardIds,
                cardAmount: controller.enemyPlayers()[0].cardIds.length,
                characterName: controller.enemyPlayers()[0].characterName,
                equipment: controller
                    .enemyPlayers()[0]
                    .equipment, //controller.equipmentCards,
                health: controller.enemyPlayers()[0].health,
                playerName: controller.enemyPlayers()[0].playerName,
                isSheriff: controller.enemyPlayers()[0].isSheriff,
                id: controller.enemyPlayers()[0].playerId,

                temporaryEffects: controller
                    .enemyPlayers()[0]
                    .temporaryEffects, //controller.temporaryEffectCards,
                isTakingNextAction: controller.nextActionPlayerId() ==
                    controller.enemyPlayers()[0].playerId,
                canBeTargeted: controller
                    .targetableCardIds()
                    .contains(controller.enemyPlayers()[0].playerId),
                currentlyHasRound: controller.currentlyHasRound() ==
                    controller.enemyPlayers()[0].playerId,
                hasTargetableCard: controller
                    .enemyPlayers()[0]
                    .cardIds
                    .any((id) => controller.targetableCardIds().contains(id)),
              ),
              top: 40,
              left: width / 2 - 100),
          _buildPlayer(),
        ];
      case 3:
        return [
          Positioned(
              child: EnemyPlayer(
                left: true,
                cardIds: controller.enemyPlayers()[0].cardIds,

                cardAmount: controller.enemyPlayers()[0].cardIds.length,
                characterName: controller.enemyPlayers()[0].characterName,
                equipment: controller
                    .enemyPlayers()[0]
                    .equipment, //controller.equipmentCards,
                health: controller.enemyPlayers()[0].health,
                playerName: controller.enemyPlayers()[0].playerName,
                isSheriff: controller.enemyPlayers()[0].isSheriff,
                id: controller.enemyPlayers()[0].playerId,
                temporaryEffects: controller.enemyPlayers()[0].temporaryEffects,
              ),
              top: height * 0.38,
              left: 10),
          Positioned(
              child: EnemyPlayer(
                right: true,
                cardIds: controller.enemyPlayers()[1].cardIds,
                cardAmount: controller.enemyPlayers()[1].cardIds.length,
                characterName: controller.enemyPlayers()[1].characterName,
                equipment: controller
                    .enemyPlayers()[1]
                    .equipment, //controller.equipmentCards,
                health: controller.enemyPlayers()[1].health,
                playerName: controller.enemyPlayers()[1].playerName,
                isSheriff: controller.enemyPlayers()[1].isSheriff,
                id: controller.enemyPlayers()[1].playerId,
                temporaryEffects: controller.enemyPlayers()[1].temporaryEffects,
              ),
              top: height * 0.38,
              right: 10),
          _buildPlayer(),
        ];
      case 4:
        return _buildFourPlayerLayout(width: width, height: height);
      case 5:
        return _buildFivePlayerLayout(width: width, height: height);
      case 6:
        return _buildSixPlayerLayout(width: width, height: height);
      case 7:
        return _buildSevenPlayerLayout(width: width, height: height);
      default:
        return [
          Container(),
        ];
    }
  }

  List<Widget> _buildFourPlayerLayout(
          {required double width, required double height}) =>
      [
        Positioned(
            child: EnemyPlayer(
              top: true,
              left: true,
              cardAmount: 4,
              characterName: 'willythekid',
              equipment: controller.equipmentCards,
              health: 4,
              playerName: 'Bonjour',
              temporaryEffects: controller.temporaryEffectCards,
            ),
            top: 40,
            left: width / 2 - 100),
        Positioned(
          child: EnemyPlayer(
            left: true,
            cardAmount: 4,
            characterName: 'willythekid',
            equipment: controller.equipmentCards,
            health: 4,
            playerName: 'Bonjour',
            temporaryEffects: controller.temporaryEffectCards,
          ),
          top: height * 0.37,
          left: 10,
        ),
        Positioned(
          child: EnemyPlayer(
            right: true,
            cardAmount: 4,
            characterName: 'willythekid',
            equipment: controller.equipmentCards,
            health: 4,
            playerName: 'Bonjour',
            temporaryEffects: controller.temporaryEffectCards,
          ),
          top: height * 0.37,
          right: 10,
        ),
        _buildPlayer(),
      ];
  List<Widget> _buildFivePlayerLayout(
          {required double width, required double height}) =>
      [
        Positioned(
            child: EnemyPlayer(
              left: true,
              cardAmount: 4,
              characterName: 'willythekid',
              equipment: controller.equipmentCards,
              health: 4,
              playerName: 'Bonjour',
              temporaryEffects: controller.temporaryEffectCards,
            ),
            top: height * 0.38,
            left: 10),
        Positioned(
            child: EnemyPlayer(
              right: true,
              cardAmount: 4,
              characterName: 'willythekid',
              equipment: controller.equipmentCards,
              health: 4,
              playerName: 'Bonjour',
              temporaryEffects: controller.temporaryEffectCards,
            ),
            top: height * 0.38,
            right: 10),
        Positioned(
          child: EnemyPlayer(
            top: true,
            right: true,
            cardAmount: 4,
            characterName: 'willythekid',
            equipment: controller.equipmentCards,
            health: 4,
            playerName: 'Bonjour',
            temporaryEffects: controller.temporaryEffectCards,
          ),
          top: height * 0.1,
          left: width * 0.76 - 100,
        ),
        Positioned(
            child: EnemyPlayer(
              top: true,
              left: true,
              cardAmount: 4,
              characterName: 'willythekid',
              equipment: controller.equipmentCards,
              health: 4,
              playerName: 'Bonjour',
              temporaryEffects: controller.temporaryEffectCards,
            ),
            top: height * 0.1,
            left: width * 0.24 - 100),
        _buildPlayer(),
      ];
  List<Widget> _buildSixPlayerLayout(
          {required double width, required double height}) =>
      [
        Positioned(
            child: EnemyPlayer(
              left: true,
              cardAmount: 4,
              characterName: 'willythekid',
              equipment: controller.equipmentCards,
              health: 4,
              playerName: 'Bonjour',
              temporaryEffects: controller.temporaryEffectCards,
            ),
            top: height * 0.45,
            left: 5),
        Positioned(
            child: EnemyPlayer(
              right: true,
              cardAmount: 4,
              characterName: 'willythekid',
              equipment: controller.equipmentCards,
              health: 4,
              playerName: 'Bonjour',
              temporaryEffects: controller.temporaryEffectCards,
            ),
            top: height * 0.45,
            right: 5),
        Positioned(
            child: EnemyPlayer(
              right: true,
              cardAmount: 4,
              characterName: 'willythekid',
              equipment: controller.equipmentCards,
              health: 4,
              playerName: 'Bonjour',
              temporaryEffects: controller.temporaryEffectCards,
            ),
            top: height * 0.24,
            right: 20),
        Positioned(
            child: EnemyPlayer(
              left: true,
              cardAmount: 4,
              characterName: 'willythekid',
              equipment: controller.equipmentCards,
              health: 4,
              playerName: 'Bonjour',
              temporaryEffects: controller.temporaryEffectCards,
            ),
            top: height * 0.24,
            left: 20),
        Positioned(
          child: EnemyPlayer(
            top: true,
            left: true,
            cardAmount: 4,
            characterName: 'willythekid',
            equipment: controller.equipmentCards,
            health: 4,
            playerName: 'Bonjour',
            temporaryEffects: controller.temporaryEffectCards,
          ),
          top: height * 0.05,
          left: width / 2 - 100,
        ),
        _buildPlayer(),
      ];
  List<Widget> _buildSevenPlayerLayout(
          {required double width, required double height}) =>
      [
        Positioned(
            child: EnemyPlayer(
              left: true,
              cardAmount: 4,
              characterName: 'willythekid',
              equipment: controller.equipmentCards,
              health: 4,
              playerName: 'Bonjour',
              temporaryEffects: controller.temporaryEffectCards,
            ),
            top: height * 0.48,
            left: 5),
        Positioned(
            child: EnemyPlayer(
              right: true,
              cardAmount: 4,
              characterName: 'willythekid',
              equipment: controller.equipmentCards,
              health: 4,
              playerName: 'Bonjour',
              temporaryEffects: controller.temporaryEffectCards,
            ),
            top: height * 0.48,
            right: 5),
        Positioned(
            child: EnemyPlayer(
              right: true,
              cardAmount: 4,
              characterName: 'willythekid',
              equipment: controller.equipmentCards,
              health: 4,
              playerName: 'Bonjour',
              temporaryEffects: controller.temporaryEffectCards,
            ),
            top: height * 0.27,
            right: 20),
        Positioned(
            child: EnemyPlayer(
              left: true,
              cardAmount: 4,
              characterName: 'willythekid',
              equipment: controller.equipmentCards,
              health: 4,
              playerName: 'Bonjour',
              temporaryEffects: controller.temporaryEffectCards,
            ),
            top: height * 0.27,
            left: 20),
        Positioned(
          child: EnemyPlayer(
            top: true,
            right: true,
            cardAmount: 4,
            characterName: 'willythekid',
            equipment: controller.equipmentCards,
            health: 4,
            playerName: 'Bonjour',
            temporaryEffects: controller.temporaryEffectCards,
          ),
          top: height * 0.07,
          right: width / 3 - 125,
        ),
        Positioned(
          child: EnemyPlayer(
            top: true,
            left: true,
            cardAmount: 4,
            characterName: 'willythekid',
            equipment: controller.equipmentCards,
            health: 4,
            playerName: 'Bonjour',
            temporaryEffects: controller.temporaryEffectCards,
          ),
          top: height * 0.07,
          left: width / 3 - 125,
        ),
        _buildPlayer(),
      ];

  Widget _buildPlayer() {
    return Obx(() => Align(
          alignment: Alignment.bottomCenter,
          child: Player(
              health: controller.myPlayer().health,
              characterCard: CharacterCard(
                  background: controller.myPlayer().characterName,
                  health: controller.myPlayer().health,
                  name: controller.myPlayer().characterName),
              roleCard: RoleCard(role: controller.myPlayer().role),
              cardsInHand: _mapCards(),
              equipment: [], // controller.equipmentList,
              handDoubleTap: controller.toggleExpandedHand,
              highlightedIndexInHand: controller.highlightedIndex(),
              isEquipmentViewExpanded: controller.isEquipmentViewExpanded(),
              isHandViewExpanded: controller.isHandExpanded(),
              temporaryEffects: [], //controller.temporaryEffectList,
              toggleEquipmentView: controller.toggleEquipmentView,
              currentRoundGlow:
                  controller.currentlyHasRound() == controller.myPlayer().id,
              nextActionGlow:
                  controller.nextActionPlayerId() == controller.myPlayer().id,
              targetGlow: controller
                  .targetableCardIds()
                  .contains(controller.myPlayer().id)),
        ));
  }

  List<Widget> _mapCards() {
    var cards = controller.myPlayer().cards;
    return [
      for (int i = 0; i < cards.length; i++)
        PlayableCard(
          scale: 0.85,
          card: cards[i],
          canBeDragged: true,
          onDragStartedCallback: () => controller.highlightTargets(i),
          onDragEndedCallback: () => controller.highlightTargets(-1),
          onDragSuccessCallback: () => controller.removeCard(i),
          handCallback: () => controller.highlight(i),
          handCallbackInverse: () => controller.highlight(-1),
          targetGlow: controller.targetableCardIds().contains(cards[i].id),
        )
    ];
  }

  void _buildChat(BuildContext context) {
    showModalBottomSheet(
      isScrollControlled: true,
      backgroundColor: AppColors.background,
      elevation: 10,
      shape: RoundedRectangleBorder(borderRadius: BorderRadius.circular(30)),
      context: context,
      builder: (context) {
        return StatefulBuilder(
          builder: (BuildContext context, StateSetter setState) {
            controller.onMessageArrivedCallback = setState;

            controller.modalSheetScrollController = ScrollController();
            return BangChat(
                scrollController: controller.modalSheetScrollController,
                send: controller.sendMessage,
                textController: controller.chatTextController,
                playerName: controller.playerName,
                messages: controller.messages());
          },
        );
      },
    );
    controller.scrollToBottom();
  }
}
