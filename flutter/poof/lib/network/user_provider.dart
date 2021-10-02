import 'dart:developer';

import 'package:bang/models/login_response.dart';
import 'package:bang/services/shared_preference_service.dart';
import 'package:get/get_connect.dart';

import 'network_provider.dart';

class UserProvider extends NetworkProvider {
  Future<Response<LoginResponse>> login(String body) =>
      post("/connect/token", body,
          contentType: 'application/x-www-form-urlencoded',
          decoder: (response) {
        var token = response['access_token'];
        log('Bearer $token');
        SharedPreferenceService.token = token;
        return LoginResponse(token);
      });
}
