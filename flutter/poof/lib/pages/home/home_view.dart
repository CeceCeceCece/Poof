import 'package:bang/core/app_colors.dart';
import 'package:bang/core/app_theme.dart';
import 'package:bang/core/helpers/app_validators.dart';
import 'package:bang/core/lang/app_strings.dart';
import 'package:bang/pages/home/home_controller.dart';
import 'package:bang/routes/routes.dart';
import 'package:bang/services/audio_service.dart';
import 'package:bang/services/game_service.dart';
import 'package:bang/widgets/bang_background.dart';
import 'package:bang/widgets/bang_button.dart';
import 'package:bang/widgets/bang_input_field.dart';
import 'package:bang/widgets/bang_logo.dart';
import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:font_awesome_flutter/font_awesome_flutter.dart';
import 'package:get/get.dart';

class HomeView extends GetView<HomeController> {
  final _formKey = GlobalKey<FormState>();
  @override
  Widget build(BuildContext context) {
    return BangBackground(
      safeAreaTop: false,
      resizeToAvoidBottomInset: false,
      child: Center(
        child: Column(
          children: [
            _buildLogoAndButtons(),
            SizedBox(
              height: 25,
            ),
            _buildJoinWithCodeButton(),
            SizedBox(height: 10),
            BangButton(
              onPressed: controller.readQR,
              text: AppStrings.reading_qr.tr,
            ),
            Spacer(),
            BangButton(
              // todo remove when finished testing
              text: 'shortcut',
              onPressed: () {
                var gameService = Get.put(GameService());
                gameService.roomId.value = 'roomID';
                AudioService.playBackgroundMusic();
                Get.offAndToNamed(Routes.GAME);
              },
            ),
            _buildCreateRoomButton(),
            Spacer(),
            Spacer(),
            _buildExitButton(),
          ],
        ),
      ),
    );
  }

  Align _buildExitButton() {
    return Align(
      alignment: Alignment.bottomRight,
      child: Padding(
        padding: const EdgeInsets.all(12.0),
        child: BangButton(
          width: 90,
          height: 40,
          onPressed: () => SystemNavigator.pop(animated: true),
          text: AppStrings.exit.tr,
        ),
      ),
    );
  }

  Widget _buildCreateRoomButton() {
    return BangButton(
      text: AppStrings.create_room.tr,
      onPressed: () async {
        String? lobbyName;
        lobbyName = await _showDialogForResult(
          title: AppStrings.give_name_to_room.tr,
        );
        if (lobbyName != null) controller.createGame(lobbyName);
      },
    );
  }

  Widget _buildJoinWithCodeButton() => BangButton(
        text: AppStrings.join_with_code.tr,
        onPressed: () async {
          String? roomId;

          roomId = await _showDialogForResult(
            title: AppStrings.enter_room_name.tr,
          );
          if (roomId != null) controller.joinRoom(roomId);
        },
      );

  Widget _buildLogoAndButtons() => Stack(
        alignment: Alignment.topCenter,
        children: [
          BangLogo(size: 275),
          Align(
            alignment: Alignment.centerRight,
            child: Padding(
              padding: const EdgeInsets.all(12.0),
              child: Row(
                children: _buildIconButtons(),
              ),
            ),
          ),
          _buildSlogan(),
        ],
      );

  List<Widget> _buildIconButtons() => [
        IconButton(
            onPressed: () => Get.toNamed(Routes.SETTINGS),
            icon: FaIcon(FontAwesomeIcons.cog),
            iconSize: 28,
            color: AppColors.background),
        SizedBox(
          width: 5,
        ),
        IconButton(
            onPressed: () => controller.logout(),
            icon: FaIcon(FontAwesomeIcons.signOutAlt),
            iconSize: 28,
            color: AppColors.background),
      ];

  Widget _buildSlogan() => Positioned(
        top: 235,
        child: Text(
          AppStrings.slogan.tr,
          style: AppTheme.slogan,
          textAlign: TextAlign.center,
        ),
      );

  Future<String?> _showDialogForResult({required String title}) async {
    var textController = TextEditingController();
    var content = Form(
      key: _formKey,
      child: BangInputField(
        autofocus: true,
        onSubmit: () => _onConfirm(textController.text),
        hint: AppStrings.room_name.tr,
        validator: AppValidators.defaultValidator,
        controller: textController,
      ),
    );
    return Get.defaultDialog<String>(
        title: title,
        content: content,
        cancel: BangButton(
          text: AppStrings.cancel.tr,
          isNormal: false,
          onPressed: Get.back,
          height: 35,
          width: 60,
        ),
        confirm: BangButton(
            text: AppStrings.ok.tr,
            height: 35,
            width: 60,
            onPressed: () => _onConfirm(textController.text)),
        onCancel: Get.back,
        onWillPop: _dialogOnWillPop,
        onConfirm: () => _onConfirm(textController.text));
  }

  Future<bool> _dialogOnWillPop() async {
    Get.back();
    return false;
  }

  void _onConfirm(String text) {
    if (_formKey.currentState!.validate()) Get.back(result: text);
  }
}
