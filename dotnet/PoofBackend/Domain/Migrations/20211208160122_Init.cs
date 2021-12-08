using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cards",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
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
                name: "CharacterCards",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LifePoint = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterCards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Lobbies",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Vezeto = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lobbies", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Connections",
                columns: table => new
                {
                    ConnectionId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LobbyName = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Connections", x => x.ConnectionId);
                    table.ForeignKey(
                        name: "FK_Connections_Lobbies_LobbyName",
                        column: x => x.LobbyName,
                        principalTable: "Lobbies",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                });

            migrationBuilder.CreateTable(
                name: "GameCards",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CardId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CharacterId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CharacterId1 = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    GameId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    GameId1 = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameCards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameCards_Cards_CardId",
                        column: x => x.CardId,
                        principalTable: "Cards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Event = table.Column<int>(type: "int", nullable: false),
                    CurrentUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NextUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Win = table.Column<int>(type: "int", nullable: false),
                    NextCardId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Games_GameCards_NextCardId",
                        column: x => x.NextCardId,
                        principalTable: "GameCards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    GameId = table.Column<int>(type: "int", nullable: true),
                    GameId1 = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Games_GameId1",
                        column: x => x.GameId1,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Characters",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConnectionId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaxLifePoint = table.Column<int>(type: "int", nullable: false),
                    LifePoint = table.Column<int>(type: "int", nullable: false),
                    BangState = table.Column<int>(type: "int", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    WeaponDistance = table.Column<int>(type: "int", nullable: false),
                    AimDistance = table.Column<int>(type: "int", nullable: false),
                    DistanceFromOthers = table.Column<int>(type: "int", nullable: false),
                    GameId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    WeaponId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    PersonalCardId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Characters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Characters_CharacterCards_PersonalCardId",
                        column: x => x.PersonalCardId,
                        principalTable: "CharacterCards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Characters_GameCards_WeaponId",
                        column: x => x.WeaponId,
                        principalTable: "GameCards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Characters_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Kuldo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tartalom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Datum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GameId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    LobbyName = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Messages_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Messages_Lobbies_LobbyName",
                        column: x => x.LobbyName,
                        principalTable: "Lobbies",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Cards",
                columns: new[] { "Id", "Description", "Name", "Suite", "Type", "Value" },
                values: new object[,]
                {
                    { "0d9e71f4-a17d-40ee-8be7-f18381af6451", "The Barrel allows you to “draw!” when you are the target of a BANG! if you draw a Heart card, you are Missed! (just like if you played a Missed! card)-otherwise nothing happens.", "Barrel", 0, 0, 10 },
                    { "c45f1747-366b-44fe-ab65-0b9c27cf02f9", "The Gatling shoots a BANG! to all the other players, regardless of the distance. Even though the Gatling shoots a BANG! to all the other players, it is not considered a BANG! card. During your turn you can play any number of Gatling, but only one BANG! card.", "Gatling", 1, 2, 8 },
                    { "310ca9a3-375c-4fe4-b923-f8c3374d5897", "With this card you can challenge any other player (staring him in the eyes!), regardless of the distance. The challenged player may discard a BANG! card (even though it is not his turn!). If he does, you may discard a BANG! card, and so on: the first player failing to discard a BANG! card loses one life point, and the duel is I over. You cannot play Missed! or use the Barrel during a duel. The Duel is not a BANG! card. BANG! cards discarded during a Duel are not accounted towards the one BANG! card limitation.", "Duel", 2, 2, 6 },
                    { "3707c918-ac70-4274-90b9-bc35be0114f9", "With this card you can challenge any other player (staring him in the eyes!), regardless of the distance. The challenged player may discard a BANG! card (even though it is not his turn!). If he does, you may discard a BANG! card, and so on: the first player failing to discard a BANG! card loses one life point, and the duel is I over. You cannot play Missed! or use the Barrel during a duel. The Duel is not a BANG! card. BANG! cards discarded during a Duel are not accounted towards the one BANG! card limitation.", "Duel", 0, 2, 9 },
                    { "0f3d8d33-c4d1-46eb-9aa7-00da458e4999", "With this card you can challenge any other player (staring him in the eyes!), regardless of the distance. The challenged player may discard a BANG! card (even though it is not his turn!). If he does, you may discard a BANG! card, and so on: the first player failing to discard a BANG! card loses one life point, and the duel is I over. You cannot play Missed! or use the Barrel during a duel. The Duel is not a BANG! card. BANG! cards discarded during a Duel are not accounted towards the one BANG! card limitation.", "Duel", 3, 2, 10 },
                    { "c07caac6-6309-4500-9e8d-ec535061a4ec", "Draw two cards from the top of the deck", "Stagecoach", 0, 2, 7 },
                    { "e91a9287-b453-44c1-9a6d-364f57e004d3", "Draw two cards from the top of the deck", "Stagecoach", 0, 2, 7 },
                    { "f3a00800-b889-474b-ac20-272594a22f96", "Force any one player to discard a card, regardless of the distance.", "Cat Balou", 3, 2, 9 },
                    { "c12e9153-59b4-4159-9bda-a470bb8523eb", "Each player, excluding the one who played this card, may discard a BANG! card, or lose one life point. Neither Missed! nor Barrel have effect in this case.", "Indians!", 3, 2, 11 },
                    { "4cee97b9-eb81-4203-a570-80513df8f3ab", "Force any one player to discard a card, regardless of the distance.", "Cat Balou", 3, 2, 8 },
                    { "2a601c80-d0b2-4ef2-884c-de5ae16d0f5b", "Force any one player to discard a card, regardless of the distance.", "Cat Balou", 1, 2, 11 },
                    { "1d5b9193-e6ae-442d-8ed1-101256fbcbec", "This card lets you regain one life point - take a bullet from the pile. You cannot gain more life points than your starting amount! The Beer cannot be used to help other players. The Beer can be played in two ways: as usual, during your turn and out of turn, but only if you have just received a hit that is lethal (i.e. a hit that takes away your last life point), and not if you are simply hit. Beer has no effect if there are only 2 players left in the game; in other words, if you play a Beer you do not gain any life point.", "Beer", 1, 2, 9 },
                    { "52a2f4f4-85b1-4abe-ae7b-9fe8f3b3a458", "This card lets you regain one life point - take a bullet from the pile. You cannot gain more life points than your starting amount! The Beer cannot be used to help other players. The Beer can be played in two ways: as usual, during your turn and out of turn, but only if you have just received a hit that is lethal (i.e. a hit that takes away your last life point), and not if you are simply hit. Beer has no effect if there are only 2 players left in the game; in other words, if you play a Beer you do not gain any life point.", "Beer", 1, 2, 8 },
                    { "de66ea8e-5d7f-42dd-9418-69a88e201b5f", "This card lets you regain one life point - take a bullet from the pile. You cannot gain more life points than your starting amount! The Beer cannot be used to help other players. The Beer can be played in two ways: as usual, during your turn and out of turn, but only if you have just received a hit that is lethal (i.e. a hit that takes away your last life point), and not if you are simply hit. Beer has no effect if there are only 2 players left in the game; in other words, if you play a Beer you do not gain any life point.", "Beer", 1, 2, 7 },
                    { "701d51d1-369f-40f7-837e-984972ac6e95", "This card lets you regain one life point - take a bullet from the pile. You cannot gain more life points than your starting amount! The Beer cannot be used to help other players. The Beer can be played in two ways: as usual, during your turn and out of turn, but only if you have just received a hit that is lethal (i.e. a hit that takes away your last life point), and not if you are simply hit. Beer has no effect if there are only 2 players left in the game; in other words, if you play a Beer you do not gain any life point.", "Beer", 1, 2, 5 },
                    { "f1107166-0f8d-4d2b-8be8-3beaf57d3945", "This card lets you regain one life point - take a bullet from the pile. You cannot gain more life points than your starting amount! The Beer cannot be used to help other players. The Beer can be played in two ways: as usual, during your turn and out of turn, but only if you have just received a hit that is lethal (i.e. a hit that takes away your last life point), and not if you are simply hit. Beer has no effect if there are only 2 players left in the game; in other words, if you play a Beer you do not gain any life point.", "Beer", 1, 2, 4 },
                    { "07160c04-4d19-49e1-8f4c-514afbe5698f", "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", "Bang!", 1, 2, 12 },
                    { "d51fbf04-d288-4b78-b9e2-8dcdc7ef5632", "Force any one player to discard a card, regardless of the distance.", "Cat Balou", 3, 2, 7 },
                    { "115cba3b-3b62-45b9-a9c0-393075b27854", "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", "Bang!", 1, 2, 11 },
                    { "b7832fca-80df-4f11-8bd6-2b13df082824", "Each player, excluding the one who played this card, may discard a BANG! card, or lose one life point. Neither Missed! nor Barrel have effect in this case.", "Indians!", 3, 2, 12 },
                    { "852fb203-d178-447b-a4e2-583fe8154bce", "If you are hit by a BANG! you may immediately play a Missed! - even though it is not your turn! - to cancel the shot. If you do not, you lose one life point (discard a bullet). Discarded bullet go into a pile in the middle of the table.", "Missed!", 2, 2, 9 },
                    { "6fab0027-00cb-46ec-af1e-6758a9e5ce14", "Draw three cards from the top of the deck", "Wells Fargo", 1, 2, 1 },
                    { "5365515e-df5e-4eb1-ad55-a27e7144d07a", "Cards with symbols on two lines have two simultaneous effects, one for each line. Here symbols say: Regain one life point, and this applies to All the other players, and on the next line: [You] regain one life point. The overall effect is that all players in play regain one life point. You cannot play a Saloon out of turn when you are losing your last life point: the Saloon is not a Beer!", "Saloon", 1, 2, 3 },
                    { "2f06affa-e92b-4a60-8cb8-aa5e2987cb1d", "The symbols state: Draw a card from a player at distance 1. Remember that this distance is not modified by weapons, but only by cards such as Mustang and/or Scope.", "Panic!", 3, 2, 6 },
                    { "6aaed3bf-cd98-49a9-8204-11c354d77016", "The symbols state: Draw a card from a player at distance 1. Remember that this distance is not modified by weapons, but only by cards such as Mustang and/or Scope.", "Panic!", 1, 2, 12 },
                    { "3700bad1-e3d2-4038-a548-c7ca958d0919", "The symbols state: Draw a card from a player at distance 1. Remember that this distance is not modified by weapons, but only by cards such as Mustang and/or Scope.", "Panic!", 1, 2, 10 },
                    { "e58497a4-27d8-4821-83c4-368f4f572bf7", "The symbols state: Draw a card from a player at distance 1. Remember that this distance is not modified by weapons, but only by cards such as Mustang and/or Scope.", "Panic!", 1, 2, 9 },
                    { "e70b6d8d-a8cb-48cb-b30d-7c009331e4d9", "If you are hit by a BANG! you may immediately play a Missed! - even though it is not your turn! - to cancel the shot. If you do not, you lose one life point (discard a bullet). Discarded bullet go into a pile in the middle of the table.", "Missed!", 0, 2, 6 },
                    { "73575b4d-a301-490f-b708-db442cb74c31", "If you are hit by a BANG! you may immediately play a Missed! - even though it is not your turn! - to cancel the shot. If you do not, you lose one life point (discard a bullet). Discarded bullet go into a pile in the middle of the table.", "Missed!", 2, 2, 8 },
                    { "e35dc557-5109-4bb0-84cb-0c63e60ea1b6", "If you are hit by a BANG! you may immediately play a Missed! - even though it is not your turn! - to cancel the shot. If you do not, you lose one life point (discard a bullet). Discarded bullet go into a pile in the middle of the table.", "Missed!", 0, 2, 5 },
                    { "38e0fcd4-3bf8-4c54-b2df-96b27ac4fc7d", "If you are hit by a BANG! you may immediately play a Missed! - even though it is not your turn! - to cancel the shot. If you do not, you lose one life point (discard a bullet). Discarded bullet go into a pile in the middle of the table.", "Missed!", 0, 2, 3 },
                    { "be06ce0a-e3ea-4117-a65b-a32df4e53536", "If you are hit by a BANG! you may immediately play a Missed! - even though it is not your turn! - to cancel the shot. If you do not, you lose one life point (discard a bullet). Discarded bullet go into a pile in the middle of the table.", "Missed!", 0, 2, 2 },
                    { "d05ded32-c0a7-4142-b8b8-434e1b8f610a", "If you are hit by a BANG! you may immediately play a Missed! - even though it is not your turn! - to cancel the shot. If you do not, you lose one life point (discard a bullet). Discarded bullet go into a pile in the middle of the table.", "Missed!", 0, 2, 1 },
                    { "1683cddb-ac8a-4144-bce1-ffa16ce6ecd3", "If you are hit by a BANG! you may immediately play a Missed! - even though it is not your turn! - to cancel the shot. If you do not, you lose one life point (discard a bullet). Discarded bullet go into a pile in the middle of the table.", "Missed!", 0, 2, 0 },
                    { "995b623f-d8db-429c-84bc-63595591c2ae", "If you are hit by a BANG! you may immediately play a Missed! - even though it is not your turn! - to cancel the shot. If you do not, you lose one life point (discard a bullet). Discarded bullet go into a pile in the middle of the table.", "Missed!", 2, 2, 12 },
                    { "bce881dd-bb7f-4363-9c63-a2279171156d", "If you are hit by a BANG! you may immediately play a Missed! - even though it is not your turn! - to cancel the shot. If you do not, you lose one life point (discard a bullet). Discarded bullet go into a pile in the middle of the table.", "Missed!", 2, 2, 11 },
                    { "595c6b60-e1ae-4080-b732-ddc0ad6a01af", "If you are hit by a BANG! you may immediately play a Missed! - even though it is not your turn! - to cancel the shot. If you do not, you lose one life point (discard a bullet). Discarded bullet go into a pile in the middle of the table.", "Missed!", 2, 2, 10 },
                    { "aa95e1ec-e8d4-43c9-a796-21ee85b116a0", "If you are hit by a BANG! you may immediately play a Missed! - even though it is not your turn! - to cancel the shot. If you do not, you lose one life point (discard a bullet). Discarded bullet go into a pile in the middle of the table.", "Missed!", 0, 2, 4 },
                    { "dacadaf9-9c72-4506-8ad6-bfee9ef552a7", "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", "Bang!", 1, 2, 10 },
                    { "7a96ca13-0092-46e4-a10b-43cd9cbab56e", "This card lets you regain one life point - take a bullet from the pile. You cannot gain more life points than your starting amount! The Beer cannot be used to help other players. The Beer can be played in two ways: as usual, during your turn and out of turn, but only if you have just received a hit that is lethal (i.e. a hit that takes away your last life point), and not if you are simply hit. Beer has no effect if there are only 2 players left in the game; in other words, if you play a Beer you do not gain any life point.", "Beer", 1, 2, 6 },
                    { "c6e757af-5d55-4a29-982f-f51f48bc1cc1", "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", "Bang!", 2, 2, 6 },
                    { "4151480e-e651-4f7e-8fe0-1ecb91345e82", "When you have a Scope in play, you see all the other players at a distance decreased by 1.", "Scope", 0, 0, 12 }
                });

            migrationBuilder.InsertData(
                table: "Cards",
                columns: new[] { "Id", "Description", "Name", "Suite", "Type", "Value" },
                values: new object[,]
                {
                    { "5b65c41a-e108-4d60-9eb8-c0bc545b68d0", "", "Winchester", 2, 1, 8 },
                    { "ff4d1f03-4836-4f6e-9455-713aaf52d1a3", "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", "Bang!", 2, 2, 7 },
                    { "10f86a25-dc51-4e18-9c8b-25f0f7d38b53", "", "Volcanic", 2, 1, 8 },
                    { "c4d776e2-4195-4f8b-a5dd-e161c67e919b", "", "Schofield", 2, 1, 11 },
                    { "8d1a7771-5e9d-45a9-9ad9-738f6bf80324", "", "Schofield", 2, 1, 10 },
                    { "46c267cf-1de8-4cec-bb15-37d6c48822a9", "", "Schofield", 2, 1, 9 },
                    { "3d74d923-f391-4d51-bed8-de83fba6eb7f", "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", "Bang!", 0, 2, 12 },
                    { "1eab3a3b-a8ba-406a-8011-c3aad33136c6", "", "Carabine", 2, 1, 12 },
                    { "960b391c-b03b-40a0-adad-231584d1b187", "When you have a Mustang horse in play the distance between other players and you is increased by 1. However, you still see the other players at the normal distance.", "Mustang", 1, 0, 6 },
                    { "58f1d61a-6524-43bc-a464-1026ca6b9062", "When you have a Mustang horse in play the distance between other players and you is increased by 1. However, you still see the other players at the normal distance.", "Mustang", 1, 0, 6 },
                    { "6e7db904-8f52-4d77-93f2-b7c37091a2c9", "Play this card in front of any player regardless of the distance: you put him in jail! If you are in jail, you must draw! before the beginning of your turn: if you draw a Heart card, you escape from jail: discard the Jail, and continue your turn as normal otherwise discard the Jail and skip your turn. If you are in Jail you remain a possible target for BANG! cards and can still play response cards (e.g. Missed! and Beer) out of your turn, if necessary. Jail cannot be played on the Sheriff.", "Jail", 0, 0, 8 },
                    { "f12b837d-6e1b-4809-92a9-2754e1f42d49", "Play this card in front of any player regardless of the distance: you put him in jail! If you are in jail, you must draw! before the beginning of your turn: if you draw a Heart card, you escape from jail: discard the Jail, and continue your turn as normal otherwise discard the Jail and skip your turn. If you are in Jail you remain a possible target for BANG! cards and can still play response cards (e.g. Missed! and Beer) out of your turn, if necessary. Jail cannot be played on the Sheriff.", "Jail", 1, 0, 2 },
                    { "956cc4f3-e1b9-403c-82b5-9175b8d9ff24", "Play this card in front of any player regardless of the distance: you put him in jail! If you are in jail, you must draw! before the beginning of your turn: if you draw a Heart card, you escape from jail: discard the Jail, and continue your turn as normal otherwise discard the Jail and skip your turn. If you are in Jail you remain a possible target for BANG! cards and can still play response cards (e.g. Missed! and Beer) out of your turn, if necessary. Jail cannot be played on the Sheriff.", "Jail", 0, 0, 9 },
                    { "f02b01c8-2b23-4530-81dd-21d4c686556d", "Play this card in front of you: the Dynamite will stay there for a whole turn. When you start your next turn(you have the Dynamite already in play), before the first phase you must draw! if you draw a card showing Spades and a number between 2 and 9, the Dynamite explodes! Discard it and lose 3 life points; otherwise, pass the Dynamite to the player on your left(who will draw! on his turn, etc)..Players keep passing the Dynamite around until it explodes, with the effect explained above, or it is drawn or discarded by a Panic!or a Cat Balou.If you have both the Dynamite and a Jail in play, check the Dynamite first. If you are damaged(or even eliminated!) by a Dynamite, this damage is not considered to be caused by any player.", "Dynamite", 1, 0, 0 },
                    { "d45a8e60-eb81-4b54-832f-e842774020c0", "The Barrel allows you to “draw!” when you are the target of a BANG! if you draw a Heart card, you are Missed! (just like if you played a Missed! card)-otherwise nothing happens.", "Barrel", 0, 0, 11 },
                    { "6ad0740b-df50-405e-9a52-a44676ea6b97", "", "Remington", 2, 1, 11 },
                    { "2ff23c46-966b-47b5-8360-adb8e55f9f38", "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", "Bang!", 3, 2, 0 },
                    { "b65292b3-e8fe-40fc-b22d-ba141df8beaf", "", "Volcanic", 0, 1, 8 },
                    { "aed6bdd2-e208-4230-a77e-1e670c3ae8e3", "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", "Bang!", 3, 2, 2 },
                    { "45d9116e-e877-4921-ad90-f246ddb0f1e7", "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", "Bang!", 2, 2, 5 },
                    { "1e745db9-bb94-4daf-823c-7b689746bb7d", "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", "Bang!", 2, 2, 4 },
                    { "be4233b5-409b-4f8d-bdf9-d83354c9016f", "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", "Bang!", 2, 2, 3 },
                    { "4c5bd04e-c55a-464a-941d-a2deb3dd0846", "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", "Bang!", 3, 2, 1 },
                    { "7c0a480a-ac57-4dd5-8e78-e6207ea5f3a9", "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", "Bang!", 2, 2, 2 },
                    { "831bfb38-ef2e-4cbc-b3e7-ff79c5c9a8f4", "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", "Bang!", 2, 2, 1 },
                    { "ab002d04-4a2e-47db-9ac9-16659504ff48", "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", "Bang!", 2, 2, 0 },
                    { "d05c7d4a-ccaa-47e1-a011-fcad5aad7538", "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", "Bang!", 3, 2, 11 },
                    { "45d069cb-820b-4b2c-ac05-e79770ca55e4", "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", "Bang!", 3, 2, 12 },
                    { "d3bd7fed-19eb-463f-962d-fc9a25ed8b42", "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", "Bang!", 3, 2, 9 },
                    { "2e8c185d-6036-4f38-894f-01a8de30f074", "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", "Bang!", 3, 2, 8 },
                    { "d1fab0f3-b7f4-4975-9e10-20a3a3bf45a0", "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", "Bang!", 3, 2, 7 },
                    { "649aca74-6545-485a-a0e6-d7c96ca5c8c3", "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", "Bang!", 3, 2, 6 },
                    { "e5efdd54-af99-44c3-bd47-797a96b57a3e", "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", "Bang!", 3, 2, 5 },
                    { "754f2940-aa8c-4d14-9059-389d69d5ec8d", "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", "Bang!", 3, 2, 4 },
                    { "e357742a-5ff3-44f4-81de-d9f0a90d68c7", "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", "Bang!", 3, 2, 3 },
                    { "d45073a6-e641-4b58-a5d6-4d7f50cb44cc", "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", "Bang!", 3, 2, 10 }
                });

            migrationBuilder.InsertData(
                table: "CharacterCards",
                columns: new[] { "Id", "Description", "LifePoint", "Name" },
                values: new object[,]
                {
                    { "a22c8dfb-760a-43a4-8ab6-43b3cd030f2b", "During phase 1 of his turn, he may choose to draw the first card from the top of the discard pile or from the deck. Then, he draws the second card from the deck.", 4, "Pedro Ramirez" },
                    { "f923fc6f-3790-4adc-a99a-37bfb3e1cbd6", "At any time, he may discard 2 cards from his hand to regain one life point. If he is willing and able, he can use this ability more than once at a time. But remember: you cannot have more life points than your starting amount!", 4, "Sid Ketchum" },
                    { "d1115af0-fbbf-4aff-a181-ade35a65b80e", "She is considered to have a Scope in play at all times; she sees the other players at a distance decreased by 1. If she has another real Scope in play, she can count both of them, reducing her distance to all other players by a total of 2.", 4, "Rose Doolan" },
                    { "c84dcd65-2422-4106-9213-1357864135d6", "he is considered to have a Mustang in play at all times; all other players must add 1 to the distance to him. If he has another real Mustang in play, he can count both of them, increasing all distances to him by a total of 2.", 3, "Paul Regret" },
                    { "0fb84f30-3f48-4d60-9a90-c0d6ba743bb7", "As soon as she has no cards in her hand, she draws a card from the draw pile.", 4, "Suzy Lafayette" },
                    { "a6236540-0985-4527-9a0f-94be10b8fbf9", "During phase 1 of his turn, he may choose to draw the first card from the deck, or randomly from the hand of any other player. Then he draws the second card from the deck.", 4, "Jesse Jones" }
                });

            migrationBuilder.InsertData(
                table: "CharacterCards",
                columns: new[] { "Id", "Description", "LifePoint", "Name" },
                values: new object[,]
                {
                    { "3c3558de-9d72-4ba0-8a07-10c962390cf9", "Each time he loses a life point due to a card played by another player, he draws a random card from the hands of that player (one card for each life point). If that player has no more cards, too bad!, he does not draw. Note that Dynamite damages are not caused by any player.", 3, "El Gringo" },
                    { "a2e7f02b-0bb8-4ab5-93f9-5b8e24fce01f", "He shows the second card he draw. On Heart or Diamonds, he draws one more card", 4, "Black Jack" },
                    { "14aceacc-cfe2-48d1-9398-217e40b53e13", "Each time he is hit, he draws a card", 4, "Bart Cassidy" },
                    { "4bfac63d-aac1-4dcf-bd47-7ff343aa01e8", "During phase 1 of his turn, he looks at the top three cards of the deck: he chooses 2 to draw, and puts the other one back on the top of the deck, face down.", 4, "Kit Carlson" },
                    { "7e54ce83-ebb6-4e4b-8a54-063dcbc4e36d", "He can play any number of BANG! cards during his turn.", 4, "Willy the Kid" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_GameId1",
                table: "AspNetUsers",
                column: "GameId1");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Characters_GameId",
                table: "Characters",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_Characters_PersonalCardId",
                table: "Characters",
                column: "PersonalCardId");

            migrationBuilder.CreateIndex(
                name: "IX_Characters_WeaponId",
                table: "Characters",
                column: "WeaponId");

            migrationBuilder.CreateIndex(
                name: "IX_Connections_LobbyName",
                table: "Connections",
                column: "LobbyName");

            migrationBuilder.CreateIndex(
                name: "IX_GameCards_CardId",
                table: "GameCards",
                column: "CardId");

            migrationBuilder.CreateIndex(
                name: "IX_GameCards_CharacterId",
                table: "GameCards",
                column: "CharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_GameCards_CharacterId1",
                table: "GameCards",
                column: "CharacterId1");

            migrationBuilder.CreateIndex(
                name: "IX_GameCards_GameId",
                table: "GameCards",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_GameCards_GameId1",
                table: "GameCards",
                column: "GameId1");

            migrationBuilder.CreateIndex(
                name: "IX_Games_NextCardId",
                table: "Games",
                column: "NextCardId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_GameId",
                table: "Messages",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_LobbyName",
                table: "Messages",
                column: "LobbyName");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GameCards_Characters_CharacterId",
                table: "GameCards",
                column: "CharacterId",
                principalTable: "Characters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GameCards_Characters_CharacterId1",
                table: "GameCards",
                column: "CharacterId1",
                principalTable: "Characters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GameCards_Games_GameId",
                table: "GameCards",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GameCards_Games_GameId1",
                table: "GameCards",
                column: "GameId1",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Characters_Games_GameId",
                table: "Characters");

            migrationBuilder.DropForeignKey(
                name: "FK_GameCards_Games_GameId",
                table: "GameCards");

            migrationBuilder.DropForeignKey(
                name: "FK_GameCards_Games_GameId1",
                table: "GameCards");

            migrationBuilder.DropForeignKey(
                name: "FK_Characters_CharacterCards_PersonalCardId",
                table: "Characters");

            migrationBuilder.DropForeignKey(
                name: "FK_Characters_GameCards_WeaponId",
                table: "Characters");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Connections");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Lobbies");

            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "CharacterCards");

            migrationBuilder.DropTable(
                name: "GameCards");

            migrationBuilder.DropTable(
                name: "Cards");

            migrationBuilder.DropTable(
                name: "Characters");
        }
    }
}
