import 'dart:developer';
import 'dart:ui';

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

  static void pauseMusic() => _musicPlayer.pause();

  static void stopAll() {
    stopMusic();
    _stingerPlayer.stop();
  }

  static void mute() {
    _musicPlayer.setVolume(0);
    _stingerPlayer.setVolume(0);
  }

  static final _trackList = [
    'bartertown',
    'chasingvictory',
    'desperado',
    'emptyworld',
    'forvictoryandhonor',
    'inthistown',
    'oneshotjones',
    'outback',
    'peyotecowboy',
    'westernapocalypse',
  ];

  static playMenuSong() => _musicPlayer
    ..setAudioSource(
      LoopingAudioSource(
        count: 200,
        child: ProgressiveAudioSource(
          Uri.parse('asset:///assets/music/oneshotjones.mp3'),
        ),
      ),
    )
    ..play();

  static playBackgroundMusic() {
    _trackList.shuffle();
    _musicPlayer.setAudioSource(LoopingAudioSource(
        count: 200,
        child: ConcatenatingAudioSource(
            useLazyPreparation: true,
            shuffleOrder: DefaultShuffleOrder(),
            children: _trackList
                .map((uri) => ProgressiveAudioSource(
                    Uri.parse('asset:///assets/music/$uri.mp3')))
                .toList())));
    _musicPlayer.play();
  }

  @override
  Future<void> init() async {
    await _stingerPlayer.setAsset('assets/sfx/dynamite.mp3');
    _stingerPlayer.play();
    log('SERVICES: AUDIO_SERVICE INITIALIZED');
  }

  static void handleLifecycleChange(AppLifecycleState state) {
    switch (state) {
      case AppLifecycleState.resumed:
        playMusic();
        break;
      case AppLifecycleState.inactive:
        pauseMusic();
        break;
      case AppLifecycleState.paused:
        pauseMusic();
        break;
      case AppLifecycleState.detached:
        stopMusic();
        break;
    }
  }
}
