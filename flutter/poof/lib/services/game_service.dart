import 'dart:async';
import 'dart:developer';

import 'package:bang/core/helpers/card_helpers.dart';
import 'package:bang/models/card_dto.dart';
import 'package:bang/models/card_id_dto.dart';
import 'package:bang/models/cards/playable_card_base.dart';
import 'package:bang/models/cards/playable_cards/action_card.dart';
import 'package:bang/models/cards/playable_cards/equipment_card.dart';
import 'package:bang/models/cards/playable_cards/weapon_card.dart';
import 'package:bang/models/draw_option_dto.dart';
import 'package:bang/models/enemy_player_dto.dart';
import 'package:bang/models/game_event_dto.dart';
import 'package:bang/models/game_start_dto.dart';
import 'package:bang/models/life_point_dto.dart';
import 'package:bang/models/message_dto.dart';
import 'package:bang/models/my_player.dart';
import 'package:bang/models/option_command.dart';
import 'package:bang/models/option_dto.dart';
import 'package:bang/models/player_died_dto.dart';
import 'package:bang/models/winner_is_dto.dart';
import 'package:bang/network/websocket/game_provider.dart';
import 'package:bang/pages/game/game_controller.dart';
import 'package:bang/routes/routes.dart';
import 'package:bang/services/lobby_service.dart';
import 'package:bang/services/service_base.dart';
import 'package:fluttertoast/fluttertoast.dart';
import 'package:get/get.dart';
import 'package:get/get_rx/src/rx_types/rx_types.dart';

class GameService extends ServiceBase {
  Rx<String?> roomId = Rx(null);
  String? gameId;
  Rx<String?> nextActionPlayerId = Rx(null);
  String? gameName;
  Rx<int> myIndex = 0.obs;
  var playerAmount = 1.obs;
  RxString currentlyHasRound = ''.obs;
  RxList<String> targetableCardIds = <String>[].obs;
  Rx<CardDto?> discardPileTop = Rx(null);
  var discardPileGlow = false.obs;
  Rx<PlayableCardBase?> cardToShow = Rx(null);
  late GameProvider provider;
  var messages = <MessageDto>[].obs;
  bool connectionInitialized = false;

  @override
  void onInit() async {
    provider = Get.put(GameProvider());
    await provider.initWebsocket();
    super.onInit();
  }

  late Rx<MyPlayer> myPlayer;

  var enemyPlayers = <EnemyPlayerDto>[].obs;

  void onNextTurn(String currentUserId) =>
      currentlyHasRound.value = currentUserId;

  void onDrawOption(DrawOptionDto drawOption) =>
      drawReact(option: OptionCommand(cardIds: [], userId: myPlayer().id));

  void onSetDiscardPile(CardDto card) => discardPileTop.value = card;

  void discard(List<String> discardedIds) => provider.discard(discardedIds);

  void onSetWeapon(String characterId, CardDto card) {
    var name =
        card.name.removeAllWhitespace.toLowerCase().replaceAll(RegExp('!'), '');
    if (characterId == myPlayer().id) {
      myPlayer().equipment.add(EquipmentCard(
          background: name,
          id: card.id,
          name: name,
          value: card.value,
          type: card.type,
          suit: card.suite));

      myPlayer.refresh();
    } else {
      var player =
          enemyPlayers().firstWhere((enemy) => characterId == enemy.playerId);

      player.equipment.add(EquipmentCard(
          background: name,
          id: card.id,
          name: name,
          value: card.value,
          type: card.type,
          suit: card.suite));

      enemyPlayers.refresh();
    }
  }

  void onSetLifePoint(LifePointDto lifePoint) {
    if (lifePoint.characterId == myPlayer().id) {
      myPlayer().health = lifePoint.lifePoint;
      myPlayer.refresh();
    } else {
      var player = enemyPlayers()
          .firstWhere((player) => player.playerId == lifePoint.characterId);
      player.health = lifePoint.lifePoint;
      enemyPlayers.refresh();
    }
  }

  void onCardsDropped(List<CardIdDto> cards) {
    log('cards dropped' + cards.toString());
    cards.forEach((card) {
      if (card.characterId == myPlayer().id) {
        myPlayer().cards.removeWhere((myCard) => myCard.id == card.cardId);
        myPlayer().equipment.removeWhere((myCard) => myCard.id == card.cardId);
        myPlayer()
            .temporaryEffects
            .removeWhere((myCard) => myCard.id == card.cardId);
        myPlayer.refresh();
      } else {
        var enemy = enemyPlayers()
            .firstWhere((player) => player.playerId == card.characterId);
        enemy.cardIds.removeWhere((cardId) => cardId == card.cardId);
        enemy.temporaryEffects.removeWhere((temp) => temp.id == card.cardId);
        enemy.equipment.removeWhere((eq) => eq.id == card.cardId);
        enemyPlayers.refresh();
      }
    });
  }

  void onCardUnequipped(CardIdDto card) {
    if (card.characterId == myPlayer().id) {
      myPlayer().equipment.removeWhere((eq) => eq.id == card.cardId);
      myPlayer().temporaryEffects.removeWhere((temp) => temp.id == card.cardId);
      myPlayer.refresh();
    } else {
      var player = enemyPlayers()
          .firstWhere((player) => player.playerId == card.characterId);
      player.equipment.removeWhere((eq) => eq.id == card.cardId);
      player.temporaryEffects.removeWhere((temp) => temp.id == card.cardId);
      enemyPlayers.refresh();
    }
  }

  void onGameJoined(GameStartDto gameStartDto) {
    Get.offAndToNamed(Routes.GAME);
    myPlayer.value = MyPlayer(
        equipment: [],
        temporaryEffects: [],
        id: gameStartDto.selfId,
        cards: mapCards(gameStartDto.cards),
        role: gameStartDto.role,
        health: gameStartDto.lifePoint,
        characterName: gameStartDto.name.removeAllWhitespace.toLowerCase());
    playerAmount.value = gameStartDto.characters.length;
    currentlyHasRound.value = gameStartDto.sheriffId;

    var sheriffIndex = gameStartDto.characters
        .indexWhere((character) => character.userId == gameStartDto.sheriffId);
    var playersBeforeSheriff = gameStartDto.characters.sublist(0, sheriffIndex);
    var playersAfterAndIncludingSheriff =
        gameStartDto.characters.sublist(sheriffIndex);

    myIndex.value = gameStartDto.characters
        .indexWhere((character) => character.userId == gameStartDto.selfId);

    var allPlayers = [
      ...playersAfterAndIncludingSheriff,
      ...playersBeforeSheriff
    ];

    enemyPlayers.value = allPlayers
        .where((character) => character.userId != gameStartDto.selfId)
        .map((e) => EnemyPlayerDto(
              equipment: [],
              temporaryEffects: [],
              cardIds: e.cardIds,
              health: e.lifePoint,
              playerId: e.userId,
              isSheriff: e.userId == gameStartDto.sheriffId,
              playerName: e.userName,
              characterName: e.name.removeAllWhitespace.toLowerCase(),
            ))
        .toList();
  }

  void onRecieveMessage(MessageDto message) {
    log('Arrived:$message');
    messages.add(message);
    Get.find<GameController>().scrollToBottom();
  }

  void onCardsEquipped(String characterId, CardDto card) {
    var name =
        card.name.removeAllWhitespace.toLowerCase().replaceAll(RegExp('!'), '');

    if (characterId == myPlayer().id) {
      if (card.name == 'Jail' || card.name == 'Dynamite') {
        myPlayer().temporaryEffects.add(EquipmentCard(
            background: name,
            id: card.id,
            name: name,
            value: card.value,
            type: card.type,
            suit: card.suite));
      } else {
        myPlayer().equipment.add(EquipmentCard(
            background: name,
            id: card.id,
            name: name,
            value: card.value,
            type: card.type,
            suit: card.suite));
      }
      myPlayer.refresh();
    } else {
      var player =
          enemyPlayers().firstWhere((enemy) => characterId == enemy.playerId);
      if (card.name == 'Jail' || card.name == 'Dynamite') {
        player.temporaryEffects.add(EquipmentCard(
            background: name,
            id: card.id,
            name: name,
            value: card.value,
            type: card.type,
            suit: card.suite));
      } else {
        player.equipment.add(EquipmentCard(
            background: name,
            id: card.id,
            name: name,
            value: card.value,
            type: card.type,
            suit: card.suite));
      }
      enemyPlayers.refresh();
    }
  }

  void onCardsAdded(List<CardIdDto> cards) {
    log('ARRIVED: ${cards.toString()}');
    cards.forEach((card) {
      if (card.characterId != myPlayer().id)
        enemyPlayers()
            .firstWhere((player) => player.playerId == card.characterId)
            .cardIds
            .add(card.cardId);
    });
    enemyPlayers.refresh();
  }

  void onShowCard(CardDto card) {
    log('${card.name} ${card.suite} ${card.value}');
    cardToShow.value = mapCards([card]).first;
  }

  void onSetGameEvent(GameEventDto gameEvent) {
    if (gameEvent.card == null) {
      nextActionPlayerId.value = null;
      return;
    }
    nextActionPlayerId.value = gameEvent.characterId;
    if (gameEvent.characterId == myPlayer().id)
      Fluttertoast.showToast(
          msg: 'Az alábbi lapra kell reagálnod: ${gameEvent.card!.name}');

    log('GAME EVENT, CARD: ${gameEvent.card?.name}, , ID: ${gameEvent.characterId}');
  }

  void onShowOption(OptionDto optionDto) {
    log('${optionDto.possibleTargets.toString()}');
    var possibleTargets = optionDto.possibleTargets;

    var possiblePlayers = enemyPlayers()
        .where((player) => possibleTargets.contains(player.playerId))
        .map((e) => e.playerId);
    if (possibleTargets.contains(myPlayer().id))
      possiblePlayers = [...possiblePlayers, myPlayer().id];
    log('${possiblePlayers.toList().toString()}');

    var possibleCards = enemyPlayers()
        .map((player) =>
            player.cardIds.where((cardId) => possibleTargets.contains(cardId)))
        .expand((i) => i);

    if (Get.find<GameController>().currentlyDraggedCardId() != null) {
      targetableCardIds.value = [
        ...possiblePlayers,
        ...possibleCards,
      ];
      if (targetableCardIds().contains(myPlayer().id))
        discardPileGlow.value = true;
    }
  }

  void onCardsReceived(List<CardDto> cards) {
    log('ARRIVED: ${cards.toString()}');
    myPlayer().cards.addAll(mapCards(cards));
    myPlayer.refresh();
  }

  void sendMessage({required String message}) async {
    if (message.isNotEmpty) {
      await provider
          .sendMessage(
            message: message,
          )
          .then(
            (value) => print('MESSAGE SENT!'),
          );
    }
  }

  void cardOption(String cardId) async => await provider.cardOption(cardId);

  void joinGame({required String gameId, required String gameName}) async {
    this.gameId = gameId;
    this.gameName = gameName;
    Get.find<LobbyService>().notInGame = false;
    provider.joinGame();
  }

  void answerCard({required OptionCommand option}) async =>
      provider.answerCard(option);

  void drawReact({required OptionCommand option}) async =>
      provider.drawReact(option);

  void nextTurn() async => provider.nextTurn();

  void playCard({required OptionCommand option, String? playedCardId}) async {
    if (playedCardId != null) provider.playCard(option, playedCardId);
  }

  List<PlayableCardBase> mapCards(List<CardDto> cards) {
    return cards.map((e) {
      var name =
          e.name.removeAllWhitespace.toLowerCase().replaceAll(RegExp('!'), '');
      switch (e.type) {
        case CardType.Action:
          return ActionCard(
              id: e.id,
              range: 0,
              background: name,
              name: name,
              value: e.value,
              type: e.type,
              suit: e.suite);
        case CardType.Equipment:
          return EquipmentCard(
              id: e.id,
              background: name,
              name: name,
              value: e.value,
              type: e.type,
              suit: e.suite);
        case CardType.Weapon:
          return WeaponCard(
              id: e.id,
              range: 0,
              background: name,
              name: name,
              value: e.value,
              type: e.type,
              suit: e.suite);

        //! default cases
        case CardType.Character:
          return WeaponCard(
              range: 0,
              background: name,
              name: name,
              value: e.value,
              type: e.type,
              suit: e.suite);
        case CardType.Role:
          return WeaponCard(
              range: 0,
              id: e.id,
              background: name,
              name: name,
              value: e.value,
              type: e.type,
              suit: e.suite);
        case CardType.Back:
          return WeaponCard(
              range: 0,
              id: e.id,
              background: name,
              name: name,
              value: e.value,
              type: e.type,
              suit: e.suite);
      }
    }).toList();
  }

  void onPlayerDied(PlayerDiedDto playerDied) {
    if (playerDied.userId == myPlayer().id) {
      myPlayer().health = 0;
      myPlayer().equipment.clear();
      myPlayer().temporaryEffects.clear();
      myPlayer().cards.clear();
      myPlayer.refresh();
    } else {
      var player = enemyPlayers()
          .firstWhere((player) => player.playerId == playerDied.userId);
      player.health = 0;
      player.role = playerDied.role;
      player.equipment.clear();
      player.isDead = true;
      player.temporaryEffects.clear();
      player.cardIds.clear();
      myPlayer.refresh();
      enemyPlayers.refresh();
    }
  }

  Future<void> reconnect() async {
    await provider.disconnect();
    await provider.initWebsocket();
  }

  void onWinnerIs(WinnerIsDto winnerIs) async {
    log('winner is ${winnerIs.winner.toString()}');
    Fluttertoast.showToast(msg: 'Győztek a ${winnerIs.winner.toString()}');
    Future.delayed(
        Duration(
          seconds: 3,
        ), () async {
      provider.disconnect();
      Get.offAndToNamed(Routes.HOME);
    });
  }
}
