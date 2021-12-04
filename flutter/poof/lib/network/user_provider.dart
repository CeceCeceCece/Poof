import 'dart:developer';

import 'package:bang/models/login_response.dart';
import 'package:bang/models/registration_dto.dart';
import 'package:get/get_connect.dart';

import 'network_provider.dart';

class UserProvider extends NetworkProvider {
  Future<Response<LoginResponse>> login(String body) => post(
        "/connect/token",
        body,
        contentType: 'application/x-www-form-urlencoded',
        decoder: (response) => LoginResponse(response['access_token']),
      );

  Future<Response<void>> register(String username, String password) => post(
        "/api/User/registration",
        RegistrationDto(userName: username, password: password).toJson(),
        contentType: 'application/json',
        decoder: (response) => log('${response.statusCode}'),
      );
}
