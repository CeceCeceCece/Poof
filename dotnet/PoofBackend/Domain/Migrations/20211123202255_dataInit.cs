using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class dataInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Characters_Cards_WeaponId",
                table: "Characters");

            migrationBuilder.DropForeignKey(
                name: "FK_Characters_RoleCards_RoleId",
                table: "Characters");

            migrationBuilder.DropTable(
                name: "RoleCards");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "011d9800-8195-423c-aa3a-527f3a9e52bd");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "026e75ad-fd88-49a9-b88a-09f8e8908ade");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "05e1b8d4-693e-4554-a49d-c1c78c2f7a78");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "0ec5f375-5ca6-438f-bf98-c9cbec5c34d8");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "0f084b5c-bb57-41d9-81cb-b4e0b2abf894");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "147d0858-6e91-4041-8ff3-d1d3e6fe29c1");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "1847e015-bb3f-4487-9ed8-ffd60a4febb3");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "197776eb-869c-4e94-940c-55b2f3310622");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "1af00051-77d6-4da2-ac63-9866db8ebb58");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "1c933d6e-7550-4e4d-b45a-913d004c9943");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "1dd17c8a-e4a6-410b-a66b-16304b03aa70");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "1e5ec8e7-c1d7-4deb-b4f5-31e4fd717a70");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "1eae789f-c025-439a-aa87-822fbf46d76e");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "28f69fee-54aa-48cc-bee9-adcae4ec622f");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "38f2c70b-13d7-4d24-9586-bc19c76671ad");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "3da946f5-86b6-44c5-ba5f-c6e39dc55398");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "41e3816f-2534-4c6e-9df6-0323af5b00c9");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "4b36687f-9739-4d15-b812-6e30f5494ac7");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "4f8d0a17-b9b6-44f4-9e82-edc3316b0dd8");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "4fb4bf76-fd2f-45c1-9ef9-e0cc747a8cdf");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "50e71c77-4bd0-41eb-b85f-2b7fcadc2350");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "55fca9d2-1bd4-4d54-a053-2f8bb0fd5dc0");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "579a1b80-4efe-4944-89e4-7c1487ee5415");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "58847b2a-eaef-4dac-8012-4816fbe8ba07");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "5e327fb8-aa07-4be9-8633-850bbeadc30f");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "617bdd17-e535-4551-9890-f33bff71bf39");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "65ff1ce7-b605-473d-8984-5ef3535bd268");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "6b7aed1b-89dd-4369-914d-c31185c11fd3");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "6b8e207a-dd84-41c4-a940-66efb46e0d5e");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "6c19481f-01ec-48fa-a915-c52bd0dca7a8");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "6e6d14e6-e3b9-4d8d-8c73-890e8610532b");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "73625043-67da-4f6e-9b64-99e947e326a0");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "74bcee38-37e2-4a77-81c8-d402dbefbfa3");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "799a20d7-a7e4-4aa8-a4c7-4df975fe1bc3");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "7a51deef-4682-4d06-98bb-dc514811bf52");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "7dd2e4be-60ef-4d56-809d-3a0b11e74191");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "86d6c584-adb8-4614-a9ba-d04ff79ef2ff");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "8868979c-dec9-43d8-a6dc-82f0f500ad20");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "8a4848c6-3e4e-4321-b5c8-f389db3ff43d");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "8c4ff17e-5b44-42bc-90a1-150d5c484ed8");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "8d16c01d-2b55-4499-abb6-bd9c2c3e3502");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "91249d15-8ecf-4637-b235-9bdbcfd5268f");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "923d42a0-e35a-42a8-bc83-c00095877fe2");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "9276d633-8e68-4c1f-9fc5-e8fd18280d6c");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "92976b77-5341-4c76-9c00-b76b6d12b4eb");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "984716df-c19a-4a2e-9e26-bbe5ab2760f9");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "985e1e59-b3f9-4164-b100-ca8012686995");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "992ba3ca-effb-44df-8ab5-dfed468a4287");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "995209c0-c237-49b1-99e8-dd57439da22a");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "9ce4fcc7-9102-45e3-b38a-904af7e0d398");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "9cfd78f4-c64d-4938-9c1f-9fc3814e85cc");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "9e1ffb10-40c1-4acb-9dc0-341b5d444a49");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "a14a1011-a4bf-44db-a355-cb909dbe8d3b");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "a3ba93be-56b7-4038-8316-0e7bbf6e0bcd");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "a3e9c7ad-fd04-4453-9bc1-ad5f4d7a9db5");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "a73eff5b-7e01-46c9-b48f-325af62cfb00");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "b0bb15f8-1ae2-4bb5-a124-9e08ff0103a1");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "b6c8cf93-f0a3-4894-9716-37dd5de16d38");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "b9a3429a-7506-4b82-81ac-07fb8554b91a");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "bab84577-cb53-41cb-a06d-fffadd4e1072");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "bf073e80-6d78-4f60-b823-1048614aac0e");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "bff2c1d5-f636-4a69-bebf-d9929bbe9ba8");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "c7ffb34d-e459-4a7d-bcd1-891fd2f6bc00");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "c9526278-2600-4dfb-9e67-92b45aa64f0c");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "cddd8dcd-3db8-4e4a-85d1-ea58f994f319");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "cf8f49d6-013b-4341-a04d-2552fe153286");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "d24fbcea-cdb5-448a-88db-6a40a87e5bb5");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "d4a7a104-c9af-4684-8f45-485ccf99c295");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "d5c96a10-870d-45ee-b7c5-0a0ce8753186");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "d7294a34-37d8-4224-b88c-afe86d4d68df");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "d7a1f350-1c85-4c32-9e67-f41f102a95d1");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "dbf53655-deeb-400e-9133-7040b82b3e6e");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "dff12a31-ed7f-4676-9e59-68e261914c4b");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "e0170649-a7ed-4af2-9319-ea67656dab80");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "e68b70f7-a614-40c1-ab31-60a60835dda6");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "e756be70-bdf1-428a-96f9-19d93c420c85");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "ef8df827-0adc-4f71-a328-2cb7a3af7a83");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "f33fc607-5027-4fe7-a8b9-01b6bb28d40c");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "f768f0b2-ab65-437d-8507-61d27b6fe71b");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "f827c908-fa5f-4600-b5cb-d1706b401531");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "fa21eebb-a585-4a7b-bee1-9679f56d1d29");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Games",
                newName: "NextUserId");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "Characters",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Characters",
                newName: "WeaponDistance");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                table: "Characters",
                newName: "PersonalCardId");

            migrationBuilder.RenameColumn(
                name: "NumberOfBangs",
                table: "Characters",
                newName: "Role");

            migrationBuilder.RenameColumn(
                name: "Distance",
                table: "Characters",
                newName: "MaxLifePoint");

            migrationBuilder.RenameIndex(
                name: "IX_Characters_RoleId",
                table: "Characters",
                newName: "IX_Characters_PersonalCardId");

            migrationBuilder.AddColumn<string>(
                name: "CurrentUserId",
                table: "Games",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Event",
                table: "Games",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "NextCardId",
                table: "Games",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Win",
                table: "Games",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CharacterId",
                table: "GameCards",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CharacterId1",
                table: "GameCards",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GameId1",
                table: "GameCards",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BangState",
                table: "Characters",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DistanceFromOthers",
                table: "Characters",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.InsertData(
                table: "Cards",
                columns: new[] { "Id", "Description", "Name", "Suite", "Type", "Value" },
                values: new object[,]
                {
                    { "8ab8d52c-c328-4eb4-aa76-116ff31e65d8", "The Barrel allows you to “draw!” when you are the target of a BANG! if you draw a Heart card, you are Missed! (just like if you played a Missed! card)-otherwise nothing happens.", "Barrel", 0, 0, 10 },
                    { "dc1152eb-32c7-4d52-b732-fd7be3b3761b", "When you play this card, turn as many cards from the deck face up as the players still playing. Starting with you and proceeding clockwise, each player chooses one of those cards and puts it in his hands.", "General Store", 2, 2, 7 },
                    { "c4e101ab-d5e8-4982-8e64-8e81158f937e", "The Gatling shoots a BANG! to all the other players, regardless of the distance. Even though the Gatling shoots a BANG! to all the other players, it is not considered a BANG! card. During your turn you can play any number of Gatling, but only one BANG! card.", "Gatling", 1, 2, 8 },
                    { "b05cdd60-f91b-4871-97a0-21853bb6d6bc", "With this card you can challenge any other player (staring him in the eyes!), regardless of the distance. The challenged player may discard a BANG! card (even though it is not his turn!). If he does, you may discard a BANG! card, and so on: the first player failing to discard a BANG! card loses one life point, and the duel is I over. You cannot play Missed! or use the Barrel during a duel. The Duel is not a BANG! card. BANG! cards discarded during a Duel are not accounted towards the one BANG! card limitation.", "Duel", 2, 2, 6 },
                    { "e9b4fff4-16ed-4a4b-8783-0a552facabc9", "With this card you can challenge any other player (staring him in the eyes!), regardless of the distance. The challenged player may discard a BANG! card (even though it is not his turn!). If he does, you may discard a BANG! card, and so on: the first player failing to discard a BANG! card loses one life point, and the duel is I over. You cannot play Missed! or use the Barrel during a duel. The Duel is not a BANG! card. BANG! cards discarded during a Duel are not accounted towards the one BANG! card limitation.", "Duel", 0, 2, 9 },
                    { "338ca7fc-898c-45bf-a890-6700a74b962a", "With this card you can challenge any other player (staring him in the eyes!), regardless of the distance. The challenged player may discard a BANG! card (even though it is not his turn!). If he does, you may discard a BANG! card, and so on: the first player failing to discard a BANG! card loses one life point, and the duel is I over. You cannot play Missed! or use the Barrel during a duel. The Duel is not a BANG! card. BANG! cards discarded during a Duel are not accounted towards the one BANG! card limitation.", "Duel", 3, 2, 10 },
                    { "0145dd1c-6d99-482f-b6c0-8004e8acf5c0", "Draw two cards from the top of the deck", "Stagecoach", 0, 2, 7 },
                    { "7503ad9c-a386-47e9-8c6f-312a699cc89d", "Draw two cards from the top of the deck", "Stagecoach", 0, 2, 7 },
                    { "4047564d-611d-4251-91a2-1c9f55f47f5e", "When you play this card, turn as many cards from the deck face up as the players still playing. Starting with you and proceeding clockwise, each player chooses one of those cards and puts it in his hands.", "General Store", 0, 2, 10 },
                    { "d0b745d4-8ad8-4553-a406-87b2fdd4044a", "Force any one player to discard a card, regardless of the distance.", "Cat Balou", 3, 2, 9 },
                    { "09ee83e6-eecf-4ef2-b428-fb98e84b2f29", "Force any one player to discard a card, regardless of the distance.", "Cat Balou", 3, 2, 7 },
                    { "b6afb876-1659-402e-9593-6bea08145570", "Force any one player to discard a card, regardless of the distance.", "Cat Balou", 1, 2, 11 },
                    { "94a516a0-3c6f-44b8-bde0-940c0bb488f5", "This card lets you regain one life point - take a bullet from the pile. You cannot gain more life points than your starting amount! The Beer cannot be used to help other players. The Beer can be played in two ways: as usual, during your turn and out of turn, but only if you have just received a hit that is lethal (i.e. a hit that takes away your last life point), and not if you are simply hit. Beer has no effect if there are only 2 players left in the game; in other words, if you play a Beer you do not gain any life point.", "Beer", 1, 2, 9 },
                    { "e79d1a0e-c3bb-4a52-8759-119d1288f431", "This card lets you regain one life point - take a bullet from the pile. You cannot gain more life points than your starting amount! The Beer cannot be used to help other players. The Beer can be played in two ways: as usual, during your turn and out of turn, but only if you have just received a hit that is lethal (i.e. a hit that takes away your last life point), and not if you are simply hit. Beer has no effect if there are only 2 players left in the game; in other words, if you play a Beer you do not gain any life point.", "Beer", 1, 2, 8 },
                    { "d69d8294-fbba-43ed-9efa-36399304d520", "This card lets you regain one life point - take a bullet from the pile. You cannot gain more life points than your starting amount! The Beer cannot be used to help other players. The Beer can be played in two ways: as usual, during your turn and out of turn, but only if you have just received a hit that is lethal (i.e. a hit that takes away your last life point), and not if you are simply hit. Beer has no effect if there are only 2 players left in the game; in other words, if you play a Beer you do not gain any life point.", "Beer", 1, 2, 6 },
                    { "21b234d4-8945-44a1-a445-a3dd9596c2ce", "This card lets you regain one life point - take a bullet from the pile. You cannot gain more life points than your starting amount! The Beer cannot be used to help other players. The Beer can be played in two ways: as usual, during your turn and out of turn, but only if you have just received a hit that is lethal (i.e. a hit that takes away your last life point), and not if you are simply hit. Beer has no effect if there are only 2 players left in the game; in other words, if you play a Beer you do not gain any life point.", "Beer", 1, 2, 5 },
                    { "3197c353-2f60-4a18-bcb4-c078044974e5", "This card lets you regain one life point - take a bullet from the pile. You cannot gain more life points than your starting amount! The Beer cannot be used to help other players. The Beer can be played in two ways: as usual, during your turn and out of turn, but only if you have just received a hit that is lethal (i.e. a hit that takes away your last life point), and not if you are simply hit. Beer has no effect if there are only 2 players left in the game; in other words, if you play a Beer you do not gain any life point.", "Beer", 1, 2, 4 },
                    { "ba93c9d7-f6b1-487d-a3b5-7889911741bf", "Force any one player to discard a card, regardless of the distance.", "Cat Balou", 3, 2, 8 },
                    { "0b182fb2-5e42-4907-bae9-1b24a1705dc0", "Each player, excluding the one who played this card, may discard a BANG! card, or lose one life point. Neither Missed! nor Barrel have effect in this case.", "Indians!", 3, 2, 11 },
                    { "834acd5c-9b73-4ec6-9d10-2ee44103b681", "Each player, excluding the one who played this card, may discard a BANG! card, or lose one life point. Neither Missed! nor Barrel have effect in this case.", "Indians!", 3, 2, 12 },
                    { "561879e9-9191-496a-bd33-23bb64e92349", "If you are hit by a BANG! you may immediately play a Missed! - even though it is not your turn! - to cancel the shot. If you do not, you lose one life point (discard a bullet). Discarded bullet go into a pile in the middle of the table.", "Missed!", 2, 2, 8 },
                    { "33b709c2-b1d0-49f7-959d-fd57b0e4de79", "Draw three cards from the top of the deck", "Wells Fargo", 1, 2, 1 },
                    { "38cc612a-2a04-42b6-95e1-fcefbd9deead", "Cards with symbols on two lines have two simultaneous effects, one for each line. Here symbols say: Regain one life point, and this applies to All the other players, and on the next line: [You] regain one life point. The overall effect is that all players in play regain one life point. You cannot play a Saloon out of turn when you are losing your last life point: the Saloon is not a Beer!", "Saloon", 1, 2, 3 },
                    { "d1948ac3-4565-4dc7-9f3e-4edac9b71d0a", "The symbols state: Draw a card from a player at distance 1. Remember that this distance is not modified by weapons, but only by cards such as Mustang and/or Scope.", "Panic!", 3, 2, 6 },
                    { "412398af-6c60-4e6e-b644-ca70661de169", "The symbols state: Draw a card from a player at distance 1. Remember that this distance is not modified by weapons, but only by cards such as Mustang and/or Scope.", "Panic!", 1, 2, 12 },
                    { "748dd8cf-eb4d-4534-a949-3a9e044cbedf", "The symbols state: Draw a card from a player at distance 1. Remember that this distance is not modified by weapons, but only by cards such as Mustang and/or Scope.", "Panic!", 1, 2, 10 },
                    { "306feb61-1c82-4df5-946a-482db3c647e5", "The symbols state: Draw a card from a player at distance 1. Remember that this distance is not modified by weapons, but only by cards such as Mustang and/or Scope.", "Panic!", 1, 2, 9 },
                    { "79067734-cec1-42ca-9b5e-d135e33124f7", "If you are hit by a BANG! you may immediately play a Missed! - even though it is not your turn! - to cancel the shot. If you do not, you lose one life point (discard a bullet). Discarded bullet go into a pile in the middle of the table.", "Missed!", 0, 2, 6 },
                    { "9933ac2d-70c6-40c6-995a-51b43ce051b8", "If you are hit by a BANG! you may immediately play a Missed! - even though it is not your turn! - to cancel the shot. If you do not, you lose one life point (discard a bullet). Discarded bullet go into a pile in the middle of the table.", "Missed!", 0, 2, 5 },
                    { "79824782-9ce7-466c-b3df-b7f9b63acf2f", "If you are hit by a BANG! you may immediately play a Missed! - even though it is not your turn! - to cancel the shot. If you do not, you lose one life point (discard a bullet). Discarded bullet go into a pile in the middle of the table.", "Missed!", 0, 2, 4 },
                    { "d5c74f8b-38a3-4982-ad83-612fcba235e3", "If you are hit by a BANG! you may immediately play a Missed! - even though it is not your turn! - to cancel the shot. If you do not, you lose one life point (discard a bullet). Discarded bullet go into a pile in the middle of the table.", "Missed!", 0, 2, 3 },
                    { "42acd740-a179-4abd-9ada-2bb8c3f41a69", "If you are hit by a BANG! you may immediately play a Missed! - even though it is not your turn! - to cancel the shot. If you do not, you lose one life point (discard a bullet). Discarded bullet go into a pile in the middle of the table.", "Missed!", 0, 2, 2 },
                    { "dbe74a8c-bc24-4212-999f-9a21bde2081f", "If you are hit by a BANG! you may immediately play a Missed! - even though it is not your turn! - to cancel the shot. If you do not, you lose one life point (discard a bullet). Discarded bullet go into a pile in the middle of the table.", "Missed!", 0, 2, 1 },
                    { "e11e5d60-83f5-4980-8a4c-e940269d6469", "If you are hit by a BANG! you may immediately play a Missed! - even though it is not your turn! - to cancel the shot. If you do not, you lose one life point (discard a bullet). Discarded bullet go into a pile in the middle of the table.", "Missed!", 0, 2, 0 },
                    { "468fe60c-2cb5-4796-b269-d0d1f1a51243", "If you are hit by a BANG! you may immediately play a Missed! - even though it is not your turn! - to cancel the shot. If you do not, you lose one life point (discard a bullet). Discarded bullet go into a pile in the middle of the table.", "Missed!", 2, 2, 12 },
                    { "f4d17cac-2070-45eb-9623-6d9cda224033", "If you are hit by a BANG! you may immediately play a Missed! - even though it is not your turn! - to cancel the shot. If you do not, you lose one life point (discard a bullet). Discarded bullet go into a pile in the middle of the table.", "Missed!", 2, 2, 11 },
                    { "a9bff813-642b-4eee-aa82-be9cb87f074f", "If you are hit by a BANG! you may immediately play a Missed! - even though it is not your turn! - to cancel the shot. If you do not, you lose one life point (discard a bullet). Discarded bullet go into a pile in the middle of the table.", "Missed!", 2, 2, 10 },
                    { "fb4a9642-7698-4fc1-80a2-4727e97094b8", "If you are hit by a BANG! you may immediately play a Missed! - even though it is not your turn! - to cancel the shot. If you do not, you lose one life point (discard a bullet). Discarded bullet go into a pile in the middle of the table.", "Missed!", 2, 2, 9 },
                    { "d0699642-de35-4dfe-8eb7-e1f3bafcd191", "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", "Bang!", 1, 2, 12 },
                    { "83ede1f4-3fa6-46cc-9b38-52e87b547aa2", "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", "Bang!", 1, 2, 11 },
                    { "48f24887-2667-4450-b4da-3fbd17f2a81c", "This card lets you regain one life point - take a bullet from the pile. You cannot gain more life points than your starting amount! The Beer cannot be used to help other players. The Beer can be played in two ways: as usual, during your turn and out of turn, but only if you have just received a hit that is lethal (i.e. a hit that takes away your last life point), and not if you are simply hit. Beer has no effect if there are only 2 players left in the game; in other words, if you play a Beer you do not gain any life point.", "Beer", 1, 2, 7 },
                    { "2a3f955e-7bf9-4373-8c2b-ce2d17e03687", "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", "Bang!", 2, 2, 7 }
                });

            migrationBuilder.InsertData(
                table: "Cards",
                columns: new[] { "Id", "Description", "Name", "Suite", "Type", "Value" },
                values: new object[,]
                {
                    { "5549e5e7-03f0-424b-a96f-77d4e4d147a9", "When you have a Scope in play, you see all the other players at a distance decreased by 1.", "Scope", 0, 0, 12 },
                    { "5504e073-c632-4d0f-89de-e7b2f65e8aa7", "", "Winchester", 2, 1, 8 },
                    { "432b72bd-8ed8-4fb7-95d7-fcd7eed92867", "", "Volcanic", 0, 1, 8 },
                    { "b96d9513-abe3-42cc-8b7e-bf02ffad7d90", "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", "Bang!", 1, 2, 10 },
                    { "9113c8da-845d-4a4a-a58d-d7f32bbf62c4", "", "Schofield", 2, 1, 11 },
                    { "59100acb-a7e4-474d-9c41-f2a39335f11a", "", "Schofield", 2, 1, 10 },
                    { "8e17da52-5718-4b77-bd9f-fba6eb0e808b", "", "Schofield", 2, 1, 9 },
                    { "9a83f13a-c34f-4b9b-a16d-5ac76a321e46", "", "Carabine", 2, 1, 12 },
                    { "1dc762eb-bc2f-41e9-a153-cbcbf416d203", "", "Remington", 2, 1, 11 },
                    { "6cb057d9-9424-4999-a787-cfc698b595b2", "When you have a Mustang horse in play the distance between other players and you is increased by 1. However, you still see the other players at the normal distance.", "Mustang", 1, 0, 6 },
                    { "bbad3b48-6391-4676-af6a-58f0915c79a8", "When you have a Mustang horse in play the distance between other players and you is increased by 1. However, you still see the other players at the normal distance.", "Mustang", 1, 0, 6 },
                    { "4feefb20-55e0-4d88-953b-b544926c1dab", "Play this card in front of any player regardless of the distance: you put him in jail! If you are in jail, you must draw! before the beginning of your turn: if you draw a Heart card, you escape from jail: discard the Jail, and continue your turn as normal otherwise discard the Jail and skip your turn. If you are in Jail you remain a possible target for BANG! cards and can still play response cards (e.g. Missed! and Beer) out of your turn, if necessary. Jail cannot be played on the Sheriff.", "Jail", 0, 0, 8 },
                    { "734ec77d-a104-4f43-9f60-443ea924a24d", "Play this card in front of any player regardless of the distance: you put him in jail! If you are in jail, you must draw! before the beginning of your turn: if you draw a Heart card, you escape from jail: discard the Jail, and continue your turn as normal otherwise discard the Jail and skip your turn. If you are in Jail you remain a possible target for BANG! cards and can still play response cards (e.g. Missed! and Beer) out of your turn, if necessary. Jail cannot be played on the Sheriff.", "Jail", 1, 0, 2 },
                    { "e0837842-4379-43f6-bd23-a5cdf6bf57bc", "Play this card in front of any player regardless of the distance: you put him in jail! If you are in jail, you must draw! before the beginning of your turn: if you draw a Heart card, you escape from jail: discard the Jail, and continue your turn as normal otherwise discard the Jail and skip your turn. If you are in Jail you remain a possible target for BANG! cards and can still play response cards (e.g. Missed! and Beer) out of your turn, if necessary. Jail cannot be played on the Sheriff.", "Jail", 0, 0, 9 },
                    { "97a54798-dbb0-417c-971c-4eaf6b3e3264", "Play this card in front of you: the Dynamite will stay there for a whole turn. When you start your next turn(you have the Dynamite already in play), before the first phase you must draw! if you draw a card showing Spades and a number between 2 and 9, the Dynamite explodes! Discard it and lose 3 life points; otherwise, pass the Dynamite to the player on your left(who will draw! on his turn, etc)..Players keep passing the Dynamite around until it explodes, with the effect explained above, or it is drawn or discarded by a Panic!or a Cat Balou.If you have both the Dynamite and a Jail in play, check the Dynamite first. If you are damaged(or even eliminated!) by a Dynamite, this damage is not considered to be caused by any player.", "Dynamite", 1, 0, 0 },
                    { "00a9e537-b114-4ad9-b099-991d540ab994", "Play this card in front of you: the Dynamite will stay there for a whole turn. When you start your next turn(you have the Dynamite already in play), before the first phase you must draw! if you draw a card showing Spades and a number between 2 and 9, the Dynamite explodes! Discard it and lose 3 life points; otherwise, pass the Dynamite to the player on your left(who will draw! on his turn, etc)..Players keep passing the Dynamite around until it explodes, with the effect explained above, or it is drawn or discarded by a Panic!or a Cat Balou.If you have both the Dynamite and a Jail in play, check the Dynamite first. If you are damaged(or even eliminated!) by a Dynamite, this damage is not considered to be caused by any player.", "Dynamite", 1, 0, 0 },
                    { "3d91db27-d066-44be-a1a8-c5032d50d327", "The Barrel allows you to “draw!” when you are the target of a BANG! if you draw a Heart card, you are Missed! (just like if you played a Missed! card)-otherwise nothing happens.", "Barrel", 0, 0, 11 },
                    { "6049aeaa-40a6-43e6-9a85-7168b6026809", "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", "Bang!", 0, 2, 12 },
                    { "bbf50ea9-4da6-486d-9689-92ce324b1eac", "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", "Bang!", 3, 2, 0 },
                    { "0d8c6caf-f933-41ea-8203-c0aff547695d", "", "Volcanic", 2, 1, 8 },
                    { "3cd3f4d0-5460-4c70-aba4-d40e8a746351", "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", "Bang!", 3, 2, 2 },
                    { "92698c35-6ec9-43ee-b67e-656fb704160e", "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", "Bang!", 2, 2, 6 },
                    { "a94b7c2d-1ce5-41f7-8ca8-bf44c50be37d", "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", "Bang!", 2, 2, 5 },
                    { "8fae6e39-56ea-4b4f-b76d-b3c4d589b6da", "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", "Bang!", 2, 2, 4 },
                    { "d723cefe-5af1-406e-a366-b0f228b4657b", "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", "Bang!", 3, 2, 1 },
                    { "7c91f175-315c-4d81-bee5-98b6569bf2e4", "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", "Bang!", 2, 2, 2 },
                    { "666f0e7c-75a4-4c09-ae91-592c3bfc8533", "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", "Bang!", 2, 2, 1 },
                    { "fb804c15-9486-4006-a822-5be7411a40e1", "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", "Bang!", 2, 2, 0 },
                    { "1d8cfeff-97fb-4f2f-acf3-11089614f545", "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", "Bang!", 3, 2, 12 },
                    { "f02e2f65-fcc1-425a-9b8d-86cff121f63f", "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", "Bang!", 2, 2, 3 },
                    { "91d28053-a725-4fda-b36f-9f9d9989635c", "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", "Bang!", 3, 2, 10 },
                    { "01a8b64f-4495-4b47-b9be-b515f28a09c2", "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", "Bang!", 3, 2, 9 },
                    { "6a686a73-7464-4e13-b9dd-babe5da84cc2", "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", "Bang!", 3, 2, 8 },
                    { "489e1852-124b-485e-8174-840f88bfc2b6", "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", "Bang!", 3, 2, 7 },
                    { "3d7b9a8c-6909-4548-80c2-7fa0af29021e", "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", "Bang!", 3, 2, 6 },
                    { "3e911bd3-ad42-438c-bc1c-99ddae893b2a", "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", "Bang!", 3, 2, 5 },
                    { "e7a321cc-1793-4f6c-9283-d25ad84cdcba", "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", "Bang!", 3, 2, 4 },
                    { "419a5422-fc45-4c90-8174-b5ab6ad3e1d2", "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", "Bang!", 3, 2, 3 },
                    { "685b2e22-dd1d-4c5f-abe0-12489b5ab5bc", "BANG! cards are the main method to reduce other players' ife points. If you want to play a BANG! card to hit one of the players, determine: what the distance to that player is; and if your weapon is capable of reaching that distance.", "Bang!", 3, 2, 11 }
                });

            migrationBuilder.InsertData(
                table: "CharacterCards",
                columns: new[] { "Id", "Description", "LifePoint", "Name" },
                values: new object[,]
                {
                    { "6356a211-0c94-4fae-851b-06ab7f5aeecd", "At any time, he may discard 2 cards from his hand to regain one life point. If he is willing and able, he can use this ability more than once at a time. But remember: you cannot have more life points than your starting amount!", 4, "Sid Ketchum" },
                    { "173d4941-d975-4e74-9705-f2bc542a3c26", "She is considered to have a Scope in play at all times; she sees the other players at a distance decreased by 1. If she has another real Scope in play, she can count both of them, reducing her distance to all other players by a total of 2.", 4, "Rose Doolan" },
                    { "58841ecd-abfe-4cb4-ad95-c2291b57dd0e", "During phase 1 of his turn, he may choose to draw the first card from the top of the discard pile or from the deck. Then, he draws the second card from the deck.", 4, "Pedro Ramirez" }
                });

            migrationBuilder.InsertData(
                table: "CharacterCards",
                columns: new[] { "Id", "Description", "LifePoint", "Name" },
                values: new object[,]
                {
                    { "1d3ae93c-8a8f-40f1-ab2e-4bd66c13ae36", "he is considered to have a Mustang in play at all times; all other players must add 1 to the distance to him. If he has another real Mustang in play, he can count both of them, increasing all distances to him by a total of 2.", 3, "Paul Regret" },
                    { "e07ab2d0-d40c-490c-bf8e-745bfc7b7db5", "During phase 1 of his turn, he looks at the top three cards of the deck: he chooses 2 to draw, and puts the other one back on the top of the deck, face down.", 4, "Kit Carlson" },
                    { "137941ae-99d6-4e2f-834d-0b5924a09da9", "He shows the second card he draw. On Heart or Diamonds, he draws one more card", 4, "Black Jack" },
                    { "4b394d0d-86ce-4726-9605-0b34bce0c4fb", "During phase 1 of his turn, he may choose to draw the first card from the deck, or randomly from the hand of any other player. Then he draws the second card from the deck.", 4, "Jesse Jones" },
                    { "706167eb-ce1c-4f66-b1c7-764811a98c79", "Each time he loses a life point due to a card played by another player, he draws a random card from the hands of that player (one card for each life point). If that player has no more cards, too bad!, he does not draw. Note that Dynamite damages are not caused by any player.", 3, "El Gringo" },
                    { "3032d0fd-ef9a-43e3-b045-056b17341e06", "She can use BANG! cards as Missed! cards and vice versa. If she plays a Missed! as a BANG!, she cannot play another BANG! card that turn (unless she has a Volcanic in play).", 4, "Calamity Janet" },
                    { "dae8ba37-aaa4-4abc-bed0-61b5d446a87e", "Each time he is hit, he draws a card", 4, "Bart Cassidy" },
                    { "95273421-f62b-4429-96ff-d64726400850", "As soon as she has no cards in her hand, she draws a card from the draw pile.", 4, "Suzy Lafayette" },
                    { "c6ead2f3-271e-420f-96ef-148d6934ef95", "He is considered to have a Barrel in play at all times; he can draw! when he is the target of a BANG!, and on a Heart he is missed. If he has another real Barrel card in play, he can count both of them, giving him two chances to cancel the BANG! before playing a Missed!.", 4, "Jourdonnais" },
                    { "9e48703b-5359-4b69-8a49-413364874bb7", "He can play any number of BANG! cards during his turn.", 4, "Willy the Kid" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Games_NextCardId",
                table: "Games",
                column: "NextCardId");

            migrationBuilder.CreateIndex(
                name: "IX_GameCards_CharacterId",
                table: "GameCards",
                column: "CharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_GameCards_CharacterId1",
                table: "GameCards",
                column: "CharacterId1");

            migrationBuilder.CreateIndex(
                name: "IX_GameCards_GameId1",
                table: "GameCards",
                column: "GameId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_CharacterCards_PersonalCardId",
                table: "Characters",
                column: "PersonalCardId",
                principalTable: "CharacterCards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_GameCards_WeaponId",
                table: "Characters",
                column: "WeaponId",
                principalTable: "GameCards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

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
                name: "FK_GameCards_Games_GameId1",
                table: "GameCards",
                column: "GameId1",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Games_GameCards_NextCardId",
                table: "Games",
                column: "NextCardId",
                principalTable: "GameCards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Characters_CharacterCards_PersonalCardId",
                table: "Characters");

            migrationBuilder.DropForeignKey(
                name: "FK_Characters_GameCards_WeaponId",
                table: "Characters");

            migrationBuilder.DropForeignKey(
                name: "FK_GameCards_Characters_CharacterId",
                table: "GameCards");

            migrationBuilder.DropForeignKey(
                name: "FK_GameCards_Characters_CharacterId1",
                table: "GameCards");

            migrationBuilder.DropForeignKey(
                name: "FK_GameCards_Games_GameId1",
                table: "GameCards");

            migrationBuilder.DropForeignKey(
                name: "FK_Games_GameCards_NextCardId",
                table: "Games");

            migrationBuilder.DropTable(
                name: "CharacterCards");

            migrationBuilder.DropIndex(
                name: "IX_Games_NextCardId",
                table: "Games");

            migrationBuilder.DropIndex(
                name: "IX_GameCards_CharacterId",
                table: "GameCards");

            migrationBuilder.DropIndex(
                name: "IX_GameCards_CharacterId1",
                table: "GameCards");

            migrationBuilder.DropIndex(
                name: "IX_GameCards_GameId1",
                table: "GameCards");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "00a9e537-b114-4ad9-b099-991d540ab994");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "0145dd1c-6d99-482f-b6c0-8004e8acf5c0");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "01a8b64f-4495-4b47-b9be-b515f28a09c2");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "09ee83e6-eecf-4ef2-b428-fb98e84b2f29");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "0b182fb2-5e42-4907-bae9-1b24a1705dc0");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "0d8c6caf-f933-41ea-8203-c0aff547695d");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "1d8cfeff-97fb-4f2f-acf3-11089614f545");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "1dc762eb-bc2f-41e9-a153-cbcbf416d203");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "21b234d4-8945-44a1-a445-a3dd9596c2ce");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "2a3f955e-7bf9-4373-8c2b-ce2d17e03687");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "306feb61-1c82-4df5-946a-482db3c647e5");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "3197c353-2f60-4a18-bcb4-c078044974e5");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "338ca7fc-898c-45bf-a890-6700a74b962a");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "33b709c2-b1d0-49f7-959d-fd57b0e4de79");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "38cc612a-2a04-42b6-95e1-fcefbd9deead");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "3cd3f4d0-5460-4c70-aba4-d40e8a746351");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "3d7b9a8c-6909-4548-80c2-7fa0af29021e");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "3d91db27-d066-44be-a1a8-c5032d50d327");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "3e911bd3-ad42-438c-bc1c-99ddae893b2a");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "4047564d-611d-4251-91a2-1c9f55f47f5e");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "412398af-6c60-4e6e-b644-ca70661de169");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "419a5422-fc45-4c90-8174-b5ab6ad3e1d2");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "42acd740-a179-4abd-9ada-2bb8c3f41a69");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "432b72bd-8ed8-4fb7-95d7-fcd7eed92867");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "468fe60c-2cb5-4796-b269-d0d1f1a51243");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "489e1852-124b-485e-8174-840f88bfc2b6");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "48f24887-2667-4450-b4da-3fbd17f2a81c");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "4feefb20-55e0-4d88-953b-b544926c1dab");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "5504e073-c632-4d0f-89de-e7b2f65e8aa7");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "5549e5e7-03f0-424b-a96f-77d4e4d147a9");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "561879e9-9191-496a-bd33-23bb64e92349");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "59100acb-a7e4-474d-9c41-f2a39335f11a");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "6049aeaa-40a6-43e6-9a85-7168b6026809");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "666f0e7c-75a4-4c09-ae91-592c3bfc8533");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "685b2e22-dd1d-4c5f-abe0-12489b5ab5bc");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "6a686a73-7464-4e13-b9dd-babe5da84cc2");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "6cb057d9-9424-4999-a787-cfc698b595b2");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "734ec77d-a104-4f43-9f60-443ea924a24d");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "748dd8cf-eb4d-4534-a949-3a9e044cbedf");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "7503ad9c-a386-47e9-8c6f-312a699cc89d");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "79067734-cec1-42ca-9b5e-d135e33124f7");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "79824782-9ce7-466c-b3df-b7f9b63acf2f");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "7c91f175-315c-4d81-bee5-98b6569bf2e4");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "834acd5c-9b73-4ec6-9d10-2ee44103b681");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "83ede1f4-3fa6-46cc-9b38-52e87b547aa2");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "8ab8d52c-c328-4eb4-aa76-116ff31e65d8");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "8e17da52-5718-4b77-bd9f-fba6eb0e808b");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "8fae6e39-56ea-4b4f-b76d-b3c4d589b6da");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "9113c8da-845d-4a4a-a58d-d7f32bbf62c4");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "91d28053-a725-4fda-b36f-9f9d9989635c");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "92698c35-6ec9-43ee-b67e-656fb704160e");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "94a516a0-3c6f-44b8-bde0-940c0bb488f5");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "97a54798-dbb0-417c-971c-4eaf6b3e3264");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "9933ac2d-70c6-40c6-995a-51b43ce051b8");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "9a83f13a-c34f-4b9b-a16d-5ac76a321e46");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "a94b7c2d-1ce5-41f7-8ca8-bf44c50be37d");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "a9bff813-642b-4eee-aa82-be9cb87f074f");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "b05cdd60-f91b-4871-97a0-21853bb6d6bc");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "b6afb876-1659-402e-9593-6bea08145570");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "b96d9513-abe3-42cc-8b7e-bf02ffad7d90");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "ba93c9d7-f6b1-487d-a3b5-7889911741bf");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "bbad3b48-6391-4676-af6a-58f0915c79a8");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "bbf50ea9-4da6-486d-9689-92ce324b1eac");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "c4e101ab-d5e8-4982-8e64-8e81158f937e");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "d0699642-de35-4dfe-8eb7-e1f3bafcd191");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "d0b745d4-8ad8-4553-a406-87b2fdd4044a");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "d1948ac3-4565-4dc7-9f3e-4edac9b71d0a");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "d5c74f8b-38a3-4982-ad83-612fcba235e3");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "d69d8294-fbba-43ed-9efa-36399304d520");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "d723cefe-5af1-406e-a366-b0f228b4657b");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "dbe74a8c-bc24-4212-999f-9a21bde2081f");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "dc1152eb-32c7-4d52-b732-fd7be3b3761b");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "e0837842-4379-43f6-bd23-a5cdf6bf57bc");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "e11e5d60-83f5-4980-8a4c-e940269d6469");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "e79d1a0e-c3bb-4a52-8759-119d1288f431");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "e7a321cc-1793-4f6c-9283-d25ad84cdcba");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "e9b4fff4-16ed-4a4b-8783-0a552facabc9");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "f02e2f65-fcc1-425a-9b8d-86cff121f63f");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "f4d17cac-2070-45eb-9623-6d9cda224033");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "fb4a9642-7698-4fc1-80a2-4727e97094b8");

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: "fb804c15-9486-4006-a822-5be7411a40e1");

            migrationBuilder.DropColumn(
                name: "CurrentUserId",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "Event",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "NextCardId",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "Win",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "CharacterId",
                table: "GameCards");

            migrationBuilder.DropColumn(
                name: "CharacterId1",
                table: "GameCards");

            migrationBuilder.DropColumn(
                name: "GameId1",
                table: "GameCards");

            migrationBuilder.DropColumn(
                name: "BangState",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "DistanceFromOthers",
                table: "Characters");

            migrationBuilder.RenameColumn(
                name: "NextUserId",
                table: "Games",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "WeaponDistance",
                table: "Characters",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "Role",
                table: "Characters",
                newName: "NumberOfBangs");

            migrationBuilder.RenameColumn(
                name: "PersonalCardId",
                table: "Characters",
                newName: "RoleId");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Characters",
                newName: "UserName");

            migrationBuilder.RenameColumn(
                name: "MaxLifePoint",
                table: "Characters",
                newName: "Distance");

            migrationBuilder.RenameIndex(
                name: "IX_Characters_PersonalCardId",
                table: "Characters",
                newName: "IX_Characters_RoleId");

            migrationBuilder.CreateTable(
                name: "RoleCards",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LifePoint = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
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

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_Cards_WeaponId",
                table: "Characters",
                column: "WeaponId",
                principalTable: "Cards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_RoleCards_RoleId",
                table: "Characters",
                column: "RoleId",
                principalTable: "RoleCards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
