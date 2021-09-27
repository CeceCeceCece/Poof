import 'dart:developer';

import 'package:bang/services/service_base.dart';
import 'package:just_audio/just_audio.dart';

class AudioService extends ServiceBase {
  static final _musicPlayer = AudioPlayer();
  static final _stingerPlayer = AudioPlayer();

  static void sting(String assetName) => _stingerPlayer
    ..setAsset('assets/sfx/$assetName.mp3')
    ..play();

  static void playMusic() => _musicPlayer.play();

  static void stopMusic() => _musicPlayer.stop();

  static void stopAll() {
    stopMusic();
    _stingerPlayer.stop();
  }

  static void mute() {
    _musicPlayer.setVolume(0);
    _stingerPlayer.setVolume(0);
  }

  static final _trackList = ['dynamite'];

  @override
  Future<void> init() async {
    await _stingerPlayer.setAsset('assets/sfx/dynamite.mp3');
    _stingerPlayer.play();

    _musicPlayer.setAudioSource(LoopingAudioSource(
        count: 3,
        child: ConcatenatingAudioSource(
            shuffleOrder: DefaultShuffleOrder(),
            children: _trackList
                .map((uri) => ClippingAudioSource(
                    end: Duration(seconds: 2),
                    child: ProgressiveAudioSource(
                        Uri.parse('asset:///assets/sfx/$uri.mp3'))))
                .toList()
              ..shuffle())));

    log('SERVICES: AUDIO_SERVICE INITIALIZED');
  }
}
