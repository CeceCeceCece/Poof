import 'dart:developer';

import 'package:bang/models/login_response.dart';
import 'package:bang/models/register_response.dart';
import 'package:bang/models/registration_dto.dart';
import 'package:bang/services/shared_preference_service.dart';
import 'package:get/get_connect.dart';

import 'network_provider.dart';

class UserProvider extends NetworkProvider {
  Future<Response<LoginResponse>> login(String body) =>
      post("/connect/token", body,
          contentType: 'application/x-www-form-urlencoded',
          decoder: (response) {
        String token = response['access_token'];
        log('Bearer $token');
        SharedPreferenceService.token = token;
        return LoginResponse(token);
      });

  Future<Response<RegisterResponse>> register(
          String username, String password) =>
      post("/api/User/registration",
          RegistrationDto(userName: username, password: password).toJson(),
          contentType: 'application/json', decoder: (response) {
        log('$response');

        return RegisterResponse('token');
      });
}
