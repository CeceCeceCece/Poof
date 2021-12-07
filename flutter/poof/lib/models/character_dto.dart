import 'package:json_annotation/json_annotation.dart';

part 'character_dto.g.dart';

@JsonSerializable()
class CharacterDto {
  String name;
  int lifePoint;
  List<String> cardIds;
  String userId;
  String userName;

  CharacterDto({
    required this.name,
    required this.cardIds,
    required this.lifePoint,
    required this.userId,
    required this.userName,
  });

  factory CharacterDto.fromJson(Map<String, dynamic> json) =>
      _$CharacterDtoFromJson(json);

  Map<String, dynamic> toJson() => _$CharacterDtoToJson(this);
}
