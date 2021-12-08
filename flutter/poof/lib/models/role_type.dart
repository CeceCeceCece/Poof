import 'package:json_annotation/json_annotation.dart';

enum RoleType {
  @JsonValue(0)
  None,
  @JsonValue(1)
  Outlaw,
  @JsonValue(2)
  Renegade,
  @JsonValue(3)
  Sheriff,
  @JsonValue(4)
  DeputySheriff
}

extension RoleString on RoleType {
  String get asString {
    switch (this) {
      case RoleType.DeputySheriff:
        return 'vice';
      case RoleType.Outlaw:
        return 'outlaw';
      case RoleType.Sheriff:
        return 'sheriff';
      case RoleType.Renegade:
        return 'renegade';
      case RoleType.None:
        return 'roleback';
    }
  }
}
