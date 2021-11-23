import 'package:bang/cards/widgets/button.dart';
import 'package:bang/core/colors.dart';
import 'package:bang/core/constants.dart';
import 'package:bang/pages/lobby/lobby_controller.dart';
import 'package:flutter/material.dart';
import 'package:get/get.dart';
import 'package:google_fonts/google_fonts.dart';

class LobbyView extends GetView<LobbyController> {
  @override
  Widget build(BuildContext context) {
    return WillPopScope(
      onWillPop: () async {
        controller.back();
        return true;
      },
      child: Container(
        decoration: BoxDecoration(
            image: DecorationImage(
                image: AssetImage(
                  Constants.backgroundPath,
                ),
                fit: BoxFit.fitHeight)),
        child: Scaffold(
          backgroundColor: Colors.transparent,
          body: Padding(
            padding: const EdgeInsets.only(bottom: 20.0),
            child: Center(
              child: Obx(
                () => Column(
                  mainAxisAlignment: MainAxisAlignment.start,
                  children: [
                    Spacer(),
                    Stack(
                      alignment: Alignment.center,
                      children: [
                        Container(
                          height: 100,
                          width: 240,
                          decoration: BoxDecoration(
                              color: Colors.white38,
                              border:
                                  Border.all(color: Colors.white, width: 1.5),
                              borderRadius: BorderRadius.circular(30)),
                        ),
                        Column(
                          children: [
                            Text('Szoba kód: ${controller.roomID}',
                                style: GoogleFonts.graduate(
                                    textStyle: TextStyle(
                                        fontSize: 20,
                                        fontWeight: FontWeight.bold,
                                        color: Colors.brown))),
                            SizedBox(
                              height: 10,
                            ),
                            Text(
                                'Játékosok: ${controller.playerList().length}/7',
                                style: GoogleFonts.graduate(
                                    textStyle: TextStyle(
                                        fontSize: 18,
                                        fontWeight: FontWeight.bold,
                                        color: Colors.brown))),
                          ],
                        ),
                      ],
                    ),
                    Container(
                      padding: EdgeInsets.only(top: 20),
                      height: 7 * 47,
                      child: ListView(
                        children: controller.playerWidgetList,
                        physics: NeverScrollableScrollPhysics(),
                      ),
                    ),
                    Spacer(),
                    controller.playerIsLobbyAdmin()
                        ? Row(
                            mainAxisAlignment: MainAxisAlignment.spaceEvenly,
                            children: [
                              BangButton(
                                width: 160,
                                height: 40,
                                text: 'Játék indítása',
                                onPressed: controller.join,
                              ),
                              BangButton(
                                width: 160,
                                height: 40,
                                onPressed: controller.showQR,
                                text: 'QR kód mutatása',
                              ),
                            ],
                          )
                        : Padding(
                            padding: EdgeInsets.only(left: 50, right: 50),
                            child: Container(
                              decoration: BoxDecoration(
                                  color: Colors.white38,
                                  border: Border.all(
                                      color: Colors.white, width: 1.5),
                                  borderRadius: BorderRadius.circular(30)),
                              child: Padding(
                                padding: const EdgeInsets.all(8.0),
                                child: Text(
                                  'Várj, hogy a szoba létrehozója elindítsa a játékot!',
                                  style: GoogleFonts.graduate(
                                    textStyle: TextStyle(
                                        fontSize: 15,
                                        fontWeight: FontWeight.bold,
                                        color: Colors.brown),
                                  ),
                                  textAlign: TextAlign.center,
                                ),
                              ),
                            ),
                          ),
                    Spacer(),
                    Row(
                      mainAxisAlignment: MainAxisAlignment.spaceBetween,
                      children: [
                        Padding(
                          padding: const EdgeInsets.fromLTRB(20, 12, 12, 12),
                          child: Material(
                            elevation: 20,
                            shape: CircleBorder(),
                            color: Colors.transparent,
                            clipBehavior: Clip.hardEdge,
                            child: Ink(
                              decoration: BoxDecoration(
                                  border: Border.all(
                                      width: 1, color: BangColors.darkBrown),
                                  gradient: BangColors.buttonGradient,
                                  borderRadius: BorderRadius.circular(200)),
                              child: IconButton(
                                onPressed: () {
                                  Get.bottomSheet(
                                    Container(
                                        height: 200,
                                        child: Column(
                                          children: [
                                            Text('Hii 1', textScaleFactor: 2),
                                            Text('Hii 2', textScaleFactor: 2),
                                            Text('Hii 3', textScaleFactor: 2),
                                            Text('Hii 4', textScaleFactor: 2),
                                          ],
                                        )),
                                    barrierColor: Colors.transparent,
                                    isDismissible: true,
                                    backgroundColor: Colors.white,
                                    shape: RoundedRectangleBorder(
                                      borderRadius: BorderRadius.circular(35),
                                      /*side: BorderSide(
                                            width: 1, color: Colors.black)*/
                                    ),
                                    enableDrag: true,
                                    enterBottomSheetDuration:
                                        Duration(milliseconds: 300),
                                    exitBottomSheetDuration:
                                        Duration(milliseconds: 300),
                                  );
                                },
                                icon: Icon(
                                  Icons.chat,
                                  color: BangColors.background,
                                ),
                              ),
                            ),
                          ),
                        ),
                        Padding(
                          padding: const EdgeInsets.all(12.0),
                          child: BangButton(
                            width: 90,
                            height: 40,
                            onPressed: controller.back,
                            text: 'Kilépés',
                          ),
                        ),
                      ],
                    )
                  ],
                ),
              ),
            ),
          ),
        ),
      ),
    );
  }
}
