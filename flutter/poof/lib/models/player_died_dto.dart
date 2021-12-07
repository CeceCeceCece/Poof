import 'package:bang/models/role_type.dart';
import 'package:json_annotation/json_annotation.dart';

part 'player_died_dto.g.dart';

@JsonSerializable()
class PlayerDiedDto {
  final String userId;
  final RoleType role;

  PlayerDiedDto({
    required this.userId,
    required this.role,
  });

  factory PlayerDiedDto.fromJson(Map<String, dynamic> json) =>
      _$PlayerDiedDtoFromJson(json);

  Map<String, dynamic> toJson() => _$PlayerDiedDtoToJson(this);
}
