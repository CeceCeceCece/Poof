import 'package:bang/cards/model/action_cards/action_card.dart';
import 'package:bang/cards/model/action_cards/equipment_card.dart';
import 'package:bang/cards/model/action_cards/weapon_card.dart';
import 'package:bang/cards/model/bang_card.dart';
import 'package:bang/cards/model/card_constants.dart';
import 'package:bang/cards/widgets/bang_card_widget.dart';
import 'package:bang/cards/widgets/button.dart';
import 'package:bang/core/lang/app_strings.dart';
import 'package:bang/services/audio_service.dart';
import 'package:flutter/material.dart';
import 'package:get/get.dart';

class GameController extends GetxController {
  var _exitConfirmed = false;
  var playerNumber = 7.obs;
  var isHandExpanded = false.obs;
  var isEquipmentViewExpanded = false.obs;
  RxInt highlightedIndex = (-1).obs;

  void toggleExpandedHand() {
    isHandExpanded.value = !isHandExpanded();
    isEquipmentViewExpanded.value = false;
  }

  var handWidgets = <BangCardWidget>[].obs;

  void toggleEquipmentView() =>
      isEquipmentViewExpanded.value = !isEquipmentViewExpanded();

  void highlight(int i) {
    if (isHandExpanded())
      highlightedIndex.value = i;
    else
      highlightedIndex.value = -1;
  }

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
          handCallbackInverse: () => highlight(-1),
        ),
    ].obs;
    super.onInit();
  }

  void removeCard(int idx) async {
    handWidgets().removeAt(idx);
    handWidgets.refresh();
  }

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

  final equipmentCards = [
    BangCardWidget(
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
    BangCardWidget(
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
    BangCardWidget(
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
    BangCardWidget(
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
    BangCardWidget(
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
    BangCardWidget(
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

  var equipmentList = <BangCard>[
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
  var temporaryEffectList = <BangCard>[
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
}
