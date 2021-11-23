import 'package:json_annotation/json_annotation.dart';

part 'lobby_dto.g.dart';

@JsonSerializable()
class LobbyDto {
  final String name;
  final String owner;

  LobbyDto({required this.name, required this.owner});

  factory LobbyDto.fromJson(Map<String, dynamic> json) =>
      _$LobbyDtoFromJson(json);

  Map<String, dynamic> toJson() => _$LobbyDtoToJson(this);
}
