import 'package:json_annotation/json_annotation.dart';

part 'option_command.g.dart';

@JsonSerializable()
class OptionCommand {
  final String userId;
  final List<String> cardIds;

  OptionCommand({
    required this.userId,
    required this.cardIds,
  });

  factory OptionCommand.fromJson(Map<String, dynamic> json) =>
      _$OptionCommandFromJson(json);

  Map<String, dynamic> toJson() => _$OptionCommandToJson(this);
}
