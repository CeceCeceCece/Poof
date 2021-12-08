import 'dart:async';
import 'dart:developer';

import 'package:bang/core/app_constants.dart';
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
import 'package:bang/models/role_type.dart';
import 'package:bang/models/winner_is_dto.dart';
import 'package:bang/pages/game/game_controller.dart';
import 'package:bang/routes/routes.dart';
import 'package:bang/services/lobby_service.dart';
import 'package:bang/services/service_base.dart';
import 'package:bang/services/shared_preference_service.dart';
import 'package:fluttertoast/fluttertoast.dart';
import 'package:get/get.dart';
import 'package:get/get_rx/src/rx_types/rx_types.dart';
import 'package:signalr_core/signalr_core.dart';

class GameService extends ServiceBase {
  Rx<String?> roomId = Rx(null);
  String? gameId;
  Rx<String?> nextActionPlayerId = Rx(null);
  String? gameName;
  var playerAmount = 1.obs;
  RxString currentlyHasRound = ''.obs;
  RxList<String> targetableCardIds = <String>[].obs;
  Rx<CardDto?> discardPileTop = Rx(null);
  var discardPileGlow = false.obs;
  Rx<PlayableCardBase?> cardToShow = Rx(null);

  var myPlayer = MyPlayer(
    id: '',
    cards: [],
    equipment: [],
    temporaryEffects: [],
    role: RoleType.Renegade,
    health: 0,
    characterName: 'kitcarlson',
  ).obs;
  var enemyPlayers = <EnemyPlayerDto>[].obs;

  var statusInterval = Duration(seconds: 6);

  Timer? statusTimer;
  var messages = <MessageDto>[].obs;
  var _connectionInitialized = false;
  late HubConnection _connection;

  Future<void> initWebsocket() async {
    _connection = HubConnectionBuilder()
        .withUrl(
          AppConstants.BASE_URL + AppConstants.GAME_HUB,
          HttpConnectionOptions(
            transport: HttpTransportType.longPolling,
            logging: (level, message) => print('GAME SIGNALR ---- $message'),
            accessTokenFactory: () async => SharedPreferenceService.token,
          ),
        )
        .withHubProtocol(JsonHubProtocol())
        .withAutomaticReconnect(
          DefaultReconnectPolicy(
            retryDelays: [
              0,
              2000,
              10000,
              30000,
              60000,
              60000,
              60000,
              null,
            ],
          ),
        )
        .build();
    _connectionInitialized = true;
    try {
      await _connection.start()?.then((value) {
        log(_connection.state.toString());
      });
    } catch (error) {
      log('$error');
      _connection.start();
    }
    _connection.onreconnected((connectionId) {
      log('RECONNECTED');
      //statusTimer = Timer.periodic(statusInterval, (_) => _status());
    });
    _connection.onreconnecting((exception) {
      log(exception.toString());
    });
    _connection.onclose((exception) {
      log('$exception');
      statusTimer?.cancel();
    });

    _connection.on(
      'SetDeck',
      (cards) => _setDeck(
        cards?[0].map((e) => CardDto.fromJson(e)).toList() ?? [],
      ),
    );

    _connection.on(
      'SetEquipedDeck',
      (cards) => _setEquippedDeck(
        cards?[0].map((e) => CardDto.fromJson(e)).toList() ?? [],
      ),
    );

    _connection.on(
      'SetDiscardPile',
      (card) => _setDiscardPile(
        CardDto.fromJson(card?[0]),
      ),
    );

    _connection.on(
      'SetWeapon',
      (weaponData) => _setWeapon(
        weaponData![0] as String,
        CardDto.fromJson(
          weaponData[1],
        ),
      ),
    );

    _connection.on(
      'SetLifePoint',
      (lifePoint) => _setLifePoint(
        LifePointDto.fromJson(lifePoint?[0]),
      ),
    );

    _connection.on(
      'CardsDroped',
      (cards) => _cardsDropped(
        (cards?[0] as List).map((e) => CardIdDto.fromJson(e)).toList(),
      ),
    );

    _connection.on(
      'CardUnequiped',
      (card) => _cardUnequipped(
        CardIdDto.fromJson(
          card?[0],
        ),
      ),
    );

    _connection.on(
      'CardEquiped',
      (card) => _cardsEquipped(
        card![0] as String,
        CardDto.fromJson(
          card[1],
        ),
      ),
    );
    _connection.on(
      'ShowCard',
      (card) => _showCard(
        CardDto.fromJson(card?[0]),
      ),
    );

    _connection.on(
      'TurnStarted',
      (userId) => _nextTurn(
        userId?[0],
      ),
    );

    _connection.on(
      'CardsReceieved',
      (cards) {
        log('${cards.toString()}');
        _cardsReceived(
          (cards?.first as List).map((e) => CardDto.fromJson(e)).toList(),
        );
      },
    );
    _connection.on(
      'CardsAdded',
      (cards) {
        log('${cards.toString()}');
        _cardsAdded(
            (cards?.first as List).map((e) => CardIdDto.fromJson(e)).toList());
      },
    );
    _connection.on(
      'SetGameEvent',
      (gameEvent) => _setGameEvent(
        GameEventDto.fromJson(gameEvent?[0]),
      ),
    );
    _connection.on(
      'OnStatus',
      (_) => _onStatus(),
    );

    _connection.on(
      'MessageRecieved',
      (message) => _recieveMessage(
        MessageDto.fromJson(
          message?[0],
        ),
      ),
    );

    _connection.on(
      'ShowOption',
      (option) => _showOption(
        OptionDto.fromJson(
          option?[0],
        ),
      ),
    );

    _connection.on(
      'DrawOption',
      (option) => _drawOption(
        DrawOptionDto.fromJson(
          option?[0],
        ),
      ),
    );

    _connection.on(
      'GameJoined',
      (gameStartDto) => _onGameJoined(
        GameStartDto.fromJson(
          gameStartDto?[0],
        ),
      ),
    );
    _connection.on(
      'PlayerDied',
      (playerDiedDto) => _onPlayerDied(
        PlayerDiedDto.fromJson(
          playerDiedDto?[0],
        ),
      ),
    );

    _connection.on(
      'WinnerIs',
      (winnerIs) => _onWinnerIs(
        WinnerIsDto.fromJson(
          winnerIs?[0],
        ),
      ),
    );
  }

  void _nextTurn(String currentUserId) {
    currentlyHasRound.value = currentUserId;
    log('Turn Changed');
  }

  void _drawOption(DrawOptionDto drawOption) {
    log('draw option arrived');
    log('${drawOption.cards.toString()}');
    drawReact(option: OptionCommand(cardIds: [], userId: myPlayer().id)) /*)*/;
  }

  void _setDiscardPile(CardDto card) {
    log(card.toString());
    discardPileTop.value = card;
  }

  void discard(List<String> discardedIds) {
    _connection.invoke('Discard', args: [gameId, discardedIds]);
  }

  void _setDeck(List<CardDto> cards) {}

  void _setEquippedDeck(List<CardDto> cards) {}

  void _setWeapon(String characterId, CardDto card) {
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

  void _setLifePoint(LifePointDto lifePoint) {
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

  void _status() {
    _connection.invoke('Status', args: [gameName]);
  }

  void _onStatus() {
    log('SIGNALR Status recieved -- GAME');
  }

  void _cardsDropped(List<CardIdDto> cards) {
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

  void _cardUnequipped(CardIdDto card) {
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

  void _onGameJoined(GameStartDto gameStartDto) {
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

    enemyPlayers.value = gameStartDto.characters
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

  void _recieveMessage(MessageDto message) {
    log('Arrived:$message');
    messages.add(message);
    Get.find<GameController>().scrollToBottom();
  }

  void _cardsEquipped(String characterId, CardDto card) {
    log('cards equipped');
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

  void _cardsAdded(List<CardIdDto> cards) {
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

  void _showCard(CardDto card) {
    log('${card.name} ${card.suite} ${card.value}');
    cardToShow.value = mapCards([card]).first;
  }

  void _setGameEvent(GameEventDto gameEvent) {
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

  void _showOption(OptionDto optionDto) {
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

  void _cardsReceived(List<CardDto> cards) {
    log('ARRIVED: ${cards.toString()}');
    myPlayer().cards.addAll(mapCards(cards));
    myPlayer.refresh();
  }

  void sendMessage({required String message}) async {
    if (message.isNotEmpty) {
      await _connection.invoke('SendMessage', args: [
        gameId,
        message,
      ]).then(
        (value) => print('MESSAGE SENT!'),
      );
    }
  }

  void cardOption(String cardId) async {
    await _connection.invoke('CardOption', args: [cardId, gameId]);
  }

  void joinGame({required String gameId, required String gameName}) async {
    this.gameId = gameId;
    this.gameName = gameName;
    log('joining game ${_connection.state.toString()}');
    _status();
    statusTimer = Timer.periodic(statusInterval, (_) => _status());
    Get.find<LobbyService>().notInGame = false;
    await _connection.invoke('JoinGame', args: [gameId]);
  }

  void answerCard({required OptionCommand option}) async {
    await _connection.invoke('AnswearCard', args: [
      gameId,
      option.toJson(),
    ]).then(
      (value) => print('CARD ANSWER SENT!'),
    );
  }

  void drawReact({required OptionCommand option}) async {
    await _connection.invoke('DrawReact', args: [
      gameId,
      option.toJson(),
    ]).then(
      (value) => print('DRAW REACT SENT!'),
    );
  }

  void nextTurn() async {
    await _connection.invoke('NextTurn', args: [gameId]);
  }

  void playCard({required OptionCommand option, String? playedCardId}) async {
    if (playedCardId == null) return;
    await _connection.invoke('ActiveCard', args: [
      gameId,
      playedCardId,
      option.toJson(),
    ]).then(
      (value) => print('Card played'),
    );
  }

  Future<void> disconnect() async {
    statusTimer?.cancel();
    if (_connectionInitialized) {
      await _connection.stop();
      _connectionInitialized = false;
    }
  }

  @override
  void onClose() async {
    disconnect();
    super.onClose();
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

  void _onPlayerDied(PlayerDiedDto playerDied) {
    log('player died');
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

  void _onWinnerIs(WinnerIsDto winnerIs) async {
    log('winner is ${winnerIs.winner.toString()}');
    Fluttertoast.showToast(msg: 'Győztek a ${winnerIs.winner.toString()}');
    Future.delayed(
        Duration(
          seconds: 3,
        ), () async {
      disconnect();
      Get.offAndToNamed(Routes.HOME);
    });
  }
}
