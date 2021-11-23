// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'registration_dto.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

RegistrationDto _$RegistrationDtoFromJson(Map<String, dynamic> json) =>
    RegistrationDto(
      userName: json['userName'] as String,
      password: json['password'] as String,
    );

Map<String, dynamic> _$RegistrationDtoToJson(RegistrationDto instance) =>
    <String, dynamic>{
      'userName': instance.userName,
      'password': instance.password,
    };
