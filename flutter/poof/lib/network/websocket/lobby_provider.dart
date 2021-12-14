import 'dart:async';
import 'dart:developer';

import 'package:bang/core/app_constants.dart';
import 'package:bang/models/lobby_dto.dart';
import 'package:bang/models/message_dto.dart';
import 'package:bang/models/user_dto.dart';
import 'package:bang/services/lobby_service.dart';
import 'package:bang/services/shared_preference_service.dart';
import 'package:get/get.dart';
import 'package:signalr_core/signalr_core.dart';

class LobbyProvider extends GetConnect {
  late LobbyService service;

  var statusInterval = Duration(seconds: 6);
  Timer? statusTimer;

  @override
  void onInit() {
    service = Get.find();
    super.onInit();
  }

  late HubConnection _connection;
  Future<void> initWebsocket() async {
    _connection = HubConnectionBuilder()
        .withUrl(
          AppConstants.BASE_URL + AppConstants.LOBBY_HUB,
          HttpConnectionOptions(
            transport: HttpTransportType.longPolling,
            logging: (level, message) => print('SIGNALR ---- $message'),
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
      await _connection.start();
    } catch (error) {
      log('$error');
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
      'LobbyCreated',
      (lobby) {
        _status();
        statusTimer = Timer.periodic(statusInterval, (_) => _status());
        service.onLobbyCreated(
          LobbyDto.fromJson(lobby?[0]),
        );
      },
    );

    _connection.on(
      'LobbyDeleted',
      (name) => service.onLobbyDeleted(
        name?[0],
      ),
    );

    _connection.on(
      'LobbyJoined',
      (lobby) => service.onLobbyJoined(
        LobbyDto.fromJson(lobby?[0]),
      ),
    );

    _connection.on(
      'SetUsers',
      (users) => service.onSetUsers(
        users?.map((e) => UserDto.fromJson(e)).toList() ?? [],
      ),
    );

    _connection.on(
      'SetMessages',
      (messages) => service.onSetMessages(
        (messages?.first as List).map((e) => MessageDto.fromJson(e)).toList(),
      ),
    );

    _connection.on(
      'UserEntered',
      (user) => service.onUserEntered(
        UserDto.fromJson(
          user?[0],
        ),
      ),
    );

    _connection.on(
      'UserLeft',
      (userId) => service.onUserLeft(
        userId?[0],
      ),
    );
    _connection.on(
      'OnStatus',
      (_) => _onStatus(),
    );

    _connection.on(
      'RecieveMessage',
      (message) => service.onRecieveMessage(
        MessageDto.fromJson(
          message?[0],
        ),
      ),
    );

    _connection.on(
      'GameCreated',
      (gameId) {
        statusTimer?.cancel();
        service.onGameCreated(
          gameId?[0],
        );
      },
    );
  }

  void _status() {
    _connection.invoke('Status', args: [service.lobbyName()]);
  }

  void _onStatus() {
    log('SIGNALR Status recieved -- LOBBY');
  }

  Future<void> disconnectLobby() async {
    await _connection.invoke('DisconnectLobby', args: []);
    disconnect();
  }

  Future<void> joinLobby(String lobbyName) async {
    await _connection.invoke('JoinLobby', args: [lobbyName]);
  }

  Future<void> createLobby() async =>
      _connection.invoke('CreateLobby', args: [service.lobbyName()]);

  Future<void> disconnect() async {
    statusTimer?.cancel();
    if (service.connectionInitialized) {
      service.connectionInitialized = false;
      await _connection.stop();
    }
  }

  Future<void> createGame() async {
    await _connection.invoke('CreateGame', args: [
      service.lobbyName(),
    ]);
  }

  Future<void> sendMessage({
    required String message,
  }) async {
    await _connection.invoke('SendMessage', args: [
      service.gameId,
      message,
    ]);
  }

  @override
  void onClose() async {
    disconnect();
    super.onClose();
  }

  Future<void> removeUser(String userId) async =>
      await _connection.invoke('DeletePlayer', args: [userId]);
}
