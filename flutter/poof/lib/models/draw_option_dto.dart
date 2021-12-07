import 'package:json_annotation/json_annotation.dart';

import 'card_dto.dart';

part 'draw_option_dto.g.dart';

@JsonSerializable()
class DrawOptionDto {
  final bool userIdRequired;
  final List<CardDto> cards;

  DrawOptionDto({
    required this.userIdRequired,
    required this.cards,
  });

  factory DrawOptionDto.fromJson(Map<String, dynamic> json) =>
      _$DrawOptionDtoFromJson(json);

  Map<String, dynamic> toJson() => _$DrawOptionDtoToJson(this);
}
