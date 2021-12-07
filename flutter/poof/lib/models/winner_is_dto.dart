import 'package:bang/models/role_type.dart';
import 'package:json_annotation/json_annotation.dart';

part 'winner_is_dto.g.dart';

@JsonSerializable()
class WinnerIsDto {
  final RoleType winner;

  WinnerIsDto({
    required this.winner,
  });

  factory WinnerIsDto.fromJson(Map<String, dynamic> json) =>
      _$WinnerIsDtoFromJson(json);

  Map<String, dynamic> toJson() => _$WinnerIsDtoToJson(this);
}
