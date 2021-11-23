import 'package:json_annotation/json_annotation.dart';

part 'registration_dto.g.dart';

@JsonSerializable()
class RegistrationDto {
  final String userName;
  final String password;

  RegistrationDto({required this.userName, required this.password});

  factory RegistrationDto.fromJson(Map<String, dynamic> json) =>
      _$RegistrationDtoFromJson(json);

  Map<String, dynamic> toJson() => _$RegistrationDtoToJson(this);
}
