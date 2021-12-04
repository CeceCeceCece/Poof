import 'dart:io';
import 'dart:typed_data';

import 'package:flutter/material.dart';
import 'package:image_gallery_saver/image_gallery_saver.dart';
import 'package:json_annotation/json_annotation.dart';
import 'package:path_provider/path_provider.dart';
import 'package:share_plus/share_plus.dart';

abstract class CardHelpers {
  static const sizeScale = 0.85;
  static double get cardHeight => sizeScale * 389;
  static double get cardWidth => sizeScale * 250;

  static const _basePath = 'assets/cards/';
  static const _imageExtension = '.png';

  static Widget getAsset(
      {required String name, required CardType type, double scale = 1.0}) {
    return Container(
        width: CardHelpers.cardWidth,
        height: CardHelpers.cardHeight,
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

enum CardValue {
  @JsonValue(0)
  Two,
  @JsonValue(1)
  Three,
  @JsonValue(2)
  Four,
  @JsonValue(3)
  Five,
  @JsonValue(4)
  Six,
  @JsonValue(5)
  Seven,
  @JsonValue(6)
  Eight,
  @JsonValue(7)
  Nine,
  @JsonValue(8)
  Ten,
  @JsonValue(9)
  Jack,
  @JsonValue(10)
  Queen,
  @JsonValue(11)
  King,
  @JsonValue(12)
  Ace,
}

enum CardSuit {
  @JsonValue(0)
  Spades,
  @JsonValue(1)
  Hearts,
  @JsonValue(2)
  Clubs,
  @JsonValue(3)
  Diamonds,
}

enum CardType {
  @JsonValue(0)
  Equipment,
  @JsonValue(1)
  Weapon,
  @JsonValue(2)
  Action,
  Back,
  Role,
  Character,
}