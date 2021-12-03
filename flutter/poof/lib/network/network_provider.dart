import 'package:bang/core/app_constants.dart';
import 'package:bang/models/login_response.dart';
import 'package:bang/services/shared_preference_service.dart';
import 'package:get/get.dart';
import 'package:get/get_connect/http/src/request/request.dart';
import 'package:get_storage/get_storage.dart';

abstract class NetworkProvider extends GetConnect {
  final storage = GetStorage();
  @override
  void onInit() {
    httpClient.baseUrl = AppConstants.BASE_URL;

    httpClient.addRequestModifier<dynamic>((request) async {
      if (request is Request<
          LoginResponse>) // ! enélkül a login endpointra is akar tenni auth headert
        return request;
      request.headers['Authorization'] =
          'Bearer ${SharedPreferenceService.token}';
      return request;
    });
  }
}
