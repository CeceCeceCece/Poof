import 'package:bang/cards/model/action_cards/equipment_card.dart';
import 'package:bang/cards/model/bang_card.dart';
import 'package:bang/cards/model/card_constants.dart' as Bang;
import 'package:bang/cards/widgets/bang_card_widget.dart';
import 'package:bang/services/service_base.dart';
import 'package:get/get_rx/src/rx_types/rx_types.dart';

class GameService extends ServiceBase {
  final RxList hand = [
    EquipmentCard(
      background: 'beer',
      name: 'beer',
      suit: Bang.Suit.Clubs,
      value: Bang.Value.Ten,
      type: Bang.CardType.Equipment,
    ),
    EquipmentCard(
      background: 'remington',
      name: 'remington',
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
      background: 'volcanic',
      name: 'volcanic',
      suit: Bang.Suit.Clubs,
      value: Bang.Value.Eight,
      type: Bang.CardType.Equipment,
    ),
    EquipmentCard(
      background: 'remington',
      name: 'remington',
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
      background: 'volcanic',
      name: 'volcanic',
      suit: Bang.Suit.Clubs,
      value: Bang.Value.Eight,
      type: Bang.CardType.Equipment,
    ),
    EquipmentCard(
      background: 'stagecoach',
      name: 'stagecoach',
      suit: Bang.Suit.Clubs,
      value: Bang.Value.Nine,
      type: Bang.CardType.Equipment,
    ),
    EquipmentCard(
      background: 'volcanic',
      name: 'volcanic',
      suit: Bang.Suit.Clubs,
      value: Bang.Value.Eight,
      type: Bang.CardType.Equipment,
    ),
    EquipmentCard(
      background: 'remington',
      name: 'remington',
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
      background: 'volcanic',
      name: 'volcanic',
      suit: Bang.Suit.Clubs,
      value: Bang.Value.Eight,
      type: Bang.CardType.Equipment,
    ),
    EquipmentCard(
      background: 'stagecoach',
      name: 'stagecoach',
      suit: Bang.Suit.Clubs,
      value: Bang.Value.Nine,
      type: Bang.CardType.Equipment,
    ),
  ].obs;

  RxBool expandedHandView = false.obs;

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
