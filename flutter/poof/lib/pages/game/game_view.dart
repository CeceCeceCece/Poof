import 'dart:developer' as Dev;
import 'dart:math';

import 'package:bang/core/app_colors.dart';
import 'package:bang/core/helpers/card_helpers.dart';
import 'package:bang/models/cards/non_playable_cards/character_card.dart';
import 'package:bang/models/cards/non_playable_cards/role_card.dart';
import 'package:bang/models/cards/playable_cards/action_card.dart';
import 'package:bang/models/enemy_player_dto.dart';
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
                for (int i = 0; i < 16; i++) _buildDummyCardBack(),
                PlayableCard.back(
                    isDrawPile: true,
                    extraElevation: 2,
                    canBeTargeted: controller.drawPileGlow())
              ],
            )),
      );

  Widget _buildDummyCardBack() => Transform.translate(
        offset: Offset(
            controller.random.nextInt(5) - 2, controller.random.nextInt(5) - 2),
        child: Transform.rotate(
          angle: (pi / 180) * (controller.random.nextInt(7) - 3),
          child: PlayableCard.back(
              isDrawPile: true, extraElevation: 3, canBeTargeted: false),
        ),
      );

  Widget _buildDiscardPile(double height) => Positioned(
        bottom: height / 2 - 35,
        child: Obx(
          () => controller.discardedPileTop() == null
              ? PlayableCard(
                  isDragTarget: true,
                  shadowed: true,
                  targetGlow: controller.discardPileGlow(),
                  extraElevation: 3,
                  dragOnWillAccept: (id) {
                    Dev.log(
                        'ID: ${controller.myPlayer().id}, TARGETABLE: ${controller.targetableCardIds()}');
                    return controller
                        .targetableCardIds()
                        .contains(controller.myPlayer().id);
                  },
                  dragOnAccept: (_) => controller.targetSelected(
                      targetedUserId: controller.myPlayer().id),
                  card: ActionCard(
                    range: 0,
                    background: 'bang',
                    name: 'bang',
                    value: CardValue.Five,
                    type: CardType.Action,
                    suit: CardSuit.Diamonds,
                  ),
                  canBeFocused: false,
                  scale: 0.5,
                )
              : PlayableCard(
                  isDragTarget: true,
                  dragOnWillAccept: (id) {
                    Dev.log(
                        'ID: ${controller.myPlayer().id}, TARGETABLE: ${controller.targetableCardIds()}');
                    return controller
                        .targetableCardIds()
                        .contains(controller.myPlayer().id);
                  },
                  dragOnAccept: (_) => controller.targetSelected(
                      targetedUserId: controller.myPlayer().id),
                  targetGlow: controller.discardPileGlow(),
                  extraElevation: 6,
                  card: controller.gameService
                      .mapCards([controller.discardedPileTop()!]).first,
                  canBeFocused: true,
                  scale: 0.5,
                  highlightMultiplier: 1.3,
                ),
        ),
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
          Obx(() => Positioned(
              child: EnemyPlayer(
                top: true, left: true,
                dragOnCharacterWillAccept: (id) {
                  Dev.log(
                      'ID: $id, TARGETABLE: ${controller.targetableCardIds()}');
                  return controller.targetableCardIds().contains(id);
                },
                dragOnCharacterAccept: (id) =>
                    controller.targetSelected(targetedUserId: id),
                cardIds: controller.enemyPlayers()[0].cardIds,
                cardAmount: controller.enemyPlayers()[0].cardIds.length,
                characterName: controller.enemyPlayers()[0].characterName,
                isDead: controller.enemyPlayers()[0].isDead,
                role: controller.enemyPlayers()[0].role,
                equipment: controller
                    .enemyPlayers()[0]
                    .equipment
                    .map((e) => PlayableCard(
                          isDragTarget: true,
                          dragOnWillAccept: (_) {
                            Dev.log(
                                'ID: ${e.id}, TARGETABLE: ${controller.targetableCardIds()}');
                            var enemy = controller.enemyPlayers().firstWhere(
                                (player) => player.equipment
                                    .map((e) => e.id)
                                    .contains(e.id),
                                orElse: () => EnemyPlayerDto(
                                      cardIds: [],
                                      characterName: '',
                                      equipment: [],
                                      health: 0,
                                      isSheriff: false,
                                      playerId: '',
                                      playerName: '',
                                      temporaryEffects: [],
                                    ));
                            return controller
                                .targetableCardIds()
                                .contains(enemy.playerId);
                          },
                          dragOnAccept: (id) =>
                              controller.targetSelected(targetedCardId: id),
                          card: e,
                          canBeFocused: true,
                          scale: 0.25,
                          highlightMultiplier: 1.5,
                        ))
                    .toList(), //controller.equipmentCards,
                health: controller.enemyPlayers()[0].health,
                playerName: controller.enemyPlayers()[0].playerName,
                isSheriff: controller.enemyPlayers()[0].isSheriff,
                playerId: controller.enemyPlayers()[0].playerId,

                temporaryEffects: controller
                    .enemyPlayers()[0]
                    .temporaryEffects
                    .map((e) => PlayableCard(
                          isDragTarget: true,
                          dragOnWillAccept: (id) {
                            Dev.log(
                                'ID: ${e.id}, TARGETABLE: ${controller.targetableCardIds()}');
                            var enemy = controller.enemyPlayers().firstWhere(
                                (player) => player.temporaryEffects
                                    .map((e) => e.id)
                                    .contains(e.id),
                                orElse: () => EnemyPlayerDto(
                                      cardIds: [],
                                      characterName: '',
                                      equipment: [],
                                      health: 0,
                                      isSheriff: false,
                                      playerId: '',
                                      playerName: '',
                                      temporaryEffects: [],
                                    ));
                            return controller
                                .targetableCardIds()
                                .contains(enemy.playerId);
                          },
                          dragOnAccept: (id) =>
                              controller.targetSelected(targetedCardId: id),
                          card: e,
                          canBeFocused: true,
                          scale: 0.25,
                          highlightMultiplier: 1.5,
                        ))
                    .toList(),
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
              left: width / 2 - 100)),
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
                    .equipment
                    .map((e) => PlayableCard(
                          card: e,
                          canBeFocused: true,
                          scale: 0.25,
                          highlightMultiplier: 1.5,
                        ))
                    .toList(), //controller.equipmentCards,

                health: controller.enemyPlayers()[0].health,
                playerName: controller.enemyPlayers()[0].playerName,
                isSheriff: controller.enemyPlayers()[0].isSheriff,
                playerId: controller.enemyPlayers()[0].playerId,
                temporaryEffects: controller
                    .enemyPlayers()[0]
                    .temporaryEffects
                    .map((e) => PlayableCard(
                          card: e,
                          canBeFocused: true,
                          scale: 0.25,
                          highlightMultiplier: 1.5,
                        ))
                    .toList(),
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
                    .equipment
                    .map((e) => PlayableCard(
                          card: e,
                          canBeFocused: true,
                          scale: 0.25,
                          highlightMultiplier: 1.5,
                        ))
                    .toList(), //controller.equipmentCards,
                health: controller.enemyPlayers()[1].health,
                playerName: controller.enemyPlayers()[1].playerName,
                isSheriff: controller.enemyPlayers()[1].isSheriff,
                playerId: controller.enemyPlayers()[1].playerId,
                temporaryEffects: controller
                    .enemyPlayers()[1]
                    .temporaryEffects
                    .map((e) => PlayableCard(
                          card: e,
                          canBeFocused: true,
                          scale: 0.25,
                          highlightMultiplier: 1.5,
                        ))
                    .toList(),
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
    return Obx(() {
      var isTakingNextAction =
          controller.nextActionPlayerId() == controller.myPlayer().id;
      return Align(
        alignment: Alignment.bottomCenter,
        child: Player(
          dragOnWillAccept: (id) {
            Dev.log(
                'ID: ${controller.myPlayer().id}, TARGETABLE: ${controller.targetableCardIds()}');
            return controller
                .targetableCardIds()
                .contains(controller.myPlayer().id);
          },
          dragOnAccept: (_) => controller.targetSelected(
              targetedUserId: controller.myPlayer().id),
          discard: controller.discard,
          nextTurn: controller.nextTurn,
          health: controller.myPlayer().health,
          characterCard: CharacterCard(
              background: controller.myPlayer().characterName,
              health: controller.myPlayer().health,
              name: controller.myPlayer().characterName),
          roleCard: RoleCard(role: controller.myPlayer().role),
          cardsInHand: _mapCards(isTakingNextAction),
          handDoubleTap: controller.toggleExpandedHand,
          highlightedIndexInHand: controller.highlightedIndex(),
          isEquipmentViewExpanded: controller.isEquipmentViewExpanded(),
          isHandViewExpanded: controller.isHandExpanded() || isTakingNextAction,
          reactionCallback:
              isTakingNextAction ? controller.answerWithCard : null,
          equipment: controller.myPlayer().equipment,
          temporaryEffects: controller.myPlayer().temporaryEffects,
          toggleEquipmentView: controller.toggleEquipmentView,
          currentRoundGlow:
              controller.currentlyHasRound() == controller.myPlayer().id,
          nextActionGlow: isTakingNextAction,
          targetGlow:
              controller.targetableCardIds().contains(controller.myPlayer().id),
          dragOnEquipmentWillAccept: (id) {
            return controller
                .targetableCardIds()
                .contains(controller.myPlayer().id);
          },
          dragOnEquipmentAccept: (id) => controller.targetSelected(
            targetedCardId: id,
            targetedUserId: controller.myPlayer().id,
          ),
        ),
      );
    });
  }

  List<Widget> _mapCards([bool isTakingNextAction = false]) {
    Dev.log('DATA: ' + controller.targetableCardIds().toString());
    var cards = controller.myPlayer().cards;
    return [
      for (int i = 0; i < cards.length; i++)
        PlayableCard(
          scale: 0.85,
          card: cards[i],
          canBeDragged: true,
          reactionCallback:
              isTakingNextAction ? controller.answerWithCard : null,
          targets: controller.targetableCardIds(),
          onDragStartedCallback: () => controller.highlightTargets(i),
          onDragEndedCallback: () => controller.highlightTargets(-1),
          onDragSuccessCallback: () {
            Dev.log('onSuccessCalled');
          }, //=> controller.targetSelected(cards[i].id),
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
