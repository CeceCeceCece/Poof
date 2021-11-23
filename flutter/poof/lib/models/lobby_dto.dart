import 'package:bang/models/user_dto.dart';
import 'package:json_annotation/json_annotation.dart';

part 'lobby_dto.g.dart';

@JsonSerializable()
class LobbyDto {
  final String name;
  final String owner;
  final List<UserDto> users;

  LobbyDto({required this.name, required this.owner, required this.users});

  factory LobbyDto.fromJson(Map<String, dynamic> json) =>
      _$LobbyDtoFromJson(json);

  Map<String, dynamic> toJson() => _$LobbyDtoToJson(this);
}
