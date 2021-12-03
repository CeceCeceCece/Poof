import 'dart:async';
import 'dart:developer';

import 'package:bang/cards/model/action_cards/action_card.dart';
import 'package:bang/cards/model/action_cards/equipment_card.dart';
import 'package:bang/cards/model/action_cards/weapon_card.dart';
import 'package:bang/cards/model/bang_card.dart';
import 'package:bang/cards/model/card_constants.dart' as Bang;
import 'package:bang/cards/widgets/bang_card_widget.dart';
import 'package:bang/core/app_constants.dart';
import 'package:bang/models/card_dto.dart';
import 'package:bang/models/game_event_dto.dart';
import 'package:bang/models/life_point_dto.dart';
import 'package:bang/models/message_dto.dart';
import 'package:bang/models/option_dto.dart';
import 'package:bang/services/service_base.dart';
import 'package:bang/services/shared_preference_service.dart';
import 'package:get/get_rx/src/rx_types/rx_types.dart';
import 'package:signalr_core/signalr_core.dart';

class GameService extends ServiceBase {
  RxBool expandedHandView = false.obs;

  RxBool expandedEquipmentView = false.obs;

  Rx<String?> roomId = Rx(null);
  String? gameId;

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
      'SetDeck',
      (cards) => _setDeck(
        cards?.map((e) => CardDto.fromJson(e)).toList() ?? [],
      ),
    );

    _connection.on(
      'SetEquipedDeck',
      (cards) => _setEquippedDeck(
        cards?.map((e) => CardDto.fromJson(e)).toList() ?? [],
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
        cards?.map((e) => CardDto.fromJson(e)).toList() ?? [],
      ),
    );

    _connection.on(
      'CardUnequiped',
      (card) => _cardUnequipped(
        CardDto.fromJson(
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
      'CardsAdded',
      (cards) => _cardsAdded(
        cards?.map((e) => CardDto.fromJson(e)).toList() ?? [],
      ),
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
      'RecieveMessage',
      (message) => _recieveMessage(
        MessageDto.fromJson(
          message?[0],
        ),
      ),
    );

    _connection.on(
      'CardsReceieved',
      (cards) => _cardsReceived(
        cards?.map((e) => CardDto.fromJson(e)).toList() ?? [],
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
  }

  void _setDiscardPile(CardDto card) {}

  void _setDeck(List<CardDto> cards) {}

  void _setEquippedDeck(List<CardDto> cards) {}

  void _setWeapon(String characterId, CardDto weapon) {}

  void _setLifePoint(LifePointDto lifePoint) {}

  void _status() {
    _connection.invoke('Status', args: [roomId]);
  }

  void _onStatus() {
    log('Status recieved');
  }

  void _cardsDropped(List<CardDto> cards) {}

  void _cardUnequipped(CardDto card) {}

  void _recieveMessage(MessageDto message) {}

  void _cardsEquipped(String characterId, CardDto card) {}
  void _cardsAdded(List<CardDto> cards) {}
  void _showCard(CardDto card) {}
  void _setGameEvent(GameEventDto gameEvent) {}

  void _showOption(OptionDto optionDto) {}

  void _cardsReceived(List<CardDto> list) {}

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

  void answerCard({required OptionDto option}) async {
    await _connection.invoke('AnswearCard', args: [
      gameId,
      option.toJson(),
    ]).then(
      (value) => print('CARD ANSWER SENT!'),
    );
  }

  void drawReact({required OptionDto option}) async {
    await _connection.invoke('DrawReact', args: [
      gameId,
      option.toJson(),
    ]).then(
      (value) => print('DRAW REACT SENT!'),
    );
  }

  void activeCard({required OptionDto option, required String cardId}) async {
    await _connection.invoke('ActiveCard', args: [
      gameId,
      cardId,
      option.toJson(),
    ]).then(
      (value) => print('ACTIVE CARD SENT!'),
    );
  }

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
