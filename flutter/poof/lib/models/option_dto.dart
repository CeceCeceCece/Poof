import 'package:json_annotation/json_annotation.dart';

part 'option_dto.g.dart';

@JsonSerializable()
class OptionDto {
  final String description;
  final List<String> possibleTargets;
  final bool? requireCards;
  final int numberOfCards;
  final bool? requireAnswear;

  OptionDto({
    required this.description,
    required this.possibleTargets,
    required this.numberOfCards,
    this.requireAnswear,
    this.requireCards,
  });

  factory OptionDto.fromJson(Map<String, dynamic> json) =>
      _$OptionDtoFromJson(json);

  Map<String, dynamic> toJson() => _$OptionDtoToJson(this);
}
