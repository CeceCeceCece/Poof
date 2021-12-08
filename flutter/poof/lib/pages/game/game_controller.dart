import 'dart:developer' as Dev;
import 'dart:math';

import 'package:bang/core/helpers/card_helpers.dart';
import 'package:bang/core/lang/app_strings.dart';
import 'package:bang/models/card_dto.dart';
import 'package:bang/models/cards/playable_card_base.dart';
import 'package:bang/models/cards/playable_cards/action_card.dart';
import 'package:bang/models/cards/playable_cards/equipment_card.dart';
import 'package:bang/models/cards/playable_cards/weapon_card.dart';
import 'package:bang/models/enemy_player_dto.dart';
import 'package:bang/models/message_dto.dart';
import 'package:bang/models/my_player.dart';
import 'package:bang/models/option_command.dart';
import 'package:bang/services/audio_service.dart';
import 'package:bang/services/auth_service.dart';
import 'package:bang/services/game_service.dart';
import 'package:bang/widgets/bang_button.dart';
import 'package:bang/widgets/playable_card.dart';
import 'package:flutter/material.dart';
import 'package:get/get.dart';

class GameController extends GetxController {
  var _exitConfirmed = false;
  late RxInt playerNumber;
  var isHandExpanded = false.obs;
  var isEquipmentViewExpanded = false.obs;
  RxInt highlightedIndex = (-1).obs;
  var drawPileAmount = 80.obs;
  Random random = Random();
  var gameService = Get.find<GameService>();
  var modalSheetScrollController = ScrollController();
  var chatTextController = TextEditingController();
  StateSetter? onMessageArrivedCallback;
  var messages = <MessageDto>[].obs;
  late String playerName;

  Rx<String?> currentlyDraggedCardId = Rx(null);

  RxBool drawPileGlow = true.obs;
  RxBool discardPileGlow = false.obs;

  Rx<CardDto?> discardedPileTop = Rx(null);

  late Rx<MyPlayer> myPlayer;
  late RxList<EnemyPlayerDto> enemyPlayers;

  RxList<String> targetableCardIds = <String>[].obs;

  late RxString currentlyHasRound;

  Rx<String?> nextActionPlayerId = Rx(null);

  @override
  void onInit() {
    playerNumber = gameService.playerAmount;
    playerName = Get.find<AuthService>().player;
    messages = gameService.messages;
    enemyPlayers = gameService.enemyPlayers;
    myPlayer = gameService.myPlayer;
    currentlyHasRound = gameService.currentlyHasRound;
    nextActionPlayerId = gameService.nextActionPlayerId;
    targetableCardIds = gameService.targetableCardIds;
    discardedPileTop = gameService.discardPileTop;
    discardPileGlow = gameService.discardPileGlow;

    super.onInit();
  }

  void targetSelected({String? targetedCardId, String? targetedUserId}) {
    if (targetedCardId == null && targetedUserId == null) return;
    if (targetedUserId == null) {
      enemyPlayers().forEach((player) {
        if (player.equipment.map((e) => e.id).contains(targetedCardId!) ||
            player.temporaryEffects.map((t) => t.id).contains(targetedCardId))
          targetedUserId = player.playerId;
      });
    }
    if (targetedCardId == null && targetedUserId != myPlayer().id) {
      var cardsToSelectFrom = enemyPlayers()
          .firstWhere((player) => player.playerId == targetedUserId!)
          .cardIds;
      if (cardsToSelectFrom.isNotEmpty) {
        targetedCardId =
            cardsToSelectFrom[Random().nextInt(cardsToSelectFrom.length)];
      } else
        targetedCardId = '';
    }

    Dev.log(
        'TARGET:  $targetedCardId, TARGETED USER: $targetedUserId, PLAYED: ${currentlyDraggedCardId()}');
    gameService.playCard(
        option: OptionCommand(
            cardIds: targetedCardId == null ? [] : [targetedCardId],
            userId: targetedUserId!),
        playedCardId: currentlyDraggedCardId());
  }

  void answerWithCard(String? cardId) {
    Dev.log('REACTION: $cardId');
    gameService.answerCard(
        option: OptionCommand(
            userId: myPlayer().id, cardIds: cardId == null ? [] : [cardId]));
  }

  void sendMessage() {
    var message = chatTextController.text;
    gameService.sendMessage(message: message);
    chatTextController.clear();
  }

  void toggleExpandedHand() {
    isHandExpanded.value = !isHandExpanded();
    isEquipmentViewExpanded.value = false;
  }

  void toggleEquipmentView() =>
      isEquipmentViewExpanded.value = !isEquipmentViewExpanded();

  void highlight(int i) {
    if (isHandExpanded())
      highlightedIndex.value = i;
    else
      highlightedIndex.value = -1;
    highlightTargets();
  }

  void highlightTargets([int? index]) {
    var cardIndex = index ?? highlightedIndex();
    if (cardIndex != -1) {
      var cardId = myPlayer().cards[cardIndex].id;
      currentlyDraggedCardId.value = cardId;
      _getPossibleTargets(cardId);
    } else {
      gameService.targetableCardIds.value = [];
      gameService.discardPileGlow.value = false;
      currentlyDraggedCardId.value = null;
    }
  }

  void _getPossibleTargets(String cardId) {
    gameService.cardOption(cardId);
  }

  void scrollToBottom() {
    Future.delayed(
      Duration(milliseconds: 100),
      () {
        onMessageArrivedCallback?.call(() {});
        modalSheetScrollController.animateTo(
          modalSheetScrollController.position.maxScrollExtent + 100,
          curve: Curves.fastLinearToSlowEaseIn,
          duration: Duration(milliseconds: 300),
        );
      },
    );
  }

  /*void removeCard(int idx) async {
    handWidgets().removeAt(idx);
    handWidgets.refresh();
  }*/

  Future<bool> showBackPopupForResult() async {
    await Get.defaultDialog<bool>(
      title: AppStrings.assert_required.tr,
      onWillPop: () => Future.value(false),
      onConfirm: _exit,
      onCancel: () => Future.value(false),
      cancel: BangButton(
        text: AppStrings.cancel.tr,
        onPressed: Get.back,
        height: 35,
        width: 60,
      ),
      confirm: BangButton(
        text: AppStrings.still_exit.tr,
        onPressed: _exit,
        height: 40,
        width: 150,
        isNormal: false,
      ),
      content: Text(
        AppStrings.error_message_upon_game_exit.tr,
        textAlign: TextAlign.center,
      ),
    );
    var returnValue = _exitConfirmed;
    _exitDone();
    return Future.value(returnValue);
  }

  void _exit() {
    _exitConfirmed = true;
    Get.back();
    AudioService.playMenuSong();
  }

  void _exitDone() => _exitConfirmed = false;

  final RxList hand = [
    ActionCard(
      range: 0,
      background: 'beer',
      name: 'beer',
      suit: CardSuit.Clubs,
      value: CardValue.Ten,
      type: CardType.Action,
    ),
    WeaponCard(
      background: 'remington',
      name: 'remington',
      suit: CardSuit.Clubs,
      value: CardValue.Six,
      type: CardType.Weapon,
      range: 3,
    ),
    EquipmentCard(
      background: 'barrel',
      name: 'barrel',
      suit: CardSuit.Clubs,
      value: CardValue.Seven,
      type: CardType.Equipment,
    ),
    WeaponCard(
        background: 'volcanic',
        name: 'volcanic',
        suit: CardSuit.Clubs,
        value: CardValue.Eight,
        type: CardType.Weapon,
        range: 1),
    WeaponCard(
      background: 'remington',
      name: 'remington',
      suit: CardSuit.Clubs,
      value: CardValue.Six,
      range: 3,
      type: CardType.Weapon,
    ),
    EquipmentCard(
      background: 'barrel',
      name: 'barrel',
      suit: CardSuit.Clubs,
      value: CardValue.Seven,
      type: CardType.Equipment,
    ),
    WeaponCard(
        background: 'volcanic',
        name: 'volcanic',
        suit: CardSuit.Clubs,
        value: CardValue.Eight,
        type: CardType.Weapon,
        range: 1),
    ActionCard(
      background: 'stagecoach',
      name: 'stagecoach',
      suit: CardSuit.Clubs,
      value: CardValue.Nine,
      type: CardType.Action,
      range: 0,
    ),
    WeaponCard(
        background: 'volcanic',
        name: 'volcanic',
        suit: CardSuit.Clubs,
        value: CardValue.Eight,
        type: CardType.Weapon,
        range: 1),
    WeaponCard(
        background: 'remington',
        name: 'remington',
        suit: CardSuit.Clubs,
        value: CardValue.Six,
        type: CardType.Equipment,
        range: 3),
    EquipmentCard(
      background: 'barrel',
      name: 'barrel',
      suit: CardSuit.Clubs,
      value: CardValue.Seven,
      type: CardType.Equipment,
    ),
    WeaponCard(
        background: 'volcanic',
        name: 'volcanic',
        suit: CardSuit.Clubs,
        value: CardValue.Eight,
        type: CardType.Weapon,
        range: 1),
    ActionCard(
      background: 'stagecoach',
      name: 'stagecoach',
      suit: CardSuit.Clubs,
      value: CardValue.Nine,
      type: CardType.Action,
      range: 0,
    ),
  ].obs;

  final equipmentCards = [
    PlayableCard(
      card: EquipmentCard(
          background: 'barrel',
          name: 'barrel',
          value: CardValue.Ten,
          type: CardType.Equipment,
          suit: CardSuit.Diamonds),
      canBeFocused: true,
      scale: 0.25,
      highlightMultiplier: 1.5,
    ),
    PlayableCard(
      card: EquipmentCard(
          background: 'barrel',
          name: 'barrel',
          value: CardValue.Ten,
          type: CardType.Equipment,
          suit: CardSuit.Diamonds),
      canBeFocused: true,
      scale: 0.25,
      highlightMultiplier: 1.5,
    ),
    PlayableCard(
      card: EquipmentCard(
          background: 'barrel',
          name: 'barrel',
          value: CardValue.Ten,
          type: CardType.Equipment,
          suit: CardSuit.Diamonds),
      canBeFocused: true,
      scale: 0.25,
      highlightMultiplier: 1.5,
    ),
    PlayableCard(
      card: EquipmentCard(
          background: 'barrel',
          name: 'barrel',
          value: CardValue.Ten,
          type: CardType.Equipment,
          suit: CardSuit.Diamonds),
      canBeFocused: true,
      scale: 0.25,
      highlightMultiplier: 1.5,
    ),
  ];
  final temporaryEffectCards = [
    PlayableCard(
      card: EquipmentCard(
          background: 'dynamite',
          name: 'dynamite',
          value: CardValue.Ten,
          type: CardType.Equipment,
          suit: CardSuit.Diamonds),
      canBeFocused: true,
      scale: 0.25,
      highlightMultiplier: 1.5,
    ),
    PlayableCard(
      card: EquipmentCard(
          background: 'jail',
          name: 'jail',
          value: CardValue.Ten,
          type: CardType.Equipment,
          suit: CardSuit.Diamonds),
      canBeFocused: true,
      scale: 0.25,
      highlightMultiplier: 1.5,
    ),
  ];

  var equipmentList = <PlayableCardBase>[
    WeaponCard(
      background: 'remington',
      name: 'remington',
      suit: CardSuit.Clubs,
      value: CardValue.Six,
      range: 3,
      type: CardType.Weapon,
    ),
    WeaponCard(
      background: 'remington',
      name: 'remington',
      suit: CardSuit.Clubs,
      value: CardValue.Six,
      range: 3,
      type: CardType.Weapon,
    ),
    WeaponCard(
      background: 'remington',
      name: 'remington',
      suit: CardSuit.Clubs,
      value: CardValue.Six,
      range: 3,
      type: CardType.Weapon,
    ),
    WeaponCard(
      background: 'remington',
      name: 'remington',
      suit: CardSuit.Clubs,
      value: CardValue.Six,
      range: 3,
      type: CardType.Weapon,
    ),
  ].obs;
  var temporaryEffectList = <PlayableCardBase>[
    EquipmentCard(
      background: 'jail',
      name: 'jail',
      suit: CardSuit.Clubs,
      value: CardValue.Seven,
      type: CardType.Equipment,
    ),
    EquipmentCard(
      background: 'dynamite',
      name: 'dynamite',
      suit: CardSuit.Clubs,
      value: CardValue.Eight,
      type: CardType.Equipment,
    ),
  ].obs;

  void nextTurn() => gameService.nextTurn();

  void discard() {
    var cardAmountToDiscard = myPlayer().cards.length - myPlayer().health;
    if (cardAmountToDiscard <= 0) return;

    gameService.discard(
        myPlayer().cards.take(cardAmountToDiscard).map((e) => e.id).toList());
  }
}
