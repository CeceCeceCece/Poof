import 'package:json_annotation/json_annotation.dart';

part 'option_dto.g.dart';

@JsonSerializable()
class OptionDto {
  final String description;
  final List<String> possibleTargets;
  final bool? requireCards;

  OptionDto({
    required this.description,
    required this.possibleTargets,
    this.requireCards,
  });

  factory OptionDto.fromJson(Map<String, dynamic> json) =>
      _$OptionDtoFromJson(json);

  Map<String, dynamic> toJson() => _$OptionDtoToJson(this);
}
