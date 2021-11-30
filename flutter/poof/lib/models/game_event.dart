import 'package:json_annotation/json_annotation.dart';

enum GameEvent {
  @JsonValue(0)
  None,
  @JsonValue(1)
  Draw,
  @JsonValue(2)
  SingleReact,
  @JsonValue(3)
  CallerReact,
  @JsonValue(4)
  AllReact
}
