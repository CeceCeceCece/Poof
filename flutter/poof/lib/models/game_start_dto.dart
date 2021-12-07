import 'package:bang/models/role_type.dart';
import 'package:json_annotation/json_annotation.dart';

import 'card_dto.dart';
import 'character_dto.dart';

part 'game_start_dto.g.dart';

@JsonSerializable()
class GameStartDto {
  String name;
  RoleType role;
  String selfId;
  String sheriffId;
  List<CardDto> cards;
  List<CharacterDto> characters;
  int lifePoint;

  GameStartDto({
    required this.name,
    required this.selfId,
    required this.cards,
    required this.sheriffId,
    required this.role,
    required this.characters,
    required this.lifePoint,
  });

  factory GameStartDto.fromJson(Map<String, dynamic> json) =>
      _$GameStartDtoFromJson(json);

  Map<String, dynamic> toJson() => _$GameStartDtoToJson(this);
}
