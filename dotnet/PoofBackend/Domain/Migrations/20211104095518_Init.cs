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
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
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
                name: "RoleCards",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LifePoint = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleCards", x => x.Id);
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
                name: "GameCards",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CardId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    GameId = table.Column<string>(type: "nvarchar(450)", nullable: true)
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
                    table.ForeignKey(
                        name: "FK_GameCards_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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

            migrationBuilder.CreateTable(
                name: "Characters",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConnectionId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LifePoint = table.Column<int>(type: "int", nullable: false),
                    NumberOfBangs = table.Column<int>(type: "int", nullable: false),
                    AimDistance = table.Column<int>(type: "int", nullable: false),
                    Distance = table.Column<int>(type: "int", nullable: false),
                    GameId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    WeaponId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Characters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Characters_Cards_WeaponId",
                        column: x => x.WeaponId,
                        principalTable: "Cards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Characters_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Characters_RoleCards_RoleId",
                        column: x => x.RoleId,
                        principalTable: "RoleCards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Cards",
                columns: new[] { "Id", "Description", "Name", "Suite", "Type", "Value" },
                values: new object[,]
                {
                    { "1e5ec8e7-c1d7-4deb-b4f5-31e4fd717a70", "The Barrel allows you to “draw!” when you are the target of a BANG! if you draw a Heart card, you are Missed! (just like if you played a Missed! card)-otherwise nothing happens.", "Barrel", 0, 0, 10 },
                    { "6b8e207a-dd84-41c4-a940-66efb46e0d5e", "When you play this card, turn as many cards from the deck face up as the players still playing. Starting with you and proceeding clockwise, each player chooses one of those cards and puts it in his hands.", "General Store", 2, 2, 7 },
                    { "985e1e59-b3f9-4164-b100-ca8012686995", "The Gatling shoots a BANG! to all the other players, regardless of the distance. Even though the Gatling shoots a BANG! to all the other players, it is not considered a BANG! card. During your turn you can play any number of Gatling, but only one BANG! card.", "Gatling", 1, 2, 8 },
                    { "b6c8cf93-f0a3-4894-9716-37dd5de16d38", "With this card you can challenge any other player (staring him in the eyes!), regardless of the distance. The challenged player may discard a BANG! card (even though it is not his turn!). If he does, you may discard a BANG! card, and so on: the first player failing to discard a BANG! card loses one life point, and the duel is I over. You cannot play Missed! or use the Barrel during a duel. The Duel is not a BANG! card. BANG! cards discarded during a Duel are not accounted towards the one BANG! card limitation.", "Duel", 2, 2, 6 },
                    { "7a51deef-4682-4d06-98bb-dc514811bf52", "With this card you can challenge any other player (staring him in the eyes!), regardless of the distance. The challenged player may discard a BANG! card (even though it is not his turn!). If he does, you may discard a BANG! card, and so on: the first player failing to discard a BANG! card loses one life point, and the duel is I over. You cannot play Missed! or use the Barrel during a duel. The Duel is not a BANG! card. BANG! cards discarded during a Duel are not accounted towards the one BANG! card limitation.", "Duel", 0, 2, 9 },
                    { "8d16c01d-2b55-4499-abb6-bd9c2c3e3502", "With this card you can challenge any other player (staring him in the eyes!), regardless of the distance. The challenged player may discard a BANG! card (even though it is not his turn!). If he does, you may discard a BANG! card, and so on: the first player failing to discard a BANG! card loses one life point, and the duel is I over. You cannot play Missed! or use the Barrel during a duel. The Duel is not a BANG! card. BANG! cards discarded during a Duel are not accounted towards the one BANG! card limitation.", "Duel", 3, 2, 10 },
                    { "5e327fb8-aa07-4be9-8633-850bbeadc30f", "Draw two cards from the top of the deck", "Stagecoach", 0, 2, 7 },
                    { "026e75ad-fd88-49a9-b88a-09f8e8908ade", "Draw two cards from the top of the deck", "Stagecoach", 0, 2, 7 },
                    { "a3ba93be-56b7-4038-8316-0e7bbf6e0bcd", "When you play this card, turn as many cards from the deck face up as the players still playing. Starting with you and proceeding clockwise, each player chooses one of those cards and puts it in his hands.", "General Store", 0, 2, 10 },
                    { "bab84577-cb53-41cb-a06d-fffadd4e1072", "Force any one player to discard a card, regardless of the distance.", "Cat Balou", 3, 2, 9 },
                    { "6e6d14e6-e3b9-4d8d-8c73-890e8610532b", "Force any one player to discard a card, regardless of the distance.", "Cat Balou", 3, 2, 7 },
                    { "58847b2a-eaef-4dac-8012-4816fbe8ba07", "Force any one player to discard a card, regardless of the distance.", "Cat Balou", 1, 2, 11 },
                    { "bff2c1d5-f636-4a69-bebf-d9929bbe9ba8", "This card lets you regain one life point - take a bullet from the pile. You cannot gain more life points than your starting amount! The Beer cannot be used to help other players. The Beer can be played in two ways: as usual, during your turn and out of turn, but only if you have just received a hit that is lethal (i.e. a hit that takes away your last life point), and not if you are simply hit. Beer has no effect if there are only 2 players left in the game; in other words, if you play a Beer you do not gain any life point.", "Beer", 1, 2, 8 },
                    { "e756be70-bdf1-428a-96f9-19d93c420c85", "This card lets you regain one life point - take a bullet from the pile. You cannot gain more life points than your starting amount! The Beer cannot be used to help other players. The Beer can be played in two ways: as usual, during your turn and out of turn, but only if you have just received a hit that is lethal (i.e. a hit that takes away your last life point), and not if you are simply hit. Beer has no effect if there are only 2 players left in the game; in other words, if you play a Beer you do not gain any life point.", "Beer", 1, 2, 7 },
                    { "50e71c77-4bd0-41eb-b85f-2b7fcadc2350", "This card lets you regain one life point - take a bullet from the pile. You cannot gain more life points than your starting amount! The Beer cannot be used to help other players. The Beer can be played in two ways: as usual, during your turn and out of turn, but only if you have just received a hit that is lethal (i.e. a hit that takes away your last life point), and not if you are simply hit. Beer has no effect if there are only 2 players left in the game; in other words, if you play a Beer you do not gain any life point.", "Beer", 1, 2, 6 },
                    { "d24fbcea-cdb5-448a-88db-6a40a87e5bb5", "This card lets you regain one life point - take a bullet from the pile. You cannot gain more life points than your starting amount! The Beer cannot be used to help other players. The Beer can be played in two ways: as usual, during your turn and out of turn, but only if you have just received a hit that is lethal (i.e. a hit that takes away your last life point), and not if you are simply hit. Beer has no effect if there are only 2 players left in the game; in other words, if you play a Beer you do not gain any life point.", "Beer", 1, 2, 5 },
                    { "9e1ffb10-40c1-4acb-9dc0-341b5d444a49", "This card lets you regain one life point - take a bullet from the pile. You cannot gain more life points than your starting amount! The Beer cannot be used to help other players. The Beer can be played in two ways: as usual, during your turn and out of turn, but only if you have just received a hit that is lethal (i.e. a hit that takes away your last life point), and not if you are simply hit. Beer has no effect if there are only 2 players left in the game; in other words, if you play a Beer you do not gain any life point.", "Beer", 1, 2, 4 },
                    { "992ba3ca-effb-44df-8ab5-dfed468a4287", "Force any one player to discard a card, regardless of the distance.", "Cat Balou", 3, 2, 8 },
                    { "f827c908-fa5f-4600-b5cb-d1706b401531", "Each player, excluding the one who played this card, may discard a BANG! card, or lose one life point. Neither Missed! nor Barrel have effect in this case.", "Indians!", 3, 2, 11 },
                    { "dff12a31-ed7f-4676-9e59-68e261914c4b", "Each player, excluding the one who played this card, may discard a BANG! card, or lose one life point. Neither Missed! nor Barrel have effect in this case.", "Indians!", 3, 2, 12 },
                    { "0ec5f375-5ca6-438f-bf98-c9cbec5c34d8", "If you are hit by a BANG! you may immediately play a Missed! - even though it is not your turn! - to cancel the shot. If you do not, you lose one life point (discard a bullet). Discarded bullet go into a pile in the middle of the table.", "Missed!", 2, 2, 8 },
                    { "d7a1f350-1c85-4c32-9e67-f41f102a95d1", "Draw three cards from the top of the deck", "Wells Fargo", 1, 2, 1 },
                    { "05e1b8d4-693e-4554-a49d-c1c78c2f7a78", "Cards with symbols on two lines have two simultaneous effects, one for each line. Here symbols say: Regain one life point, and this applies to All the other players, and on the next line: [You] regain one life point. The overall effect is that all players in play regain one life point. You cannot play a Saloon out of turn when you are losing your last life point: the Saloon is not a Beer!", "Saloon", 1, 2, 3 },
                    { "bf073e80-6d78-4f60-b823-1048614aac0e", "The symbols state: Draw a card from a player at distance 1. Remember that this distance is not modified by weapons, but only by cards such as Mustang and/or Scope.", "Panic!", 3, 2, 6 },
                    { "1af00051-77d6-4da2-ac63-9866db8ebb58", "The symbols state: Draw a card from a player at distance 1. Remember that this distance is not modified by weapons, but only by cards such as Mustang and/or Scope.", "Panic!", 1, 2, 12 },
                    { "cddd8dcd-3db8-4e4a-85d1-ea58f994f319", "The symbols state: Draw a card from a player at distance 1. Remember that this distance is not modified by weapons, but only by cards such as Mustang and/or Scope.", "Panic!", 1, 2, 10 },
                    { "3da946f5-86b6-44c5-ba5f-c6e39dc55398", "The symbols state: Draw a card from a player at distance 1. Remember that this distance is not modified by weapons, but only by cards such as Mustang and/or Scope.", "Panic!", 1, 2, 9 },
                    { "1eae789f-c025-439a-aa87-822fbf46d76e", "If you are hit by a BANG! you may immediately play a Missed! - even though it is not your turn! - to cancel the shot. If you do not, you lose one life point (discard a bullet). Discarded bullet go into a pile in the middle of the table.", "Missed!", 0, 2, 6 },
                    { "e0170649-a7ed-4af2-9319-ea67656dab80", "If you are hit by a BANG! you may immediately play a Missed! - even though it is not your turn! - to cancel the shot. If you do not, you lose one life point (discard a bullet). Discarded bullet go into a pile in the middle of the table.", "Missed!", 0, 2, 5 },
                    { "011d9800-8195-423c-aa3a-527f3a9e52bd", "If you are hit by a BANG! you may immediately play a Missed! - even though it is not your turn! - to cancel the shot. If you do not, you lose one life point (discard a bullet). Discarded bullet go into a pile in the middle of the table.", "Missed!", 0, 2, 4 },
                    { "d4a7a104-c9af-4684-8f45-485ccf99c295", "If you are hit by a BANG! you may immediately play a Missed! - even though it is not your turn! - to cancel the shot. If you do not, you lose one life point (discard a bullet). Discarded bullet go into a pile in the middle of the table.", "Missed!", 0, 2, 3 },
                    { "c9526278-2600-4dfb-9e67-92b45aa64f0c", "If you are hit by a BANG! you may immediately play a Missed! - even though it is not your turn! - to cancel the shot. If you do not, you lose one life point (discard a bullet). Discarded bullet go into a pile in the middle of the table.", "Missed!", 0, 2, 2 },
                    { "197776eb-869c-4e94-940c-55b2f3310622", "If you are hit by a BANG! you may immediately play a Missed! - even though it is not your turn! - to cancel the shot. If you do not, you lose one life point (discard a bullet). Discarded bullet go into a pile in the middle of the table.", "Missed!", 0, 2, 1 },
                    { "65ff1ce7-b605-473d-8984-5ef3535bd268", "If you are hit by a BANG! you may immediately play a Missed! - even though it is not your turn! - to cancel the shot. If you do not, you lose one life point (discard a bullet). Discarded bullet go into a pile in the middle of the table.", "Missed!", 0, 2, 0 },
                    { "1dd17c8a-e4a6-410b-a66b-16304b03aa70", "If you are hit by a BANG! you may immediately play a Missed! - even though it is not your turn! - to cancel the shot. If you do not, you lose one life point (discard a bullet). Discarded bullet go into a pile in the middle of the table.", "Missed!", 2, 2, 12 },
                    { "9cfd78f4-c64d-4938-9c1f-9fc3814e85cc", "If you are hit by a BANG! you may immediately play a Missed! - even though it is not your turn! - to cancel the shot. If you do not, you lose one life point (discard a bullet). Discarded bullet go into a pile in the middle of the table.", "Missed!", 2, 2, 11 },
                    { "1847e015-bb3f-4487-9ed8-ffd60a4febb3", "If you are hit by a BANG! you may immediately play a Missed! - even though it is not your turn! - to cancel the shot. If you do not, you lose one life point (discard a bullet). Discarded bullet go into a pile in the middle of the table.", "Missed!", 2, 2, 10 },
                    { "a14a1011-a4bf-44db-a355-cb909dbe8d3b", "If you are hit by a BANG! you may immediately play a Missed! - even though it is not your turn! - to cancel the shot. If you do not, you lose one life point (discard a bullet). Discarded bullet go into a pile in the middle of the table.", "Missed!", 2, 2, 9 },
                    { "cf8f49d6-013b-4341-a04d-2552fe153286", "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", "Bang", 1, 2, 12 },
                    { "fa21eebb-a585-4a7b-bee1-9679f56d1d29", "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", "Bang", 1, 2, 11 },
                    { "c7ffb34d-e459-4a7d-bcd1-891fd2f6bc00", "This card lets you regain one life point - take a bullet from the pile. You cannot gain more life points than your starting amount! The Beer cannot be used to help other players. The Beer can be played in two ways: as usual, during your turn and out of turn, but only if you have just received a hit that is lethal (i.e. a hit that takes away your last life point), and not if you are simply hit. Beer has no effect if there are only 2 players left in the game; in other words, if you play a Beer you do not gain any life point.", "Beer", 1, 2, 9 },
                    { "4fb4bf76-fd2f-45c1-9ef9-e0cc747a8cdf", "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", "Bang", 2, 2, 7 }
                });

            migrationBuilder.InsertData(
                table: "Cards",
                columns: new[] { "Id", "Description", "Name", "Suite", "Type", "Value" },
                values: new object[,]
                {
                    { "0f084b5c-bb57-41d9-81cb-b4e0b2abf894", "When you have a Scope in play, you see all the other players at a distance decreased by 1.", "Scope", 0, 0, 12 },
                    { "74bcee38-37e2-4a77-81c8-d402dbefbfa3", "", "Winchester", 2, 1, 8 },
                    { "91249d15-8ecf-4637-b235-9bdbcfd5268f", "", "Volcanic", 0, 1, 8 },
                    { "55fca9d2-1bd4-4d54-a053-2f8bb0fd5dc0", "", "Volcanic", 2, 1, 8 },
                    { "86d6c584-adb8-4614-a9ba-d04ff79ef2ff", "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", "Bang", 1, 2, 10 },
                    { "9ce4fcc7-9102-45e3-b38a-904af7e0d398", "", "Schofield", 2, 1, 10 },
                    { "41e3816f-2534-4c6e-9df6-0323af5b00c9", "", "Schofield", 2, 1, 9 },
                    { "f33fc607-5027-4fe7-a8b9-01b6bb28d40c", "", "Carabine", 2, 1, 12 },
                    { "a73eff5b-7e01-46c9-b48f-325af62cfb00", "", "Remington", 2, 1, 11 },
                    { "28f69fee-54aa-48cc-bee9-adcae4ec622f", "When you have a Mustang horse in play the distance between other players and you is increased by 1. However, you still see the other players at the normal distance.", "Mustang", 1, 0, 6 },
                    { "8c4ff17e-5b44-42bc-90a1-150d5c484ed8", "When you have a Mustang horse in play the distance between other players and you is increased by 1. However, you still see the other players at the normal distance.", "Mustang", 1, 0, 6 },
                    { "73625043-67da-4f6e-9b64-99e947e326a0", "Play this card in front of any player regardless of the distance: you put him in jail! If you are in jail, you must draw! before the beginning of your turn: if you draw a Heart card, you escape from jail: discard the Jail, and continue your turn as normal otherwise discard the Jail and skip your turn. If you are in Jail you remain a possible target for BANG! cards and can still play response cards (e.g. Missed! and Beer) out of your turn, if necessary. Jail cannot be played on the Sheriff.", "Jail", 0, 0, 8 },
                    { "92976b77-5341-4c76-9c00-b76b6d12b4eb", "Play this card in front of any player regardless of the distance: you put him in jail! If you are in jail, you must draw! before the beginning of your turn: if you draw a Heart card, you escape from jail: discard the Jail, and continue your turn as normal otherwise discard the Jail and skip your turn. If you are in Jail you remain a possible target for BANG! cards and can still play response cards (e.g. Missed! and Beer) out of your turn, if necessary. Jail cannot be played on the Sheriff.", "Jail", 1, 0, 2 },
                    { "ef8df827-0adc-4f71-a328-2cb7a3af7a83", "Play this card in front of any player regardless of the distance: you put him in jail! If you are in jail, you must draw! before the beginning of your turn: if you draw a Heart card, you escape from jail: discard the Jail, and continue your turn as normal otherwise discard the Jail and skip your turn. If you are in Jail you remain a possible target for BANG! cards and can still play response cards (e.g. Missed! and Beer) out of your turn, if necessary. Jail cannot be played on the Sheriff.", "Jail", 0, 0, 9 },
                    { "799a20d7-a7e4-4aa8-a4c7-4df975fe1bc3", "Play this card in front of you: the Dynamite will stay there for a whole turn. When you start your next turn(you have the Dynamite already in play), before the first phase you must draw! if you draw a card showing Spades and a number between 2 and 9, the Dynamite explodes! Discard it and lose 3 life points; otherwise, pass the Dynamite to the player on your left(who will draw! on his turn, etc)..Players keep passing the Dynamite around until it explodes, with the effect explained above, or it is drawn or discarded by a Panic!or a Cat Balou.If you have both the Dynamite and a Jail in play, check the Dynamite first. If you are damaged(or even eliminated!) by a Dynamite, this damage is not considered to be caused by any player.", "Dynamite", 1, 0, 0 },
                    { "617bdd17-e535-4551-9890-f33bff71bf39", "Play this card in front of you: the Dynamite will stay there for a whole turn. When you start your next turn(you have the Dynamite already in play), before the first phase you must draw! if you draw a card showing Spades and a number between 2 and 9, the Dynamite explodes! Discard it and lose 3 life points; otherwise, pass the Dynamite to the player on your left(who will draw! on his turn, etc)..Players keep passing the Dynamite around until it explodes, with the effect explained above, or it is drawn or discarded by a Panic!or a Cat Balou.If you have both the Dynamite and a Jail in play, check the Dynamite first. If you are damaged(or even eliminated!) by a Dynamite, this damage is not considered to be caused by any player.", "Dynamite", 1, 0, 0 },
                    { "4b36687f-9739-4d15-b812-6e30f5494ac7", "The Barrel allows you to “draw!” when you are the target of a BANG! if you draw a Heart card, you are Missed! (just like if you played a Missed! card)-otherwise nothing happens.", "Barrel", 0, 0, 11 },
                    { "995209c0-c237-49b1-99e8-dd57439da22a", "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", "Bang", 0, 2, 12 },
                    { "b9a3429a-7506-4b82-81ac-07fb8554b91a", "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", "Bang", 3, 2, 0 },
                    { "579a1b80-4efe-4944-89e4-7c1487ee5415", "", "Schofield", 2, 1, 11 },
                    { "4f8d0a17-b9b6-44f4-9e82-edc3316b0dd8", "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", "Bang", 3, 2, 2 },
                    { "984716df-c19a-4a2e-9e26-bbe5ab2760f9", "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", "Bang", 2, 2, 6 },
                    { "147d0858-6e91-4041-8ff3-d1d3e6fe29c1", "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", "Bang", 3, 2, 1 },
                    { "6c19481f-01ec-48fa-a915-c52bd0dca7a8", "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", "Bang", 2, 2, 4 },
                    { "f768f0b2-ab65-437d-8507-61d27b6fe71b", "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", "Bang", 2, 2, 3 },
                    { "dbf53655-deeb-400e-9133-7040b82b3e6e", "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", "Bang", 2, 2, 2 },
                    { "d5c96a10-870d-45ee-b7c5-0a0ce8753186", "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", "Bang", 2, 2, 1 },
                    { "8868979c-dec9-43d8-a6dc-82f0f500ad20", "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", "Bang", 2, 2, 0 },
                    { "7dd2e4be-60ef-4d56-809d-3a0b11e74191", "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", "Bang", 3, 2, 12 },
                    { "b0bb15f8-1ae2-4bb5-a124-9e08ff0103a1", "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", "Bang", 2, 2, 5 },
                    { "d7294a34-37d8-4224-b88c-afe86d4d68df", "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", "Bang", 3, 2, 10 },
                    { "1c933d6e-7550-4e4d-b45a-913d004c9943", "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", "Bang", 3, 2, 9 },
                    { "923d42a0-e35a-42a8-bc83-c00095877fe2", "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", "Bang", 3, 2, 8 },
                    { "38f2c70b-13d7-4d24-9586-bc19c76671ad", "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", "Bang", 3, 2, 7 },
                    { "8a4848c6-3e4e-4321-b5c8-f389db3ff43d", "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", "Bang", 3, 2, 6 },
                    { "9276d633-8e68-4c1f-9fc5-e8fd18280d6c", "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", "Bang", 3, 2, 5 },
                    { "e68b70f7-a614-40c1-ab31-60a60835dda6", "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", "Bang", 3, 2, 4 },
                    { "6b7aed1b-89dd-4369-914d-c31185c11fd3", "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", "Bang", 3, 2, 3 },
                    { "a3e9c7ad-fd04-4453-9bc1-ad5f4d7a9db5", "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", "Bang", 3, 2, 11 }
                });

            migrationBuilder.InsertData(
                table: "RoleCards",
                columns: new[] { "Id", "Description", "LifePoint", "Name" },
                values: new object[,]
                {
                    { "2602ae98-3eb2-43bb-9cec-fe9ac0b341c6", "As soon as she has no cards in her hand, she draws a card from the draw pile.", 4, "Suzy Lafayette" },
                    { "6b37a20b-b5d2-42d0-9cd2-ac924e6a658e", "Players trying to cancel his BANG! cards need to play 2 Missed!. The Barrel effect, if successfully used, only counts as one Missed!.", 4, "Slab the Killer" },
                    { "83d60c82-770d-4f9c-82d1-079649799b7f", "At any time, he may discard 2 cards from his hand to regain one life point. If he is willing and able, he can use this ability more than once at a time. But remember: you cannot have more life points than your starting amount!", 4, "Sid Ketchum" }
                });

            migrationBuilder.InsertData(
                table: "RoleCards",
                columns: new[] { "Id", "Description", "LifePoint", "Name" },
                values: new object[,]
                {
                    { "377e6d31-a5d5-4d80-85e1-4b6926c84d29", "She is considered to have a Scope in play at all times; she sees the other players at a distance decreased by 1. If she has another real Scope in play, she can count both of them, reducing her distance to all other players by a total of 2.", 4, "Rose Doolan" },
                    { "7deed5cd-8bde-4d79-a5d1-678d79984728", "During phase 1 of his turn, he may choose to draw the first card from the top of the discard pile or from the deck. Then, he draws the second card from the deck.", 4, "Pedro Ramirez" },
                    { "6d5a30a4-ddb8-48eb-b7ff-d93f9c3b8e08", "he is considered to have a Mustang in play at all times; all other players must add 1 to the distance to him. If he has another real Mustang in play, he can count both of them, increasing all distances to him by a total of 2.", 3, "Paul Regret" },
                    { "1af0014f-aa21-406d-a00f-9a51bb13356f", "Each time he is required to draw!, he flips the top two cards from the deck and chooses the result he prefers. Discard both cards afterward.", 4, "Lucky Duke" },
                    { "8e917c46-5920-4208-ac32-5f2b8e23441a", "Each time he loses a life point due to a card played by another player, he draws a random card from the hands of that player (one card for each life point). If that player has no more cards, too bad!, he does not draw. Note that Dynamite damages are not caused by any player.", 3, "El Gringo" },
                    { "ace22034-0064-454b-a7da-01c3f21c9714", "He is considered to have a Barrel in play at all times; he can draw! when he is the target of a BANG!, and on a Heart he is missed. If he has another real Barrel card in play, he can count both of them, giving him two chances to cancel the BANG! before playing a Missed!.", 4, "Jourdonnais" },
                    { "6d4b3bc9-0689-41d8-b9ae-ad3c0bda0a8c", "During phase 1 of his turn, he may choose to draw the first card from the deck, or randomly from the hand of any other player. Then he draws the second card from the deck.", 4, "Jesse Jones" },
                    { "9cb1846b-1499-4d64-8bc5-2e0eb857400f", "She can use BANG! cards as Missed! cards and vice versa. If she plays a Missed! as a BANG!, she cannot play another BANG! card that turn (unless she has a Volcanic in play).", 4, "Calamity Janet" },
                    { "86536e3f-4b15-4a6e-8a02-2727484b9990", "He shows the second card he draw. On Heart or Diamonds, he draws one more card", 4, "Black Jack" },
                    { "8c52aa39-7ee4-4b1a-b63c-dcb561198ecf", "Each time he is hit, he draws a card", 4, "Bart Cassidy" },
                    { "e8999773-6289-44fb-b00f-4d8162eada6d", "Whenever a character is eliminated from the game, Sam takes all the cards that player had in his hand and in play, and adds them to his hand.", 4, "Vulture Sam" },
                    { "2439e6b3-9254-45fd-ac05-f303ec90a5b4", "During phase 1 of his turn, he looks at the top three cards of the deck: he chooses 2 to draw, and puts the other one back on the top of the deck, face down.", 4, "Kit Carlson" },
                    { "33e32467-694c-4b2e-af75-0f36ad9ae2c8", "He can play any number of BANG! cards during his turn.", 4, "Willy the Kid" }
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
                name: "IX_Characters_RoleId",
                table: "Characters",
                column: "RoleId");

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
                name: "IX_GameCards_GameId",
                table: "GameCards",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_GameId",
                table: "Messages",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_LobbyName",
                table: "Messages",
                column: "LobbyName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                name: "Characters");

            migrationBuilder.DropTable(
                name: "Connections");

            migrationBuilder.DropTable(
                name: "GameCards");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "RoleCards");

            migrationBuilder.DropTable(
                name: "Cards");

            migrationBuilder.DropTable(
                name: "Lobbies");

            migrationBuilder.DropTable(
                name: "Games");
        }
    }
}
