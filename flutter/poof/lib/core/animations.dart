abstract class BangAnimations {
  static const _prefix = 'assets/animations/';
  static const _fileFormat = '.json';

  static String _format(String name) => _prefix + name + _fileFormat;

  static get splash => _format('boom_');
  static get noInternet => _format('no_internet');
}
