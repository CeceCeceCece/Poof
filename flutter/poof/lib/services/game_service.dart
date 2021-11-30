import 'dart:async';
import 'dart:developer';

import 'package:bang/cards/model/action_cards/action_card.dart';
import 'package:bang/cards/model/action_cards/equipment_card.dart';
import 'package:bang/cards/model/action_cards/weapon_card.dart';
import 'package:bang/cards/model/bang_card.dart';
import 'package:bang/cards/model/card_constants.dart' as Bang;
import 'package:bang/cards/widgets/bang_card_widget.dart';
import 'package:bang/core/constants.dart';
import 'package:bang/services/service_base.dart';
import 'package:bang/services/shared_preference_service.dart';
import 'package:get/get_rx/src/rx_types/rx_types.dart';
import 'package:signalr_core/signalr_core.dart';

class GameService extends ServiceBase {
  RxBool expandedHandView = false.obs;

  RxBool expandedEquipmentView = false.obs;

  Rx<String?> roomId = Rx(null);

  RxList handWidgets = <BangCardWidget>[].obs;
  var statusInterval = Duration(seconds: 10);

  Timer? statusTimer;
  RxInt highlightedIndex = (-1).obs;

  late HubConnection _connection;

  var equipmentList = <BangCard>[
    WeaponCard(
      background: 'remington',
      name: 'remington',
      suit: Bang.Suit.Clubs,
      value: Bang.Value.Six,
      range: 3,
      type: Bang.CardType.Weapon,
    ),
    WeaponCard(
      background: 'remington',
      name: 'remington',
      suit: Bang.Suit.Clubs,
      value: Bang.Value.Six,
      range: 3,
      type: Bang.CardType.Weapon,
    ),
    WeaponCard(
      background: 'remington',
      name: 'remington',
      suit: Bang.Suit.Clubs,
      value: Bang.Value.Six,
      range: 3,
      type: Bang.CardType.Weapon,
    ),
    WeaponCard(
      background: 'remington',
      name: 'remington',
      suit: Bang.Suit.Clubs,
      value: Bang.Value.Six,
      range: 3,
      type: Bang.CardType.Weapon,
    ),
  ].obs;
  var temporaryEffectList = <BangCard>[
    EquipmentCard(
      background: 'jail',
      name: 'jail',
      suit: Bang.Suit.Clubs,
      value: Bang.Value.Seven,
      type: Bang.CardType.Equipment,
    ),
    EquipmentCard(
      background: 'dynamite',
      name: 'dynamite',
      suit: Bang.Suit.Clubs,
      value: Bang.Value.Eight,
      type: Bang.CardType.Equipment,
    ),
  ].obs;

  final RxList hand = [
    ActionCard(
      range: 0,
      background: 'beer',
      name: 'beer',
      suit: Bang.Suit.Clubs,
      value: Bang.Value.Ten,
      type: Bang.CardType.Action,
    ),
    WeaponCard(
      background: 'remington',
      name: 'remington',
      suit: Bang.Suit.Clubs,
      value: Bang.Value.Six,
      type: Bang.CardType.Weapon,
      range: 3,
    ),
    EquipmentCard(
      background: 'barrel',
      name: 'barrel',
      suit: Bang.Suit.Clubs,
      value: Bang.Value.Seven,
      type: Bang.CardType.Equipment,
    ),
    WeaponCard(
        background: 'volcanic',
        name: 'volcanic',
        suit: Bang.Suit.Clubs,
        value: Bang.Value.Eight,
        type: Bang.CardType.Weapon,
        range: 1),
    WeaponCard(
      background: 'remington',
      name: 'remington',
      suit: Bang.Suit.Clubs,
      value: Bang.Value.Six,
      range: 3,
      type: Bang.CardType.Weapon,
    ),
    EquipmentCard(
      background: 'barrel',
      name: 'barrel',
      suit: Bang.Suit.Clubs,
      value: Bang.Value.Seven,
      type: Bang.CardType.Equipment,
    ),
    WeaponCard(
        background: 'volcanic',
        name: 'volcanic',
        suit: Bang.Suit.Clubs,
        value: Bang.Value.Eight,
        type: Bang.CardType.Weapon,
        range: 1),
    ActionCard(
      background: 'stagecoach',
      name: 'stagecoach',
      suit: Bang.Suit.Clubs,
      value: Bang.Value.Nine,
      type: Bang.CardType.Action,
      range: 0,
    ),
    WeaponCard(
        background: 'volcanic',
        name: 'volcanic',
        suit: Bang.Suit.Clubs,
        value: Bang.Value.Eight,
        type: Bang.CardType.Weapon,
        range: 1),
    WeaponCard(
        background: 'remington',
        name: 'remington',
        suit: Bang.Suit.Clubs,
        value: Bang.Value.Six,
        type: Bang.CardType.Equipment,
        range: 3),
    EquipmentCard(
      background: 'barrel',
      name: 'barrel',
      suit: Bang.Suit.Clubs,
      value: Bang.Value.Seven,
      type: Bang.CardType.Equipment,
    ),
    WeaponCard(
        background: 'volcanic',
        name: 'volcanic',
        suit: Bang.Suit.Clubs,
        value: Bang.Value.Eight,
        type: Bang.CardType.Weapon,
        range: 1),
    ActionCard(
      background: 'stagecoach',
      name: 'stagecoach',
      suit: Bang.Suit.Clubs,
      value: Bang.Value.Nine,
      type: Bang.CardType.Action,
      range: 0,
    ),
  ].obs;

  @override
  void onInit() {
    handWidgets = [
      for (int i = 0; i < hand().length; i++)
        BangCardWidget(
          scale: 0.85,
          card: hand[i],
          canBeDragged: true,
          onDragSuccessCallback: () => removeCard(i),
          handCallback: () => highlight(i),
        ),
    ].obs;
    super.onInit();
  }

  void removeCard(int idx) async {
    handWidgets().removeAt(idx);
    handWidgets.refresh();
  }

  void setCards() {
    var newCards = [
      EquipmentCard(
        background: 'barrel',
        name: 'barrel',
        suit: Bang.Suit.Clubs,
        value: Bang.Value.Ten,
        type: Bang.CardType.Equipment,
      ),
      EquipmentCard(
        background: 'barrel',
        name: 'barrel',
        suit: Bang.Suit.Clubs,
        value: Bang.Value.Six,
        type: Bang.CardType.Equipment,
      ),
      EquipmentCard(
        background: 'barrel',
        name: 'barrel',
        suit: Bang.Suit.Clubs,
        value: Bang.Value.Seven,
        type: Bang.CardType.Equipment,
      ),
      EquipmentCard(
        background: 'barrel',
        name: 'barrel',
        suit: Bang.Suit.Clubs,
        value: Bang.Value.Eight,
        type: Bang.CardType.Equipment,
      ),
      EquipmentCard(
        background: 'barrel',
        name: 'barrel',
        suit: Bang.Suit.Clubs,
        value: Bang.Value.Nine,
        type: Bang.CardType.Equipment,
      ),
    ];

    hand.value = newCards;
  }

  void highlight(int i) {
    if (expandedHandView())
      highlightedIndex.value = i;
    else
      highlightedIndex.value = -1;
  }

  Future<void> initWebsocket() async {
    _connection = HubConnectionBuilder()
        .withUrl(
          Constants.BASE_URL + Constants.GAME_HUB,
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
    try {
      await _connection.start();
    } catch (error) {
      log('$error');
      _connection.start();
    }
    _connection.onreconnected((connectionId) {
      log('RECONNECTED');
      //statusTimer = Timer.periodic(statusInterval, (_) => _status()); //TODO
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
      (lobby) => _lobbyCreated(),
    );

    _connection.on(
      'LobbyDeleted',
      (name) => _lobbyDeleted(),
    );

    _connection.on(
      'LobbyJoined',
      (lobby) => _lobbyJoined(),
    );

    _connection.on(
      'SetUsers',
      (users) => _setUsers(),
    );

    _connection.on(
      'SetMessages',
      (messages) => _setMessages(),
    );

    _connection.on(
      'UserEntered',
      (user) => _userEntered(),
    );

    _connection.on(
      'UserLeft',
      (userId) => _userLeft(),
    );
    _connection.on(
      'OnStatus',
      (_) => _onStatus(),
    );

    _connection.on(
      'RecieveMessage',
      (message) => _recieveMessage(),
    );

    _connection.on(
      'GameCreated',
      (gameId) => _gameCreated(),
    );
  }

  void _lobbyJoined() {}

  void _lobbyCreated() {}

  void _lobbyDeleted() {}

  void _setUsers() {}

  void _setMessages() {}

  void _status() {
    _connection.invoke('Status', args: [roomId]);
  }

  void _onStatus() {
    log('Status recieved');
  }

  void _userEntered() {}

  void _userLeft() {}

  void _recieveMessage() {}

  void _gameCreated() {}

  Future<void> disconnect() async {
    statusTimer?.cancel();
    await _connection.stop();
  }

  @override
  void onClose() async {
    disconnect();
    super.onClose();
  }
}
