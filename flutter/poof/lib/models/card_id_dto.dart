import 'package:json_annotation/json_annotation.dart';

part 'card_id_dto.g.dart';

@JsonSerializable()
class CardIdDto {
  final String cardId;
  final String characterId;

  CardIdDto({
    required this.cardId,
    required this.characterId,
  });

  factory CardIdDto.fromJson(Map<String, dynamic> json) =>
      _$CardIdDtoFromJson(json);

  Map<String, dynamic> toJson() => _$CardIdDtoToJson(this);
}
