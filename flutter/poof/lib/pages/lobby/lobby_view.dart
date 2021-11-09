import 'package:bang/cards/widgets/button.dart';
import 'package:bang/core/colors.dart';
import 'package:bang/pages/lobby/lobby_controller.dart';
import 'package:flutter/material.dart';
import 'package:get/get.dart';

class LobbyView extends GetView<LobbyController> {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      backgroundColor: BangColors.background,
      body: Center(
        child: Obx(
          () => Column(
            mainAxisAlignment: MainAxisAlignment.center,
            children: [
              Text('LOBBY: ${controller.roomID}'),
              Text('PLAYERS: ${controller.playerList().length}/7'),
              SizedBox(height: 50),
              BangButton(
                text: 'Enter game',
                onPressed: controller.join,
              ),
              Container(
                padding: EdgeInsets.only(top: 20),
                height: 7 * 35,
                child: ListView(
                  children: controller.playerList
                      .map((element) => controller.playerIsLobbyAdmin()
                          ? Dismissible(
                              confirmDismiss: (details) async =>
                                  Future.delayed(Duration(seconds: 1), () {
                                return !controller.isAdmin(
                                    controller.playerList[details.index]);
                              }),
                              onDismissed: (_) =>
                                  controller.removePlayer(element),
                              key: Key(element),
                              background: Container(
                                alignment: Alignment.centerLeft,
                                color: BangColors.buttonShadowColor,
                                child: Row(
                                  children: [
                                    SizedBox(
                                      width: 30,
                                    ),
                                    Icon(Icons.highlight_remove_sharp,
                                        color: Colors.white),
                                  ],
                                ),
                              ),
                              secondaryBackground: Container(
                                alignment: Alignment.centerRight,
                                color: BangColors.buttonShadowColor,
                                child: Row(
                                  mainAxisAlignment: MainAxisAlignment.end,
                                  children: [
                                    Icon(Icons.highlight_remove_sharp,
                                        color: Colors.white),
                                    SizedBox(
                                      width: 30,
                                    ),
                                  ],
                                ),
                              ),
                              child: Padding(
                                padding: EdgeInsets.fromLTRB(30, 3, 30, 3),
                                child: Container(
                                  height: 35,
                                  decoration: BoxDecoration(
                                      borderRadius: BorderRadius.circular(10.0),
                                      color: Colors.white),
                                  child: Row(
                                      mainAxisAlignment:
                                          MainAxisAlignment.center,
                                      crossAxisAlignment:
                                          CrossAxisAlignment.center,
                                      children: [
                                        SizedBox(
                                          width: 35,
                                        ),
                                        Text(element),
                                        Spacer(),
                                        IconButton(
                                          icon: Icon(
                                            Icons.star,
                                            color: controller.isAdmin(element)
                                                ? Colors.amber
                                                : Colors.grey,
                                          ),
                                          onPressed: () =>
                                              controller.toggleAdmin(element),
                                        ),
                                        SizedBox(
                                          width: 20,
                                        )
                                      ]),
                                ),
                              ),
                            )
                          : Padding(
                              padding: EdgeInsets.fromLTRB(30, 3, 30, 3),
                              child: Container(
                                height: 35,
                                decoration: BoxDecoration(
                                    borderRadius: BorderRadius.circular(10.0),
                                    color: Colors.white),
                                child: Row(
                                    mainAxisAlignment: MainAxisAlignment.center,
                                    crossAxisAlignment:
                                        CrossAxisAlignment.center,
                                    children: [
                                      SizedBox(
                                        width: 35,
                                      ),
                                      Text(element),
                                      Spacer(),
                                      controller.isAdmin(element)
                                          ? IconButton(
                                              icon: Icon(Icons.star,
                                                  color: Colors.amber),
                                              onPressed: null,
                                            )
                                          : Container(),
                                      SizedBox(
                                        width: 20,
                                      )
                                    ]),
                              ),
                            ))
                      .toList(),
                ),
              ),
              SizedBox(height: 20),
              BangButton(
                text: 'Add/remove players',
                onPressed: () => controller.addPlayers(['players']),
              ),
            ],
          ),
        ),
      ),
    );
  }
}
