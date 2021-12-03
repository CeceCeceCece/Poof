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

  static Widget getAsset(
      {required String name, required CardType type, double scale = 1.0}) {
    return Container(
        width: CardWidgetHelpers.cardWidth,
        height: CardWidgetHelpers.cardHeight,
        child: Padding(
          padding: EdgeInsets.all(3.0 * scale),
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

  static getCardBack(CardType type, [double scale = 1.0]) => getAsset(
      name: '${_typeToAssetFolder(type).split('/')[0]}back',
      type: type,
      scale: scale);

  static String cardSuitToString(CardSuit suit) {
    switch (suit) {
      case CardSuit.Clubs:
        return "♣";
      case CardSuit.Diamonds:
        return "♦";
      case CardSuit.Hearts:
        return "♥";
      case CardSuit.Spades:
        return "♠";
    }
  }

  static Color cardSuitColor(CardSuit suit) =>
      (suit == CardSuit.Hearts || suit == CardSuit.Diamonds)
          ? Colors.red
          : Colors.black;

  static String cardValueToString(CardValue value) {
    switch (value) {
      case CardValue.Ace:
        return 'A';
      case CardValue.King:
        return 'K';
      case CardValue.Queen:
        return 'Q';
      case CardValue.Jack:
        return 'J';
      case CardValue.Ten:
        return '10';
      case CardValue.Nine:
        return '9';
      case CardValue.Eight:
        return '8';
      case CardValue.Seven:
        return '7';
      case CardValue.Six:
        return '6';
      case CardValue.Five:
        return '5';
      case CardValue.Four:
        return '4';
      case CardValue.Three:
        return '3';
      case CardValue.Two:
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
