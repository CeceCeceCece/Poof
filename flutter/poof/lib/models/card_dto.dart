import 'package:bang/cards/model/card_constants.dart';
import 'package:json_annotation/json_annotation.dart';

part 'card_dto.g.dart';

@JsonSerializable()
class CardDto {
  final String id;
  final String name;
  final CardType type;
  final CardSuit suite;
  final CardValue value;

  CardDto({
    required this.id,
    required this.name,
    required this.type,
    required this.suite,
    required this.value,
  });

  factory CardDto.fromJson(Map<String, dynamic> json) =>
      _$CardDtoFromJson(json);

  Map<String, dynamic> toJson() => _$CardDtoToJson(this);
}
