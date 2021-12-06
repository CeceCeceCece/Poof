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
                    { "45f79e52-0595-412e-a51a-f05d7991d998", "The Barrel allows you to “draw!” when you are the target of a BANG! if you draw a Heart card, you are Missed! (just like if you played a Missed! card)-otherwise nothing happens.", "Barrel", 0, 0, 10 },
                    { "9d9c0cca-354d-49e3-b01d-d06bd7bd9a1b", "The Gatling shoots a BANG! to all the other players, regardless of the distance. Even though the Gatling shoots a BANG! to all the other players, it is not considered a BANG! card. During your turn you can play any number of Gatling, but only one BANG! card.", "Gatling", 1, 2, 8 },
                    { "dfb36793-9b21-488f-b16d-0185db1eeb01", "With this card you can challenge any other player (staring him in the eyes!), regardless of the distance. The challenged player may discard a BANG! card (even though it is not his turn!). If he does, you may discard a BANG! card, and so on: the first player failing to discard a BANG! card loses one life point, and the duel is I over. You cannot play Missed! or use the Barrel during a duel. The Duel is not a BANG! card. BANG! cards discarded during a Duel are not accounted towards the one BANG! card limitation.", "Duel", 2, 2, 6 },
                    { "2af70a2b-e2af-4d73-b667-5f21cc0edcea", "With this card you can challenge any other player (staring him in the eyes!), regardless of the distance. The challenged player may discard a BANG! card (even though it is not his turn!). If he does, you may discard a BANG! card, and so on: the first player failing to discard a BANG! card loses one life point, and the duel is I over. You cannot play Missed! or use the Barrel during a duel. The Duel is not a BANG! card. BANG! cards discarded during a Duel are not accounted towards the one BANG! card limitation.", "Duel", 0, 2, 9 },
                    { "3b116735-0276-4886-aea6-b6f4d4c42bf8", "With this card you can challenge any other player (staring him in the eyes!), regardless of the distance. The challenged player may discard a BANG! card (even though it is not his turn!). If he does, you may discard a BANG! card, and so on: the first player failing to discard a BANG! card loses one life point, and the duel is I over. You cannot play Missed! or use the Barrel during a duel. The Duel is not a BANG! card. BANG! cards discarded during a Duel are not accounted towards the one BANG! card limitation.", "Duel", 3, 2, 10 },
                    { "c00e0aca-aeb3-4ce0-9a82-7c3286b8034f", "Draw two cards from the top of the deck", "Stagecoach", 0, 2, 7 },
                    { "b23b8275-2536-45e4-a414-180ea81bb347", "Draw two cards from the top of the deck", "Stagecoach", 0, 2, 7 },
                    { "5e17bb99-ec1e-4c26-b17a-8b03fb684a05", "Force any one player to discard a card, regardless of the distance.", "Cat Balou", 3, 2, 9 },
                    { "7787a481-55d5-4828-a8b4-762bf2ec5324", "Each player, excluding the one who played this card, may discard a BANG! card, or lose one life point. Neither Missed! nor Barrel have effect in this case.", "Indians!", 3, 2, 11 },
                    { "5a6dfa9a-2eec-4aa9-906a-b6f5023fc4b5", "Force any one player to discard a card, regardless of the distance.", "Cat Balou", 3, 2, 8 },
                    { "2cc9c3d9-6a4e-4c6c-b2d0-e54994230122", "Force any one player to discard a card, regardless of the distance.", "Cat Balou", 1, 2, 11 },
                    { "f3ce6436-394d-4b8d-9432-9104a48cb219", "This card lets you regain one life point - take a bullet from the pile. You cannot gain more life points than your starting amount! The Beer cannot be used to help other players. The Beer can be played in two ways: as usual, during your turn and out of turn, but only if you have just received a hit that is lethal (i.e. a hit that takes away your last life point), and not if you are simply hit. Beer has no effect if there are only 2 players left in the game; in other words, if you play a Beer you do not gain any life point.", "Beer", 1, 2, 9 },
                    { "362c92a4-0b33-43f4-8b58-c5d1d49169fe", "This card lets you regain one life point - take a bullet from the pile. You cannot gain more life points than your starting amount! The Beer cannot be used to help other players. The Beer can be played in two ways: as usual, during your turn and out of turn, but only if you have just received a hit that is lethal (i.e. a hit that takes away your last life point), and not if you are simply hit. Beer has no effect if there are only 2 players left in the game; in other words, if you play a Beer you do not gain any life point.", "Beer", 1, 2, 8 },
                    { "2c4feb12-0634-48df-b1f2-2f229f92f043", "This card lets you regain one life point - take a bullet from the pile. You cannot gain more life points than your starting amount! The Beer cannot be used to help other players. The Beer can be played in two ways: as usual, during your turn and out of turn, but only if you have just received a hit that is lethal (i.e. a hit that takes away your last life point), and not if you are simply hit. Beer has no effect if there are only 2 players left in the game; in other words, if you play a Beer you do not gain any life point.", "Beer", 1, 2, 7 },
                    { "84c50233-4338-4a2d-a7ac-6be36f1d72cb", "This card lets you regain one life point - take a bullet from the pile. You cannot gain more life points than your starting amount! The Beer cannot be used to help other players. The Beer can be played in two ways: as usual, during your turn and out of turn, but only if you have just received a hit that is lethal (i.e. a hit that takes away your last life point), and not if you are simply hit. Beer has no effect if there are only 2 players left in the game; in other words, if you play a Beer you do not gain any life point.", "Beer", 1, 2, 6 },
                    { "b454476a-9ed3-456b-84fa-ead14b219c68", "This card lets you regain one life point - take a bullet from the pile. You cannot gain more life points than your starting amount! The Beer cannot be used to help other players. The Beer can be played in two ways: as usual, during your turn and out of turn, but only if you have just received a hit that is lethal (i.e. a hit that takes away your last life point), and not if you are simply hit. Beer has no effect if there are only 2 players left in the game; in other words, if you play a Beer you do not gain any life point.", "Beer", 1, 2, 4 },
                    { "aa3acb0a-3380-410a-92b1-d762d366b330", "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", "Bang!", 1, 2, 12 },
                    { "3c9a684d-5a97-4421-9c3b-949636b1fe1e", "Force any one player to discard a card, regardless of the distance.", "Cat Balou", 3, 2, 7 },
                    { "77a2d69a-e221-49c1-a7f7-c0a2bc41e144", "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", "Bang!", 1, 2, 11 },
                    { "9662b781-33b7-4edf-9ccc-b318f25b3528", "Each player, excluding the one who played this card, may discard a BANG! card, or lose one life point. Neither Missed! nor Barrel have effect in this case.", "Indians!", 3, 2, 12 },
                    { "4ea2f2da-3358-4612-b697-2504ea3c4241", "If you are hit by a BANG! you may immediately play a Missed! - even though it is not your turn! - to cancel the shot. If you do not, you lose one life point (discard a bullet). Discarded bullet go into a pile in the middle of the table.", "Missed!", 2, 2, 9 },
                    { "04bfd399-4c44-4c92-95ff-d067256e0755", "Draw three cards from the top of the deck", "Wells Fargo", 1, 2, 1 },
                    { "3bc196bb-bf71-4eda-aa16-94dd063b3db9", "Cards with symbols on two lines have two simultaneous effects, one for each line. Here symbols say: Regain one life point, and this applies to All the other players, and on the next line: [You] regain one life point. The overall effect is that all players in play regain one life point. You cannot play a Saloon out of turn when you are losing your last life point: the Saloon is not a Beer!", "Saloon", 1, 2, 3 },
                    { "16493f4b-d373-4362-97e7-228efebdcf64", "The symbols state: Draw a card from a player at distance 1. Remember that this distance is not modified by weapons, but only by cards such as Mustang and/or Scope.", "Panic!", 3, 2, 6 },
                    { "2f3304a8-4094-459b-ac07-9c72c584e959", "The symbols state: Draw a card from a player at distance 1. Remember that this distance is not modified by weapons, but only by cards such as Mustang and/or Scope.", "Panic!", 1, 2, 12 },
                    { "f4c1d2f8-43e3-4aba-bf63-79a3e126c247", "The symbols state: Draw a card from a player at distance 1. Remember that this distance is not modified by weapons, but only by cards such as Mustang and/or Scope.", "Panic!", 1, 2, 10 },
                    { "6fe65725-9ac8-431e-83c6-113a05260dcf", "The symbols state: Draw a card from a player at distance 1. Remember that this distance is not modified by weapons, but only by cards such as Mustang and/or Scope.", "Panic!", 1, 2, 9 },
                    { "a054c90a-788c-46b6-a6c5-61556c408ea7", "If you are hit by a BANG! you may immediately play a Missed! - even though it is not your turn! - to cancel the shot. If you do not, you lose one life point (discard a bullet). Discarded bullet go into a pile in the middle of the table.", "Missed!", 0, 2, 6 },
                    { "a4ffbdb8-b076-4364-ae95-89d4b6dc76ce", "If you are hit by a BANG! you may immediately play a Missed! - even though it is not your turn! - to cancel the shot. If you do not, you lose one life point (discard a bullet). Discarded bullet go into a pile in the middle of the table.", "Missed!", 2, 2, 8 },
                    { "1f1c6f9f-84b9-4a7d-ae51-d1e4d1bcf97d", "If you are hit by a BANG! you may immediately play a Missed! - even though it is not your turn! - to cancel the shot. If you do not, you lose one life point (discard a bullet). Discarded bullet go into a pile in the middle of the table.", "Missed!", 0, 2, 5 },
                    { "b7f44a3e-e74c-458c-8baf-f1d9f96cf157", "If you are hit by a BANG! you may immediately play a Missed! - even though it is not your turn! - to cancel the shot. If you do not, you lose one life point (discard a bullet). Discarded bullet go into a pile in the middle of the table.", "Missed!", 0, 2, 3 },
                    { "c744c2c2-5c69-4a59-93a7-2d6d86b085ba", "If you are hit by a BANG! you may immediately play a Missed! - even though it is not your turn! - to cancel the shot. If you do not, you lose one life point (discard a bullet). Discarded bullet go into a pile in the middle of the table.", "Missed!", 0, 2, 2 },
                    { "73c82a41-3f6b-412a-b2c6-cc6b54998785", "If you are hit by a BANG! you may immediately play a Missed! - even though it is not your turn! - to cancel the shot. If you do not, you lose one life point (discard a bullet). Discarded bullet go into a pile in the middle of the table.", "Missed!", 0, 2, 1 },
                    { "47212c40-0901-4fa6-a2d0-9785aa06b916", "If you are hit by a BANG! you may immediately play a Missed! - even though it is not your turn! - to cancel the shot. If you do not, you lose one life point (discard a bullet). Discarded bullet go into a pile in the middle of the table.", "Missed!", 0, 2, 0 },
                    { "2a730800-3bca-4b2c-ac53-0460c1c7c5a3", "If you are hit by a BANG! you may immediately play a Missed! - even though it is not your turn! - to cancel the shot. If you do not, you lose one life point (discard a bullet). Discarded bullet go into a pile in the middle of the table.", "Missed!", 2, 2, 12 },
                    { "8f26a167-e8b6-47b5-8480-d19bbeae8644", "If you are hit by a BANG! you may immediately play a Missed! - even though it is not your turn! - to cancel the shot. If you do not, you lose one life point (discard a bullet). Discarded bullet go into a pile in the middle of the table.", "Missed!", 2, 2, 11 },
                    { "1b730627-2128-4994-9bfa-e338cb3b25b2", "If you are hit by a BANG! you may immediately play a Missed! - even though it is not your turn! - to cancel the shot. If you do not, you lose one life point (discard a bullet). Discarded bullet go into a pile in the middle of the table.", "Missed!", 2, 2, 10 },
                    { "ed1caf31-b5fc-4c96-8a62-aeda17f0ebd9", "If you are hit by a BANG! you may immediately play a Missed! - even though it is not your turn! - to cancel the shot. If you do not, you lose one life point (discard a bullet). Discarded bullet go into a pile in the middle of the table.", "Missed!", 0, 2, 4 },
                    { "8bd49c33-3bdb-432c-93c0-7f3269290121", "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", "Bang!", 1, 2, 10 },
                    { "00ac6f02-e705-423e-ac4e-76c72fa4c4cb", "This card lets you regain one life point - take a bullet from the pile. You cannot gain more life points than your starting amount! The Beer cannot be used to help other players. The Beer can be played in two ways: as usual, during your turn and out of turn, but only if you have just received a hit that is lethal (i.e. a hit that takes away your last life point), and not if you are simply hit. Beer has no effect if there are only 2 players left in the game; in other words, if you play a Beer you do not gain any life point.", "Beer", 1, 2, 5 },
                    { "803ba610-5380-4524-90de-62d5a349dab5", "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", "Bang!", 2, 2, 6 },
                    { "ea3b6123-5c7f-47d2-ba6d-b3d504797599", "When you have a Scope in play, you see all the other players at a distance decreased by 1.", "Scope", 0, 0, 12 }
                });

            migrationBuilder.InsertData(
                table: "Cards",
                columns: new[] { "Id", "Description", "Name", "Suite", "Type", "Value" },
                values: new object[,]
                {
                    { "3f03b4c4-22e8-4582-8d3b-64ec4752bfc8", "", "Winchester", 2, 1, 8 },
                    { "06798780-7939-4aac-aafd-08b2d18cdee3", "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", "Bang!", 2, 2, 7 },
                    { "79c45b27-7e89-48ab-9829-5a70c7cad80f", "", "Volcanic", 2, 1, 8 },
                    { "93087b57-8f57-439e-8aaa-afb33b0930cd", "", "Schofield", 2, 1, 11 },
                    { "78f2db17-af0e-4200-80a2-b9ca6561d763", "", "Schofield", 2, 1, 10 },
                    { "a5563e73-fa16-4b98-aa37-57493f67ba50", "", "Schofield", 2, 1, 9 },
                    { "868f00ef-2eea-4ae9-ad29-0038059f1642", "", "Carabine", 2, 1, 12 },
                    { "9abf73b3-7788-4e02-bc87-218a000a634f", "", "Remington", 2, 1, 11 },
                    { "ad05e863-a14f-416d-9cfe-867033aa085b", "When you have a Mustang horse in play the distance between other players and you is increased by 1. However, you still see the other players at the normal distance.", "Mustang", 1, 0, 6 },
                    { "35a08511-afd3-4c4b-ba7d-6eb87df11a00", "When you have a Mustang horse in play the distance between other players and you is increased by 1. However, you still see the other players at the normal distance.", "Mustang", 1, 0, 6 },
                    { "37a7e551-fcd2-4ef0-8ee7-3322bbe35ccf", "Play this card in front of any player regardless of the distance: you put him in jail! If you are in jail, you must draw! before the beginning of your turn: if you draw a Heart card, you escape from jail: discard the Jail, and continue your turn as normal otherwise discard the Jail and skip your turn. If you are in Jail you remain a possible target for BANG! cards and can still play response cards (e.g. Missed! and Beer) out of your turn, if necessary. Jail cannot be played on the Sheriff.", "Jail", 0, 0, 8 },
                    { "3fa2b409-d4c7-419d-a48b-a0f14a900ea0", "Play this card in front of any player regardless of the distance: you put him in jail! If you are in jail, you must draw! before the beginning of your turn: if you draw a Heart card, you escape from jail: discard the Jail, and continue your turn as normal otherwise discard the Jail and skip your turn. If you are in Jail you remain a possible target for BANG! cards and can still play response cards (e.g. Missed! and Beer) out of your turn, if necessary. Jail cannot be played on the Sheriff.", "Jail", 1, 0, 2 },
                    { "eec256e3-3b35-4052-b0a2-d0348989c6de", "Play this card in front of any player regardless of the distance: you put him in jail! If you are in jail, you must draw! before the beginning of your turn: if you draw a Heart card, you escape from jail: discard the Jail, and continue your turn as normal otherwise discard the Jail and skip your turn. If you are in Jail you remain a possible target for BANG! cards and can still play response cards (e.g. Missed! and Beer) out of your turn, if necessary. Jail cannot be played on the Sheriff.", "Jail", 0, 0, 9 },
                    { "b1aa8189-1f0c-423a-a44a-10cb43010b7c", "Play this card in front of you: the Dynamite will stay there for a whole turn. When you start your next turn(you have the Dynamite already in play), before the first phase you must draw! if you draw a card showing Spades and a number between 2 and 9, the Dynamite explodes! Discard it and lose 3 life points; otherwise, pass the Dynamite to the player on your left(who will draw! on his turn, etc)..Players keep passing the Dynamite around until it explodes, with the effect explained above, or it is drawn or discarded by a Panic!or a Cat Balou.If you have both the Dynamite and a Jail in play, check the Dynamite first. If you are damaged(or even eliminated!) by a Dynamite, this damage is not considered to be caused by any player.", "Dynamite", 1, 0, 0 },
                    { "1ddbd9f6-8fe0-4777-b5f2-64a10e87120f", "Play this card in front of you: the Dynamite will stay there for a whole turn. When you start your next turn(you have the Dynamite already in play), before the first phase you must draw! if you draw a card showing Spades and a number between 2 and 9, the Dynamite explodes! Discard it and lose 3 life points; otherwise, pass the Dynamite to the player on your left(who will draw! on his turn, etc)..Players keep passing the Dynamite around until it explodes, with the effect explained above, or it is drawn or discarded by a Panic!or a Cat Balou.If you have both the Dynamite and a Jail in play, check the Dynamite first. If you are damaged(or even eliminated!) by a Dynamite, this damage is not considered to be caused by any player.", "Dynamite", 1, 0, 0 },
                    { "30351639-08d8-4d8c-8f2b-139e173eceb2", "The Barrel allows you to “draw!” when you are the target of a BANG! if you draw a Heart card, you are Missed! (just like if you played a Missed! card)-otherwise nothing happens.", "Barrel", 0, 0, 11 },
                    { "d6d3defa-1fed-40f9-a792-bab72c269e50", "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", "Bang!", 0, 2, 12 },
                    { "ac335306-ffc0-43f3-8c92-f90970e411f8", "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", "Bang!", 3, 2, 0 },
                    { "b0e9a4c7-7be2-4874-8440-153f2a882ea7", "", "Volcanic", 0, 1, 8 },
                    { "85dfd984-7ee2-491b-87ef-59b2946eaca5", "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", "Bang!", 3, 2, 2 },
                    { "80f929b9-b1c6-471f-a603-009a6601f10a", "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", "Bang!", 2, 2, 5 },
                    { "99fa6c44-df12-4dd7-bc59-d16734c374e3", "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", "Bang!", 2, 2, 4 },
                    { "f4c87b67-82a5-49c4-ba47-1960d4ec95c9", "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", "Bang!", 2, 2, 3 },
                    { "70f7f637-d77b-4aa9-a3bf-86d4f7a3f09e", "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", "Bang!", 3, 2, 1 },
                    { "6eae4432-ea9e-49d0-add7-62046354afce", "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", "Bang!", 2, 2, 2 },
                    { "c2ee7a6d-be74-4b9a-8b47-07c66ba918e5", "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", "Bang!", 2, 2, 1 },
                    { "3a6cd64b-31e3-4340-943b-b8ebf66be663", "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", "Bang!", 2, 2, 0 },
                    { "b2d9aeab-2464-4ba5-ac55-550f1b9041d7", "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", "Bang!", 3, 2, 11 },
                    { "715035f1-ec26-4da4-b218-1f74d4ab3b3c", "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", "Bang!", 3, 2, 12 },
                    { "f6eaac6c-40bc-456f-bb7a-91fe296ee7f0", "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", "Bang!", 3, 2, 9 },
                    { "fba7e820-06e3-46c2-92cd-3194a29e1704", "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", "Bang!", 3, 2, 8 },
                    { "09909e72-cd1d-432a-9677-54e50f78c2d7", "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", "Bang!", 3, 2, 7 },
                    { "d78a4596-6cbf-4803-a879-9e8404010857", "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", "Bang!", 3, 2, 6 },
                    { "aa6ce42d-db9e-453b-aba9-301dff0bd5dd", "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", "Bang!", 3, 2, 5 },
                    { "061f8e70-712f-42c9-9dff-c2fc7ce61f44", "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", "Bang!", 3, 2, 4 },
                    { "a049ddfa-7e7b-4556-9c1a-ca42d2126807", "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", "Bang!", 3, 2, 3 },
                    { "aa218ec5-32ff-43a4-b552-79688e29b964", "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", "Bang!", 3, 2, 10 }
                });

            migrationBuilder.InsertData(
                table: "CharacterCards",
                columns: new[] { "Id", "Description", "LifePoint", "Name" },
                values: new object[,]
                {
                    { "718cccee-2347-488c-bf7c-01bc6c96e113", "During phase 1 of his turn, he may choose to draw the first card from the top of the discard pile or from the deck. Then, he draws the second card from the deck.", 4, "Pedro Ramirez" },
                    { "af2b3906-81e6-4287-9a26-6d485e4bd817", "At any time, he may discard 2 cards from his hand to regain one life point. If he is willing and able, he can use this ability more than once at a time. But remember: you cannot have more life points than your starting amount!", 4, "Sid Ketchum" },
                    { "5c2c81b1-11bd-4ad0-ba4b-b9f1d7801000", "She is considered to have a Scope in play at all times; she sees the other players at a distance decreased by 1. If she has another real Scope in play, she can count both of them, reducing her distance to all other players by a total of 2.", 4, "Rose Doolan" },
                    { "f8978699-c8c4-4cef-af56-f8d893e33d44", "he is considered to have a Mustang in play at all times; all other players must add 1 to the distance to him. If he has another real Mustang in play, he can count both of them, increasing all distances to him by a total of 2.", 3, "Paul Regret" },
                    { "5732a149-a90d-41ed-90da-feba9823ec6e", "As soon as she has no cards in her hand, she draws a card from the draw pile.", 4, "Suzy Lafayette" }
                });

            migrationBuilder.InsertData(
                table: "CharacterCards",
                columns: new[] { "Id", "Description", "LifePoint", "Name" },
                values: new object[,]
                {
                    { "f4a610a4-3bb3-4196-95c8-d63a6dc0b02c", "During phase 1 of his turn, he may choose to draw the first card from the deck, or randomly from the hand of any other player. Then he draws the second card from the deck.", 4, "Jesse Jones" },
                    { "03e31760-36ed-4f50-a29d-b01bd009d202", "Each time he loses a life point due to a card played by another player, he draws a random card from the hands of that player (one card for each life point). If that player has no more cards, too bad!, he does not draw. Note that Dynamite damages are not caused by any player.", 3, "El Gringo" },
                    { "e2279061-2568-470d-b846-64c7cebf5732", "He shows the second card he draw. On Heart or Diamonds, he draws one more card", 4, "Black Jack" },
                    { "2bef6851-2a20-4075-9b9f-726099267388", "Each time he is hit, he draws a card", 4, "Bart Cassidy" },
                    { "50f46a97-e90e-4e34-8156-bd632cc51202", "During phase 1 of his turn, he looks at the top three cards of the deck: he chooses 2 to draw, and puts the other one back on the top of the deck, face down.", 4, "Kit Carlson" },
                    { "31f241e5-44e3-4ff2-aa12-a5fb4fc49118", "He can play any number of BANG! cards during his turn.", 4, "Willy the Kid" }
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
