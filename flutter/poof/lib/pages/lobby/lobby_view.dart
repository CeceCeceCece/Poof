import 'package:bang/core/app_colors.dart';
import 'package:bang/core/app_constants.dart';
import 'package:bang/core/app_theme.dart';
import 'package:bang/core/lang/app_strings.dart';
import 'package:bang/models/user_dto.dart';
import 'package:bang/pages/lobby/lobby_controller.dart';
import 'package:bang/services/auth_service.dart';
import 'package:bang/services/lobby_service.dart';
import 'package:bang/widgets/bang_background.dart';
import 'package:bang/widgets/bang_button.dart';
import 'package:bang/widgets/bang_input_field.dart';
import 'package:flutter/material.dart';
import 'package:flutter/rendering.dart';
import 'package:get/get.dart';
import 'package:intl/intl.dart';

class LobbyView extends GetView<LobbyController> {
  void _sendMessage(String message) {
    Get.find<LobbyService>().sendMessage(message: message);
  }

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
            child: Text(
              AppStrings.wait_for_room_admin.tr,
              style: AppTheme.smallBrown,
              textAlign: TextAlign.center,
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
            onPressed: controller.join,
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
        background: _buildDismissibleBackground(Alignment.centerLeft),
        secondaryBackground: _buildDismissibleBackground(Alignment.centerRight),
        child: _buildStaticTile(user),
      );

  Container _buildDismissibleBackground(AlignmentGeometry alignment) {
    return Container(
      alignment: alignment,
      color: Colors.transparent,
      child: Row(
        children: [
          SizedBox(
            width: 30,
          ),
          Icon(Icons.highlight_remove_sharp, color: Colors.white),
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
                      style: AppTheme.medium),
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

  _buildChat(BuildContext context) {
    showModalBottomSheet(
      isScrollControlled: true,
      backgroundColor: AppColors.background,
      elevation: 10,
      shape: RoundedRectangleBorder(borderRadius: BorderRadius.circular(30)),
      context: context,
      builder: (context) {
        return StatefulBuilder(
          builder: (BuildContext context, StateSetter setState) {
            Get.find<LobbyService>().onMessageArrivedCallback = setState;
            var username = Get.find<AuthService>().player;
            var textController = TextEditingController();
            ScrollController scrollController = ScrollController();

            controller.modalSheetScrollController = scrollController;
            var field = BangInputField(
                onSubmit: () async {
                  _sendMessage(
                    textController.text,
                  );
                  textController.clear();
                },
                controller: textController,
                hint: AppStrings.message.tr);
            setState(() {});

            return Padding(
                padding: EdgeInsets.only(
                  top: 10,
                  left: 15,
                  right: 15,
                ),
                child: Column(
                  mainAxisSize: MainAxisSize.min,
                  children: [
                    Container(
                      height: 300,
                      child: SingleChildScrollView(
                        physics: BouncingScrollPhysics(),
                        controller: scrollController,
                        child: Column(
                          mainAxisAlignment: MainAxisAlignment.end,
                          crossAxisAlignment: CrossAxisAlignment.center,
                          children: [
                            ...controller.messages.map(
                              (message) {
                                var sentBySelf = username == message.sender;
                                return Padding(
                                  padding: EdgeInsets.fromLTRB(5, 3, 5, 3),
                                  child: Align(
                                    alignment: sentBySelf
                                        ? Alignment.centerRight
                                        : Alignment.centerLeft,
                                    child: Container(
                                      decoration: BoxDecoration(
                                        borderRadius: BorderRadius.circular(20),
                                        color: sentBySelf
                                            ? Colors.brown
                                            : Colors.white,
                                      ),
                                      child: Padding(
                                        padding: const EdgeInsets.all(12.0),
                                        child: Column(
                                            mainAxisAlignment:
                                                MainAxisAlignment.start,
                                            crossAxisAlignment: sentBySelf
                                                ? CrossAxisAlignment.end
                                                : CrossAxisAlignment.start,
                                            children: [
                                              sentBySelf
                                                  ? Text(
                                                      '${AppStrings.you.tr} - ${DateFormat('kk:mm').format(message.postedDate)}',
                                                      overflow:
                                                          TextOverflow.ellipsis,
                                                      maxLines: 1,
                                                      style: TextStyle(
                                                        color: AppColors
                                                            .background,
                                                        fontWeight:
                                                            FontWeight.bold,
                                                      ),
                                                    )
                                                  : Text(
                                                      '${DateFormat('kk:mm').format(message.postedDate)} - ${message.sender}:',
                                                      overflow:
                                                          TextOverflow.ellipsis,
                                                      maxLines: 1,
                                                      style: TextStyle(
                                                          fontWeight:
                                                              FontWeight.bold)),
                                              Container(
                                                child: Padding(
                                                  padding:
                                                      const EdgeInsets.only(
                                                          left: 3,
                                                          top: 5,
                                                          right: 3),
                                                  child: Text('${message.text}',
                                                      overflow:
                                                          TextOverflow.ellipsis,
                                                      maxLines: 4,
                                                      style: sentBySelf
                                                          ? TextStyle(
                                                              color: AppColors
                                                                  .background)
                                                          : null),
                                                ),
                                              ),
                                              SizedBox(
                                                width: 20,
                                              )
                                            ]),
                                      ),
                                    ),
                                  ),
                                );
                              },
                            ),
                          ],
                        ),
                      ),
                    ),
                    Padding(
                      padding: EdgeInsets.only(
                          bottom: MediaQuery.of(context).viewInsets.bottom + 10,
                          top: 10),
                      child: Container(
                        child: Row(
                          children: [
                            Container(
                              width: 230,
                              height: 50,
                              child: field,
                            ),
                            Spacer(),
                            BangButton(
                              text: AppStrings.send.tr,
                              width: 90,
                              height: 50,
                              onPressed: () async {
                                _sendMessage(
                                  textController.text,
                                );
                                textController.clear();

                                setState(() {});
                              },
                            )
                          ],
                        ),
                      ),
                    ),
                  ],
                ));
          },
        );
      },
    );
    Future.delayed(
      Duration(milliseconds: 100),
      () => controller.modalSheetScrollController.animateTo(
        controller.modalSheetScrollController.position.maxScrollExtent + 50,
        curve: Curves.fastLinearToSlowEaseIn,
        duration: Duration(milliseconds: 500),
      ),
    );
  }
}
