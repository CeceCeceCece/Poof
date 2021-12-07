abstract class CardBase {
  String name;
  String background;
  bool showBack;
  CardBase(
      {required this.background, required this.name, this.showBack = false});

  @override
  String toString() {
    return '$name';
  }
}
