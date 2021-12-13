import 'dart:async';
import 'dart:developer';

import 'package:bang/core/app_constants.dart';
import 'package:bang/models/card_dto.dart';
import 'package:bang/models/card_id_dto.dart';
import 'package:bang/models/draw_option_dto.dart';
import 'package:bang/models/game_event_dto.dart';
import 'package:bang/models/game_start_dto.dart';
import 'package:bang/models/life_point_dto.dart';
import 'package:bang/models/message_dto.dart';
import 'package:bang/models/option_command.dart';
import 'package:bang/models/option_dto.dart';
import 'package:bang/models/player_died_dto.dart';
import 'package:bang/models/winner_is_dto.dart';
import 'package:bang/services/game_service.dart';
import 'package:bang/services/shared_preference_service.dart';
import 'package:get/get.dart';
import 'package:signalr_core/signalr_core.dart';

class GameProvider extends GetConnect {
  late GameService service;

  @override
  void onInit() {
    service = Get.find();
    super.onInit();
  }

  var statusInterval = Duration(seconds: 6);

  Timer? statusTimer;

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
    service.connectionInitialized = true;
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
    });
    _connection.onreconnecting((exception) {
      log(exception.toString());
    });
    _connection.onclose((exception) {
      log('$exception');
      statusTimer?.cancel();
    });

    _connection.on(
      'SetDiscardPile',
      (card) => service.onSetDiscardPile(
        CardDto.fromJson(card?[0]),
      ),
    );

    _connection.on(
      'SetWeapon',
      (weaponData) => service.onSetWeapon(
        weaponData![0] as String,
        CardDto.fromJson(
          weaponData[1],
        ),
      ),
    );

    _connection.on(
      'SetLifePoint',
      (lifePoint) => service.onSetLifePoint(
        LifePointDto.fromJson(lifePoint?[0]),
      ),
    );

    _connection.on(
      'CardsDroped',
      (cards) => service.onCardsDropped(
        (cards?[0] as List).map((e) => CardIdDto.fromJson(e)).toList(),
      ),
    );

    _connection.on(
      'CardUnequiped',
      (card) => service.onCardUnequipped(
        CardIdDto.fromJson(
          card?[0],
        ),
      ),
    );

    _connection.on(
      'CardEquiped',
      (card) => service.onCardsEquipped(
        card![0] as String,
        CardDto.fromJson(
          card[1],
        ),
      ),
    );
    _connection.on(
      'ShowCard',
      (card) => service.onShowCard(
        CardDto.fromJson(card?[0]),
      ),
    );

    _connection.on(
      'TurnStarted',
      (userId) => service.onNextTurn(
        userId?[0],
      ),
    );

    _connection.on(
      'CardsReceieved',
      (cards) {
        log('${cards.toString()}');
        service.onCardsReceived(
          (cards?.first as List).map((e) => CardDto.fromJson(e)).toList(),
        );
      },
    );
    _connection.on(
      'CardsAdded',
      (cards) {
        log('${cards.toString()}');
        service.onCardsAdded(
            (cards?.first as List).map((e) => CardIdDto.fromJson(e)).toList());
      },
    );
    _connection.on(
      'SetGameEvent',
      (gameEvent) => service.onSetGameEvent(
        GameEventDto.fromJson(gameEvent?[0]),
      ),
    );
    _connection.on(
      'OnStatus',
      (_) => _onStatus(),
    );

    _connection.on(
      'MessageRecieved',
      (message) => service.onRecieveMessage(
        MessageDto.fromJson(
          message?[0],
        ),
      ),
    );

    _connection.on(
      'ShowOption',
      (option) => service.onShowOption(
        OptionDto.fromJson(
          option?[0],
        ),
      ),
    );

    _connection.on(
      'DrawOption',
      (option) => service.onDrawOption(
        DrawOptionDto.fromJson(
          option?[0],
        ),
      ),
    );

    _connection.on(
      'GameJoined',
      (gameStartDto) => service.onGameJoined(
        GameStartDto.fromJson(
          gameStartDto?[0],
        ),
      ),
    );
    _connection.on(
      'PlayerDied',
      (playerDiedDto) => service.onPlayerDied(
        PlayerDiedDto.fromJson(
          playerDiedDto?[0],
        ),
      ),
    );

    _connection.on(
      'WinnerIs',
      (winnerIs) => service.onWinnerIs(
        WinnerIsDto.fromJson(
          winnerIs?[0],
        ),
      ),
    );
  }

  Future<void> sendMessage({
    required String message,
  }) async {
    await _connection.invoke('SendMessage', args: [
      service.gameId,
      message,
    ]);
  }

  Future<void> cardOption(String cardId) async {
    await _connection.invoke('CardOption', args: [cardId, service.gameId]);
  }

  void _startStatus() {
    _status();
    statusTimer = Timer.periodic(statusInterval, (_) => _status());
  }

  void _status() => _connection.invoke('Status', args: [service.gameName]);

  void _onStatus() {
    log('SIGNALR Status recieved -- GAME');
  }

  Future<void> discard(List<String> discardedIds) async =>
      _connection.invoke('Discard', args: [service.gameId, discardedIds]);

  Future<void> disconnect() async {
    statusTimer?.cancel();
    if (service.connectionInitialized) {
      await _connection.stop();
      service.connectionInitialized = false;
    }
  }

  Future<void> joinGame() async {
    _startStatus();
    return await _connection.invoke('JoinGame', args: [service.gameId]);
  }

  Future<void> drawReact(OptionCommand option) async {
    _startStatus();
    return await _connection.invoke('DrawReact', args: [
      service.gameId,
      option.toJson(),
    ]);
  }

  Future<void> playCard(OptionCommand option, String? playedCardId) async {
    return await _connection.invoke('ActiveCard', args: [
      service.gameId,
      playedCardId,
      option.toJson(),
    ]);
  }

  Future<void> nextTurn() async =>
      await _connection.invoke('NextTurn', args: [service.gameId]);

  Future<void> answerCard(OptionCommand option) async {
    await _connection.invoke('AnswearCard', args: [
      service.gameId,
      option.toJson(),
    ]);
  }

  @override
  void onClose() async {
    disconnect();
    super.onClose();
  }
}
