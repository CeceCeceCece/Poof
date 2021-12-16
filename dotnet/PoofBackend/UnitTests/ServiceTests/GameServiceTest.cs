using Application.Models.DTOs;
using Application.Services;
using Domain;
using Domain.Constants.Enums;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnitTests.ServiceTests.Helpers;
using Xunit;

namespace UnitTests.ServiceTests
{
    public class GameServiceTest
    {
        [Fact]
        public async Task CreateGameAsyncTest()
        {
            //Arrange
            var context = new ConnectionFactory().CreateContextForSQLite();

            var service = new GameService(context);
            //Act
            await service.CreateGameAsync(new Lobby
            {
                Name = "Test",
                Vezeto = "TestUser",
                Connections = new List<Connection>
                {
                    new Connection("id1", "TestUser1", "TestUserId1"),
                    new Connection("id2", "TestUser2", "TestUserId2"),
                    new Connection("id3", "TestUser3", "TestUserId3"),
                    new Connection("id4", "TestUser4", "TestUserId4"),
                    new Connection("id5", "TestUser5", "TestUserId5")
                }
            }, null);

            var game = await service.GetGameAsync(await context.Games.Where(x => x.Name == "Test").Select(x => x.Id).SingleAsync());
            //Result

            Assert.NotNull(game);
            Assert.Equal("Test", game.Name);
            Assert.True(game.Characters.All(x => x.Deck.Count == x.LifePoint || (x.Role == RoleType.Sheriff && x.Deck.Count == x.LifePoint + 1)));
            Assert.NotNull(game.Deck.First().Card);
            Assert.NotNull(game.Characters.First().Deck.First().Card);
            Assert.NotNull(game.Characters.First().PersonalCard);
            Assert.Single(game.Characters.Where(x => x.Role == RoleType.Sheriff).ToList());
            Assert.Single(game.Characters.Where(x => x.Role == RoleType.Renegade).ToList());
            Assert.Single(game.Characters.Where(x => x.Role == RoleType.DeputySheriff).ToList());
            Assert.Equal(2, game.Characters.Where(x => x.Role == RoleType.Outlaw).ToList().Count);
        }

        [Fact]
        public async Task BangActiveAsyncTest()
        {
            //Arrange
            var context = new ConnectionFactory().CreateContextForSQLite();

            var service = new GameService(context);

            var game = await SeedDatabase(context, "Bang!");

            //Act
            await service.CardActivateAsync(game.Id, game.CurrentUserId, "tesztgamecardid", new OptionDto
            {
                UserId = game.GetNextCharacter().Id
            });

            var result = await service.GetGameAsync(game.Id);
            //Result

            Assert.NotNull(result);
            Assert.Equal(GameEvent.SingleReact, result.Event);
            Assert.Equal("tesztid", result.NextCard.Card.Id);
            Assert.Equal("tesztgamecardid", result.NextCard.Id);
            Assert.Equal(result.GetNextCharacter().Id, result.NextUserId);
        }

        [Fact]
        public async Task BangNoAnswearAsyncTest()
        {
            //Arrange
            var context = new ConnectionFactory().CreateContextForSQLite();

            var service = new GameService(context);

            var game = await SeedDatabase(context, "Bang!");

            //Act
            await service.CardActivateAsync(game.Id, game.CurrentUserId, "tesztgamecardid", new OptionDto
            {
                UserId = game.GetNextCharacter().Id
            });

            var nextLife = game.GetNextCharacter().LifePoint;

            await service.CardAnswearAsync(game.Id, game.NextUserId, new OptionDto { CardIds = null });

            var result = await service.GetGameAsync(game.Id);
            //Result

            Assert.NotNull(result);
            Assert.Equal(GameEvent.None, result.Event);
            Assert.Equal(nextLife - 1, game.GetNextCharacter().LifePoint);
            Assert.Single(game.DiscardPile);
            Assert.Null(result.NextUserId);
        }

        [Fact]
        public async Task BangAnswearAsyncTest()
        {
            //Arrange
            var context = new ConnectionFactory().CreateContextForSQLite();

            var service = new GameService(context);

            var game = await SeedDatabase(context, "Bang!");

            var next = game.GetNextCharacter();
            next.Deck.Add(new GameCard("missedId", new Card
            {
                Id = "missedId",
                Name = "Missed!"
            }));

            await context.SaveChangesAsync();

            //Act
            await service.CardActivateAsync(game.Id, game.CurrentUserId, "tesztgamecardid", new OptionDto
            {
                UserId = game.GetNextCharacter().Id
            });

            var nextLife = game.GetNextCharacter().LifePoint;

            await service.CardAnswearAsync(game.Id, game.NextUserId, new OptionDto { CardIds = new List<string> { "missedId" } });

            var result = await service.GetGameAsync(game.Id);
            //Result

            Assert.NotNull(result);
            Assert.Equal(GameEvent.None, result.Event);
            Assert.Equal(nextLife, game.GetNextCharacter().LifePoint);
            Assert.Equal(2, game.DiscardPile.Count);
            Assert.Null(result.NextUserId);
        }

        [Fact]
        public async Task BarrelActiveAsyncTest()
        {
            //Arrange
            var context = new ConnectionFactory().CreateContextForSQLite();

            var service = new GameService(context);

            var game = await SeedDatabase(context, "Barrel");

            //Act
            await service.CardActivateAsync(game.Id, game.CurrentUserId, "tesztgamecardid", null);

            var result = await service.GetGameAsync(game.Id);
            //Result

            var current = result.GetCurrentCharacter();
            Assert.NotNull(result);
            Assert.Single(current.EquipedCards);
            Assert.Equal("tesztgamecardid", current.EquipedCards.First().Id);
        }

        public async Task<Game> SeedDatabase(PoofDbContext context, string cardName)
        {
            var service = new GameService(context);

            await service.CreateGameAsync(new Lobby
            {
                Name = "Test",
                Vezeto = "TestUser",
                Connections = new List<Connection>
                {
                    new Connection("id1", "TestUser1", "TestUserId1"),
                    new Connection("id2", "TestUser2", "TestUserId2"),
                    new Connection("id3", "TestUser3", "TestUserId3"),
                    new Connection("id4", "TestUser4", "TestUserId4"),
                    new Connection("id5", "TestUser5", "TestUserId5")
                }
            }, null);

            var gameId = await context.Games.Where(x => x.Name == "Test").Select(x => x.Id).SingleAsync();

            var game = await service.GetGameAsync(gameId);

            game.GetCurrentCharacter().Deck.Add(new GameCard("tesztgamecardid", new Card
            {
                Id = "tesztid",
                Description = "",
                Name = cardName,
                Suite = CardSuits.Clubs,
                Type = CardType.Action,
                Value = CardValues.Ace
            }));

            await context.SaveChangesAsync();

            return game;
        }

    }
}
