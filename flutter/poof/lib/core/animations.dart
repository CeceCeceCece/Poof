abstract class BangAnimations {
  static const _prefix = 'assets/animations/';
  static const _fileFormat = '.json';

  static String _play(String name) => _prefix + name + _fileFormat;

  static get splash => _play('boom');
  static get noInternet => _play('no_internet');
}
