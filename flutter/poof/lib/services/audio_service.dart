import 'package:audioplayers/audioplayers.dart';

class AudioService {
  static AudioCache _player = AudioCache(prefix: 'assets/sfx/');
  static play(String title) => _player.play(title);
  static const dynamite = 'dynamite.mp3';
}
