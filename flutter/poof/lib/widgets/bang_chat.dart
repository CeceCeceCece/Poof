import 'package:bang/core/app_colors.dart';
import 'package:bang/core/lang/app_strings.dart';
import 'package:bang/models/message_dto.dart';
import 'package:bang/widgets/bang_button.dart';
import 'package:flutter/material.dart';
import 'package:get/get_utils/src/extensions/internacionalization.dart';
import 'package:intl/intl.dart';

import 'bang_input_field.dart';

class BangChat extends StatelessWidget {
  const BangChat(
      {Key? key,
      required this.scrollController,
      required this.send,
      required this.textController,
      required this.playerName,
      required this.messages})
      : super(key: key);

  final ScrollController scrollController;
  final VoidCallback send;
  final TextEditingController textController;
  final String playerName;
  final List<MessageDto> messages;

  @override
  Widget build(BuildContext context) {
    return Padding(
      padding: EdgeInsets.only(
        top: 10,
        left: 15,
        right: 15,
      ),
      child: Column(
        mainAxisSize: MainAxisSize.min,
        children: [
          _buildMessageList(),
          _buildMessageField(context),
        ],
      ),
    );
  }

  Widget _buildMessageField(
    //! send, textcontroller
    BuildContext context,
  ) =>
      Padding(
        padding: EdgeInsets.only(
            bottom: MediaQuery.of(context).viewInsets.bottom + 10, top: 10),
        child: Container(
          child: Row(
            children: [
              Container(
                width: 230,
                height: 50,
                child: BangInputField(
                    onSubmit: send,
                    controller: textController,
                    hint: AppStrings.message.tr),
              ),
              Spacer(),
              BangButton(
                text: AppStrings.send.tr,
                width: 90,
                height: 50,
                onPressed: send,
              )
            ],
          ),
        ),
      );

  Widget _buildMessageList() => Container(
        height: 300,
        child: SingleChildScrollView(
          physics: BouncingScrollPhysics(),
          controller: scrollController,
          child: Column(
            mainAxisAlignment: MainAxisAlignment.end,
            crossAxisAlignment: CrossAxisAlignment.center,
            children: [
              ...messages.map(
                (message) {
                  var sentBySelf = playerName == message.sender;
                  return Padding(
                    padding: EdgeInsets.fromLTRB(5, 3, 5, 3),
                    child: _buildMessage(sentBySelf, message),
                  );
                },
              ),
            ],
          ),
        ),
      );

  Align _buildMessage(bool sentBySelf, MessageDto message) {
    return Align(
      alignment: sentBySelf ? Alignment.centerRight : Alignment.centerLeft,
      child: Container(
        decoration: BoxDecoration(
          borderRadius: BorderRadius.circular(20),
          color: sentBySelf ? Colors.brown : Colors.white,
        ),
        child: Padding(
          padding: const EdgeInsets.all(12.0),
          child: Column(
              mainAxisAlignment: MainAxisAlignment.start,
              crossAxisAlignment: sentBySelf
                  ? CrossAxisAlignment.end
                  : CrossAxisAlignment.start,
              children: [
                _buildMessageHeader(
                  sentBySelf,
                  message,
                ),
                _buildMessageText(
                  sentBySelf,
                  message,
                ),
              ]),
        ),
      ),
    );
  }

  Container _buildMessageText(
    bool sentBySelf,
    MessageDto message,
  ) {
    return Container(
      child: Padding(
        padding: const EdgeInsets.only(left: 3, top: 5, right: 3),
        child: Text(message.text,
            overflow: TextOverflow.ellipsis,
            maxLines: 4,
            style: sentBySelf ? TextStyle(color: AppColors.background) : null),
      ),
    );
  }

  Text _buildMessageHeader(bool sentBySelf, MessageDto message) {
    return Text(
      sentBySelf
          ? '${AppStrings.you.tr} - ${DateFormat('kk:mm').format(message.postedDate)}'
          : '${DateFormat('kk:mm').format(message.postedDate)} - ${message.sender}:',
      overflow: TextOverflow.ellipsis,
      maxLines: 1,
      style: TextStyle(
        color: AppColors.background,
        fontWeight: FontWeight.bold,
      ),
    );
  }
}
