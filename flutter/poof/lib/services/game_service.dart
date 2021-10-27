import 'package:bang/cards/model/action_cards/action_card.dart';
import 'package:bang/cards/model/action_cards/equipment_card.dart';
import 'package:bang/cards/model/action_cards/weapon_card.dart';
import 'package:bang/cards/model/bang_card.dart';
import 'package:bang/cards/model/card_constants.dart' as Bang;
import 'package:bang/cards/widgets/bang_card_widget.dart';
import 'package:bang/services/service_base.dart';
import 'package:get/get_rx/src/rx_types/rx_types.dart';

class GameService extends ServiceBase {
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

  RxBool expandedHandView = false.obs;

  RxBool expandedEquipmentView = false.obs;

  RxList handWidgets = [].obs;
  @override
  void onInit() {
    handWidgets = [
      for (int i = 0; i < hand().length; i++)
        BangCardWidget(
          scale: 0.85,
          card: hand[i],
          onDragSuccessCallback: () => removeCard(i),
          handCallback: () => highlight(i),
        ),
    ].obs;
    super.onInit();
  }

  RxInt highlightedIndex = (-1).obs;

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
}
