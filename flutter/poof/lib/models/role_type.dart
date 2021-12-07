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
