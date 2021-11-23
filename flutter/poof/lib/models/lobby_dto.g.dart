// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'lobby_dto.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

LobbyDto _$LobbyDtoFromJson(Map<String, dynamic> json) => LobbyDto(
      name: json['name'] as String,
      owner: json['owner'] as String,
      users: (json['users'] as List<dynamic>)
          .map((e) => UserDto.fromJson(e as Map<String, dynamic>))
          .toList(),
    );

Map<String, dynamic> _$LobbyDtoToJson(LobbyDto instance) => <String, dynamic>{
      'name': instance.name,
      'owner': instance.owner,
      'users': instance.users,
    };
