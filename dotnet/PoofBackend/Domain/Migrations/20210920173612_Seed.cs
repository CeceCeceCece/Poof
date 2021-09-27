using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class Seed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value = table.Column<int>(type: "int", nullable: false),
                    Suite = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoleCards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LifePoint = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleCards", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Cards",
                columns: new[] { "Id", "Description", "Name", "Suite", "Type", "Value" },
                values: new object[,]
                {
                    { 1, "The Barrel allows you to “draw!” when you are the target of a BANG! if you draw a Heart card, you are Missed! (just like if you played a Missed! card)-otherwise nothing happens.", "Barrel", 0, 0, 10 },
                    { 60, "When you play this card, turn as many cards from the deck face up as the players still playing. Starting with you and proceeding clockwise, each player chooses one of those cards and puts it in his hands.", "General Store", 2, 2, 7 },
                    { 59, "The Gatling shoots a BANG! to all the other players, regardless of the distance. Even though the Gatling shoots a BANG! to all the other players, it is not considered a BANG! card. During your turn you can play any number of Gatling, but only one BANG! card.", "Gatling", 1, 2, 8 },
                    { 58, "With this card you can challenge any other player (staring him in the eyes!), regardless of the distance. The challenged player may discard a BANG! card (even though it is not his turn!). If he does, you may discard a BANG! card, and so on: the first player failing to discard a BANG! card loses one life point, and the duel is I over. You cannot play Missed! or use the Barrel during a duel. The Duel is not a BANG! card. BANG! cards discarded during a Duel are not accounted towards the one BANG! card limitation.", "Duel", 2, 2, 6 },
                    { 57, "With this card you can challenge any other player (staring him in the eyes!), regardless of the distance. The challenged player may discard a BANG! card (even though it is not his turn!). If he does, you may discard a BANG! card, and so on: the first player failing to discard a BANG! card loses one life point, and the duel is I over. You cannot play Missed! or use the Barrel during a duel. The Duel is not a BANG! card. BANG! cards discarded during a Duel are not accounted towards the one BANG! card limitation.", "Duel", 0, 2, 9 },
                    { 56, "With this card you can challenge any other player (staring him in the eyes!), regardless of the distance. The challenged player may discard a BANG! card (even though it is not his turn!). If he does, you may discard a BANG! card, and so on: the first player failing to discard a BANG! card loses one life point, and the duel is I over. You cannot play Missed! or use the Barrel during a duel. The Duel is not a BANG! card. BANG! cards discarded during a Duel are not accounted towards the one BANG! card limitation.", "Duel", 3, 2, 10 },
                    { 55, "Draw two cards from the top of the deck", "Stagecoach", 0, 2, 7 },
                    { 54, "Draw two cards from the top of the deck", "Stagecoach", 0, 2, 7 },
                    { 61, "When you play this card, turn as many cards from the deck face up as the players still playing. Starting with you and proceeding clockwise, each player chooses one of those cards and puts it in his hands.", "General Store", 0, 2, 10 },
                    { 53, "Force any one player to discard a card, regardless of the distance.", "Cat Balou", 3, 2, 9 },
                    { 51, "Force any one player to discard a card, regardless of the distance.", "Cat Balou", 3, 2, 7 },
                    { 50, "Force any one player to discard a card, regardless of the distance.", "Cat Balou", 1, 2, 11 },
                    { 48, "This card lets you regain one life point - take a bullet from the pile. You cannot gain more life points than your starting amount! The Beer cannot be used to help other players. The Beer can be played in two ways: as usual, during your turn and out of turn, but only if you have just received a hit that is lethal (i.e. a hit that takes away your last life point), and not if you are simply hit. Beer has no effect if there are only 2 players left in the game; in other words, if you play a Beer you do not gain any life point.", "Beer", 1, 2, 8 },
                    { 47, "This card lets you regain one life point - take a bullet from the pile. You cannot gain more life points than your starting amount! The Beer cannot be used to help other players. The Beer can be played in two ways: as usual, during your turn and out of turn, but only if you have just received a hit that is lethal (i.e. a hit that takes away your last life point), and not if you are simply hit. Beer has no effect if there are only 2 players left in the game; in other words, if you play a Beer you do not gain any life point.", "Beer", 1, 2, 7 },
                    { 46, "This card lets you regain one life point - take a bullet from the pile. You cannot gain more life points than your starting amount! The Beer cannot be used to help other players. The Beer can be played in two ways: as usual, during your turn and out of turn, but only if you have just received a hit that is lethal (i.e. a hit that takes away your last life point), and not if you are simply hit. Beer has no effect if there are only 2 players left in the game; in other words, if you play a Beer you do not gain any life point.", "Beer", 1, 2, 6 },
                    { 45, "This card lets you regain one life point - take a bullet from the pile. You cannot gain more life points than your starting amount! The Beer cannot be used to help other players. The Beer can be played in two ways: as usual, during your turn and out of turn, but only if you have just received a hit that is lethal (i.e. a hit that takes away your last life point), and not if you are simply hit. Beer has no effect if there are only 2 players left in the game; in other words, if you play a Beer you do not gain any life point.", "Beer", 1, 2, 5 },
                    { 44, "This card lets you regain one life point - take a bullet from the pile. You cannot gain more life points than your starting amount! The Beer cannot be used to help other players. The Beer can be played in two ways: as usual, during your turn and out of turn, but only if you have just received a hit that is lethal (i.e. a hit that takes away your last life point), and not if you are simply hit. Beer has no effect if there are only 2 players left in the game; in other words, if you play a Beer you do not gain any life point.", "Beer", 1, 2, 4 },
                    { 52, "Force any one player to discard a card, regardless of the distance.", "Cat Balou", 3, 2, 8 },
                    { 62, "Each player, excluding the one who played this card, may discard a BANG! card, or lose one life point. Neither Missed! nor Barrel have effect in this case.", "Indians!", 3, 2, 11 },
                    { 63, "Each player, excluding the one who played this card, may discard a BANG! card, or lose one life point. Neither Missed! nor Barrel have effect in this case.", "Indians!", 3, 2, 12 },
                    { 64, "If you are hit by a BANG! you may immediately play a Missed! - even though it is not your turn! - to cancel the shot. If you do not, you lose one life point (discard a bullet). Discarded bullet go into a pile in the middle of the table.", "Missed!", 2, 2, 8 },
                    { 81, "Draw three cards from the top of the deck", "Wells Fargo", 1, 2, 1 },
                    { 80, "Cards with symbols on two lines have two simultaneous effects, one for each line. Here symbols say: Regain one life point, and this applies to All the other players, and on the next line: [You] regain one life point. The overall effect is that all players in play regain one life point. You cannot play a Saloon out of turn when you are losing your last life point: the Saloon is not a Beer!", "Saloon", 1, 2, 3 },
                    { 79, "The symbols state: Draw a card from a player at distance 1. Remember that this distance is not modified by weapons, but only by cards such as Mustang and/or Scope.", "Panic!", 3, 2, 6 },
                    { 78, "The symbols state: Draw a card from a player at distance 1. Remember that this distance is not modified by weapons, but only by cards such as Mustang and/or Scope.", "Panic!", 1, 2, 12 },
                    { 77, "The symbols state: Draw a card from a player at distance 1. Remember that this distance is not modified by weapons, but only by cards such as Mustang and/or Scope.", "Panic!", 1, 2, 10 },
                    { 76, "The symbols state: Draw a card from a player at distance 1. Remember that this distance is not modified by weapons, but only by cards such as Mustang and/or Scope.", "Panic!", 1, 2, 9 },
                    { 75, "If you are hit by a BANG! you may immediately play a Missed! - even though it is not your turn! - to cancel the shot. If you do not, you lose one life point (discard a bullet). Discarded bullet go into a pile in the middle of the table.", "Missed!", 0, 2, 6 },
                    { 74, "If you are hit by a BANG! you may immediately play a Missed! - even though it is not your turn! - to cancel the shot. If you do not, you lose one life point (discard a bullet). Discarded bullet go into a pile in the middle of the table.", "Missed!", 0, 2, 5 },
                    { 73, "If you are hit by a BANG! you may immediately play a Missed! - even though it is not your turn! - to cancel the shot. If you do not, you lose one life point (discard a bullet). Discarded bullet go into a pile in the middle of the table.", "Missed!", 0, 2, 4 },
                    { 72, "If you are hit by a BANG! you may immediately play a Missed! - even though it is not your turn! - to cancel the shot. If you do not, you lose one life point (discard a bullet). Discarded bullet go into a pile in the middle of the table.", "Missed!", 0, 2, 3 },
                    { 71, "If you are hit by a BANG! you may immediately play a Missed! - even though it is not your turn! - to cancel the shot. If you do not, you lose one life point (discard a bullet). Discarded bullet go into a pile in the middle of the table.", "Missed!", 0, 2, 2 },
                    { 70, "If you are hit by a BANG! you may immediately play a Missed! - even though it is not your turn! - to cancel the shot. If you do not, you lose one life point (discard a bullet). Discarded bullet go into a pile in the middle of the table.", "Missed!", 0, 2, 1 },
                    { 69, "If you are hit by a BANG! you may immediately play a Missed! - even though it is not your turn! - to cancel the shot. If you do not, you lose one life point (discard a bullet). Discarded bullet go into a pile in the middle of the table.", "Missed!", 0, 2, 0 },
                    { 68, "If you are hit by a BANG! you may immediately play a Missed! - even though it is not your turn! - to cancel the shot. If you do not, you lose one life point (discard a bullet). Discarded bullet go into a pile in the middle of the table.", "Missed!", 2, 2, 12 },
                    { 67, "If you are hit by a BANG! you may immediately play a Missed! - even though it is not your turn! - to cancel the shot. If you do not, you lose one life point (discard a bullet). Discarded bullet go into a pile in the middle of the table.", "Missed!", 2, 2, 11 },
                    { 66, "If you are hit by a BANG! you may immediately play a Missed! - even though it is not your turn! - to cancel the shot. If you do not, you lose one life point (discard a bullet). Discarded bullet go into a pile in the middle of the table.", "Missed!", 2, 2, 10 },
                    { 65, "If you are hit by a BANG! you may immediately play a Missed! - even though it is not your turn! - to cancel the shot. If you do not, you lose one life point (discard a bullet). Discarded bullet go into a pile in the middle of the table.", "Missed!", 2, 2, 9 },
                    { 43, "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", "Bang", 1, 2, 12 },
                    { 42, "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", "Bang", 1, 2, 11 },
                    { 49, "This card lets you regain one life point - take a bullet from the pile. You cannot gain more life points than your starting amount! The Beer cannot be used to help other players. The Beer can be played in two ways: as usual, during your turn and out of turn, but only if you have just received a hit that is lethal (i.e. a hit that takes away your last life point), and not if you are simply hit. Beer has no effect if there are only 2 players left in the game; in other words, if you play a Beer you do not gain any life point.", "Beer", 1, 2, 9 },
                    { 40, "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", "Bang", 2, 2, 7 }
                });

            migrationBuilder.InsertData(
                table: "Cards",
                columns: new[] { "Id", "Description", "Name", "Suite", "Type", "Value" },
                values: new object[,]
                {
                    { 18, "When you have a Scope in play, you see all the other players at a distance decreased by 1.", "Scope", 0, 0, 12 },
                    { 17, "", "Winchester", 2, 1, 8 },
                    { 16, "", "Volcanic", 0, 1, 8 },
                    { 15, "", "Volcanic", 2, 1, 8 },
                    { 41, "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", "Bang", 1, 2, 10 },
                    { 13, "", "Schofield", 2, 1, 10 },
                    { 12, "", "Schofield", 2, 1, 9 },
                    { 11, "", "Carabine", 2, 1, 12 },
                    { 10, "", "Remington", 2, 1, 11 },
                    { 9, "When you have a Mustang horse in play the distance between other players and you is increased by 1. However, you still see the other players at the normal distance.", "Mustang", 1, 0, 6 },
                    { 8, "When you have a Mustang horse in play the distance between other players and you is increased by 1. However, you still see the other players at the normal distance.", "Mustang", 1, 0, 6 },
                    { 7, "Play this card in front of any player regardless of the distance: you put him in jail! If you are in jail, you must draw! before the beginning of your turn: if you draw a Heart card, you escape from jail: discard the Jail, and continue your turn as normal otherwise discard the Jail and skip your turn. If you are in Jail you remain a possible target for BANG! cards and can still play response cards (e.g. Missed! and Beer) out of your turn, if necessary. Jail cannot be played on the Sheriff.", "Jail", 0, 0, 8 },
                    { 6, "Play this card in front of any player regardless of the distance: you put him in jail! If you are in jail, you must draw! before the beginning of your turn: if you draw a Heart card, you escape from jail: discard the Jail, and continue your turn as normal otherwise discard the Jail and skip your turn. If you are in Jail you remain a possible target for BANG! cards and can still play response cards (e.g. Missed! and Beer) out of your turn, if necessary. Jail cannot be played on the Sheriff.", "Jail", 1, 0, 2 },
                    { 5, "Play this card in front of any player regardless of the distance: you put him in jail! If you are in jail, you must draw! before the beginning of your turn: if you draw a Heart card, you escape from jail: discard the Jail, and continue your turn as normal otherwise discard the Jail and skip your turn. If you are in Jail you remain a possible target for BANG! cards and can still play response cards (e.g. Missed! and Beer) out of your turn, if necessary. Jail cannot be played on the Sheriff.", "Jail", 0, 0, 9 },
                    { 4, "Play this card in front of you: the Dynamite will stay there for a whole turn. When you start your next turn(you have the Dynamite already in play), before the first phase you must draw! if you draw a card showing Spades and a number between 2 and 9, the Dynamite explodes! Discard it and lose 3 life points; otherwise, pass the Dynamite to the player on your left(who will draw! on his turn, etc)..Players keep passing the Dynamite around until it explodes, with the effect explained above, or it is drawn or discarded by a Panic!or a Cat Balou.If you have both the Dynamite and a Jail in play, check the Dynamite first. If you are damaged(or even eliminated!) by a Dynamite, this damage is not considered to be caused by any player.", "Dynamite", 1, 0, 0 },
                    { 3, "Play this card in front of you: the Dynamite will stay there for a whole turn. When you start your next turn(you have the Dynamite already in play), before the first phase you must draw! if you draw a card showing Spades and a number between 2 and 9, the Dynamite explodes! Discard it and lose 3 life points; otherwise, pass the Dynamite to the player on your left(who will draw! on his turn, etc)..Players keep passing the Dynamite around until it explodes, with the effect explained above, or it is drawn or discarded by a Panic!or a Cat Balou.If you have both the Dynamite and a Jail in play, check the Dynamite first. If you are damaged(or even eliminated!) by a Dynamite, this damage is not considered to be caused by any player.", "Dynamite", 1, 0, 0 },
                    { 2, "The Barrel allows you to “draw!” when you are the target of a BANG! if you draw a Heart card, you are Missed! (just like if you played a Missed! card)-otherwise nothing happens.", "Barrel", 0, 0, 11 },
                    { 19, "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", "Bang", 0, 2, 12 },
                    { 20, "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", "Bang", 3, 2, 0 },
                    { 14, "", "Schofield", 2, 1, 11 },
                    { 22, "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", "Bang", 3, 2, 2 },
                    { 39, "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", "Bang", 2, 2, 6 },
                    { 21, "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", "Bang", 3, 2, 1 },
                    { 37, "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", "Bang", 2, 2, 4 },
                    { 36, "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", "Bang", 2, 2, 3 },
                    { 35, "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", "Bang", 2, 2, 2 },
                    { 34, "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", "Bang", 2, 2, 1 },
                    { 33, "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", "Bang", 2, 2, 0 },
                    { 32, "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", "Bang", 3, 2, 12 },
                    { 38, "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", "Bang", 2, 2, 5 },
                    { 30, "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", "Bang", 3, 2, 10 },
                    { 29, "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", "Bang", 3, 2, 9 },
                    { 28, "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", "Bang", 3, 2, 8 },
                    { 27, "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", "Bang", 3, 2, 7 },
                    { 26, "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", "Bang", 3, 2, 6 },
                    { 25, "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", "Bang", 3, 2, 5 },
                    { 24, "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", "Bang", 3, 2, 4 },
                    { 23, "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", "Bang", 3, 2, 3 },
                    { 31, "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", "Bang", 3, 2, 11 }
                });

            migrationBuilder.InsertData(
                table: "RoleCards",
                columns: new[] { "Id", "Description", "LifePoint", "Name" },
                values: new object[,]
                {
                    { 14, "As soon as she has no cards in her hand, she draws a card from the draw pile.", 4, "Suzy Lafayette" },
                    { 13, "Players trying to cancel his BANG! cards need to play 2 Missed!. The Barrel effect, if successfully used, only counts as one Missed!.", 4, "Slab the Killer" },
                    { 12, "At any time, he may discard 2 cards from his hand to regain one life point. If he is willing and able, he can use this ability more than once at a time. But remember: you cannot have more life points than your starting amount!", 4, "Sid Ketchum" }
                });

            migrationBuilder.InsertData(
                table: "RoleCards",
                columns: new[] { "Id", "Description", "LifePoint", "Name" },
                values: new object[,]
                {
                    { 11, "She is considered to have a Scope in play at all times; she sees the other players at a distance decreased by 1. If she has another real Scope in play, she can count both of them, reducing her distance to all other players by a total of 2.", 4, "Rose Doolan" },
                    { 10, "During phase 1 of his turn, he may choose to draw the first card from the top of the discard pile or from the deck. Then, he draws the second card from the deck.", 4, "Pedro Ramirez" },
                    { 9, "he is considered to have a Mustang in play at all times; all other players must add 1 to the distance to him. If he has another real Mustang in play, he can count both of them, increasing all distances to him by a total of 2.", 3, "Paul Regret" },
                    { 8, "Each time he is required to draw!, he flips the top two cards from the deck and chooses the result he prefers. Discard both cards afterward.", 4, "Lucky Duke" },
                    { 4, "Each time he loses a life point due to a card played by another player, he draws a random card from the hands of that player (one card for each life point). If that player has no more cards, too bad!, he does not draw. Note that Dynamite damages are not caused by any player.", 3, "El Gringo" },
                    { 6, "He is considered to have a Barrel in play at all times; he can draw! when he is the target of a BANG!, and on a Heart he is missed. If he has another real Barrel card in play, he can count both of them, giving him two chances to cancel the BANG! before playing a Missed!.", 4, "Jourdonnais" },
                    { 5, "During phase 1 of his turn, he may choose to draw the first card from the deck, or randomly from the hand of any other player. Then he draws the second card from the deck.", 4, "Jesse Jones" },
                    { 3, "She can use BANG! cards as Missed! cards and vice versa. If she plays a Missed! as a BANG!, she cannot play another BANG! card that turn (unless she has a Volcanic in play).", 4, "Calamity Janet" },
                    { 2, "He shows the second card he draw. On Heart or Diamonds, he draws one more card", 4, "Black Jack" },
                    { 1, "Each time he is hit, he draws a card", 4, "Bart Cassidy" },
                    { 15, "Whenever a character is eliminated from the game, Sam takes all the cards that player had in his hand and in play, and adds them to his hand.", 4, "Vulture Sam" },
                    { 7, "During phase 1 of his turn, he looks at the top three cards of the deck: he chooses 2 to draw, and puts the other one back on the top of the deck, face down.", 4, "Kit Carlson" },
                    { 16, "He can play any number of BANG! cards during his turn.", 4, "Willy the Kid" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cards");

            migrationBuilder.DropTable(
                name: "RoleCards");
        }
    }
}
