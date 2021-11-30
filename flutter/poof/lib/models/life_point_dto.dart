import 'package:json_annotation/json_annotation.dart';

part 'life_point_dto.g.dart';

@JsonSerializable()
class LifePointDto {
  final int lifePoint;
  final String characterId;

  LifePointDto({
    required this.lifePoint,
    required this.characterId,
  });

  factory LifePointDto.fromJson(Map<String, dynamic> json) =>
      _$LifePointDtoFromJson(json);

  Map<String, dynamic> toJson() => _$LifePointDtoToJson(this);
}
