import 'package:bang/core/app_colors.dart';
import 'package:bang/core/app_constants.dart';
import 'package:bang/core/app_theme.dart';
import 'package:bang/core/lang/app_strings.dart';
import 'package:bang/models/user_dto.dart';
import 'package:bang/pages/lobby/lobby_controller.dart';
import 'package:bang/widgets/bang_background.dart';
import 'package:bang/widgets/bang_button.dart';
import 'package:bang/widgets/bang_chat.dart';
import 'package:flutter/material.dart';
import 'package:flutter/rendering.dart';
import 'package:get/get.dart';

class LobbyView extends GetView<LobbyController> {
  @override
  Widget build(BuildContext context) {
    return BangBackground(
      safeAreaTop: false,
      onWillPop: () async {
        controller.back();
        return true;
      },
      resizeToAvoidBottomInset: false,
      child: Padding(
        padding: const EdgeInsets.only(bottom: 20.0),
        child: Center(
          child: Obx(
            () => Column(
              mainAxisAlignment: MainAxisAlignment.start,
              children: [
                Spacer(),
                _buildTitle(),
                _buildPlayerList(),
                Spacer(),
                controller.playerIsLobbyAdmin
                    ? _buildAdminOnlyButtons()
                    : _buildRegularUserPlaceholder(),
                Spacer(),
                _buildBottomButtons(context),
              ],
            ),
          ),
        ),
      ),
    );
  }

  Widget _buildBottomButtons(BuildContext context) => Padding(
        padding: const EdgeInsets.symmetric(horizontal: 20),
        child: Row(
          mainAxisAlignment: MainAxisAlignment.spaceBetween,
          children: [
            _buildChatButton(context),
            BangButton(
              width: 90,
              height: 40,
              onPressed: controller.back,
              text: AppStrings.exit.tr,
            ),
          ],
        ),
      );

  Widget _buildChatButton(BuildContext context) => Material(
        elevation: 20,
        shape: CircleBorder(),
        color: Colors.transparent,
        clipBehavior: Clip.hardEdge,
        child: Ink(
          decoration: BoxDecoration(
              border: Border.all(width: 1, color: AppColors.darkBrown),
              gradient: AppColors.buttonGradient,
              borderRadius: BorderRadius.circular(200)),
          child: IconButton(
            onPressed: () => _buildChat(context),
            icon: Icon(
              Icons.chat,
              color: AppColors.background,
            ),
          ),
        ),
      );

  Widget _buildRegularUserPlaceholder() => Padding(
        padding: EdgeInsets.symmetric(horizontal: 50),
        child: Container(
          decoration: AppTheme.whiteBackgroundAndBorder,
          child: Padding(
            padding: const EdgeInsets.all(8.0),
            child: Column(
              children: [
                Text(
                  AppStrings.wait_for_room_admin.tr,
                  style: AppTheme.smallBrown,
                  textAlign: TextAlign.center,
                ),
                BangButton(
                  width: 160,
                  height: 40,
                  onPressed: controller.showQR,
                  text: AppStrings.show_qr_code.tr,
                ),
              ],
            ),
          ),
        ),
      );

  Widget _buildAdminOnlyButtons() => Row(
        mainAxisAlignment: MainAxisAlignment.spaceEvenly,
        children: [
          BangButton(
            width: 160,
            height: 40,
            text: AppStrings.start_game.tr,
            onPressed: controller.createGame,
          ),
          BangButton(
            width: 160,
            height: 40,
            onPressed: controller.showQR,
            text: AppStrings.show_qr_code.tr,
          ),
        ],
      );

  Widget _buildPlayerList() => Container(
        padding: EdgeInsets.only(top: 20),
        height: 7 * 47,
        child: ListView(
          children: controller.users
              .map((user) => controller.playerIsLobbyAdmin
                  ? _buildDismissibleTile(user)
                  : _buildStaticTile(user))
              .toList(),
          physics: NeverScrollableScrollPhysics(),
        ),
      );

  Widget _buildStaticTile(UserDto user) => Padding(
        padding: EdgeInsets.symmetric(horizontal: 30, vertical: 3),
        child: Container(
          height: 35,
          decoration: BoxDecoration(
              borderRadius: BorderRadius.circular(10.0), color: Colors.white),
          child: Row(
              mainAxisAlignment: MainAxisAlignment.center,
              crossAxisAlignment: CrossAxisAlignment.center,
              children: [
                SizedBox(
                  width: 35,
                ),
                Expanded(
                  child: Text(
                    user.name,
                    overflow: TextOverflow.ellipsis,
                    maxLines: 1,
                  ),
                ),
                controller.isAdmin(user)
                    ? IconButton(
                        icon: Icon(Icons.star, color: Colors.amber),
                        onPressed: null,
                      )
                    : Container(),
                SizedBox(
                  width: 20,
                )
              ]),
        ),
      );

  Widget _buildDismissibleTile(UserDto user) => Dismissible(
        confirmDismiss: (DismissDirection details) async =>
            Future.delayed(Duration(seconds: 1), () {
          return !controller.isAdmin(user);
        }),
        onDismissed: (_) => controller.removeUser(user),
        key: UniqueKey(),
        background: _buildDismissibleBackground(left: true),
        secondaryBackground: _buildDismissibleBackground(left: false),
        child: _buildStaticTile(user),
      );

  Container _buildDismissibleBackground({required bool left}) {
    return Container(
      alignment: left ? Alignment.centerLeft : Alignment.centerRight,
      color: Colors.transparent,
      child: Row(
        children: [
          left
              ? SizedBox(
                  width: 30,
                )
              : Container(),
          Icon(Icons.highlight_remove_sharp, color: Colors.white),
          !left
              ? SizedBox(
                  width: 30,
                )
              : Container(),
        ],
      ),
    );
  }

  Widget _buildTitle() => Stack(
        alignment: Alignment.center,
        children: [
          Container(
              height: 100,
              width: 240,
              decoration: AppTheme.whiteBackgroundAndBorder,
              child: Column(
                mainAxisAlignment: MainAxisAlignment.center,
                children: [
                  Text(
                    AppStrings.room_name_with_code
                        .trParams({'lobbyName': controller.lobbyName()}),
                    style: AppTheme.medium,
                    overflow: TextOverflow.clip,
                    textAlign: TextAlign.center,
                    maxLines: 2,
                  ),
                  SizedBox(
                    height: 10,
                  ),
                  Text(
                      AppStrings.players.trParams({
                        'current': controller.users().length.toString(),
                        'max': AppConstants.MAX_USER_NUMBER.toString()
                      }),
                      style: AppTheme.medium.copyWith(fontSize: 18)),
                ],
              )),
        ],
      );

  void _buildChat(BuildContext context) {
    showModalBottomSheet(
      isScrollControlled: true,
      backgroundColor: AppColors.background,
      elevation: 10,
      shape: RoundedRectangleBorder(borderRadius: BorderRadius.circular(30)),
      context: context,
      builder: (context) {
        return StatefulBuilder(
          builder: (BuildContext context, StateSetter setState) {
            controller.onMessageArrivedCallback = setState;
            controller.modalSheetScrollController = ScrollController();

            return BangChat(
              scrollController: controller.modalSheetScrollController,
              send: controller.sendMessage,
              textController: controller.chatTextController,
              playerName: controller.playerName,
              messages: controller.messages(),
            );
          },
        );
      },
    );
    controller.scrollToBottom();
  }
}
