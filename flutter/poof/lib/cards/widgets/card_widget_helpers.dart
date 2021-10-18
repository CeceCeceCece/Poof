import 'dart:io';
import 'dart:typed_data';

import 'package:bang/cards/model/card_constants.dart';
import 'package:flutter/material.dart';
import 'package:image_gallery_saver/image_gallery_saver.dart';
import 'package:path_provider/path_provider.dart';
import 'package:share_plus/share_plus.dart';

abstract class CardWidgetHelpers {
  static const sizeScale = 0.85;
  static double get cardHeight => sizeScale * 389;
  static double get cardWidth => sizeScale * 250;

  static const _basePath = 'assets/cards/';
  static const _imageExtension = '.png';

  static Widget getAsset({required String name, required CardType type}) {
    return Container(
        width: CardWidgetHelpers.cardWidth,
        height: CardWidgetHelpers.cardHeight,
        child: Padding(
          padding: const EdgeInsets.all(3.0),
          child: Image.asset(
            _basePath + _typeToAssetFolder(type) + name + _imageExtension,
            fit: BoxFit.cover,
          ),
        ));
  }

  static String _typeToAssetFolder(CardType type) {
    switch (type) {
      case CardType.Action:
        return 'playable/';
      case CardType.Equipment:
        return 'playable/';
      case CardType.Weapon:
        return 'playable/';
      case CardType.Character:
        return 'character/';
      case CardType.Role:
        return 'role/';
      case CardType.Back:
        return 'back/';
    }
  }

  static getCardBack(CardType type) => getAsset(
      name: '${_typeToAssetFolder(type).split('/')[0]}back', type: type);

  static String cardSuitToString(Suit suit) {
    switch (suit) {
      case Suit.Clubs:
        return "♣";
      case Suit.Diamonds:
        return "♦";
      case Suit.Hearts:
        return "♥";
      case Suit.Spades:
        return "♠";
    }
  }

  static Color cardSuitColor(Suit suit) =>
      (suit == Suit.Hearts || suit == Suit.Diamonds)
          ? Colors.red
          : Colors.black;

  static String cardValueToString(Value value) {
    switch (value) {
      case Value.Ace:
        return 'A';
      case Value.King:
        return 'K';
      case Value.Queen:
        return 'Q';
      case Value.Jack:
        return 'J';
      case Value.Ten:
        return '10';
      case Value.Nine:
        return '9';
      case Value.Eight:
        return '8';
      case Value.Seven:
        return '7';
      case Value.Six:
        return '6';
      case Value.Five:
        return '5';
      case Value.Four:
        return '4';
      case Value.Three:
        return '3';
      case Value.Two:
        return '2';
    }
  }

  static void saveAndShareImage(Uint8List? image) async {
    if (image != null) {
      final directory = await getApplicationDocumentsDirectory();
      final imagePath = await File('${directory.path}/bang_card.png').create();
      await imagePath.writeAsBytes(image);
      await Share.shareFiles([imagePath.path]);
      ImageGallerySaver.saveImage(image, quality: 100, name: 'bang_card');
    }
  }
}
