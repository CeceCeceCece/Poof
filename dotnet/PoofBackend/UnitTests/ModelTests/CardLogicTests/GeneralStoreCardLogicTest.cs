using Application.Models.CardLogic;
using Application.Models.CharacterLogic;
using Application.Models.DTOs;
using Domain.Constants.Enums;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests
{
    public class GeneralStoreCardLogicTest
    {
        public TestInstanceCreator Creator { get; set; } = new TestInstanceCreator();

        [Fact]
        public async Task ActivateAsyncTest()
        {
            //Arrange
            var game = Creator.GetGame("elso");
            var character = game.GetCurrentCharacter().Map(null);
            var cardLogic = Creator.GetCard("General Store").Map();
            character.Character.Deck.Add(cardLogic.Card);
            //Act
            int currentPlayerDeck = character.Character.Deck.Count;

            await cardLogic.ActivateAsync(character, null);

            //Result
            Assert.Equal(currentPlayerDeck - 1, character.Character.Deck.Count);
            Assert.Equal(game.NextCard.Id, cardLogic.Card.Id);
            Assert.Equal(game.NextUserId, character.Character.Id);
            Assert.Equal(GameEvent.AllReact, game.Event);
        }

        [Fact]
        public async Task AnswearAsyncTest()
        {
            //Arrange
            var game = Creator.GetGame("elso");
            var character = game.GetCurrentCharacter().Map(null);
            var cardLogic = Creator.GetCard("General Store").Map();
            var choosenCard = Creator.GetCard("Bang!");
            var target = game.Characters.ElementAt(1).Map(null);
            game.NextCard = cardLogic.Card;
            game.Deck.Add(choosenCard);
            game.Event = GameEvent.AllReact;
            game.NextUserId = target.Character.Id;
            //Act
            int targetPlayerDeck = target.Character.Deck.Count;
            int gameDeck = game.Deck.Count;

            await cardLogic.AnswearAsync(target, new OptionDto 
            {
                CardIds = new List<string> { choosenCard.Id }
            });

            //Result
            Assert.Equal(targetPlayerDeck + 1, target.Character.Deck.Count);
            Assert.Equal(gameDeck - 1, game.Deck.Count);
            Assert.Equal(game.NextCard.Id, cardLogic.Card.Id);
            Assert.Equal(game.NextUserId, game.Characters.ElementAt(2).Id);
            Assert.Equal(GameEvent.AllReact, game.Event);
        }

        [Fact]
        public async Task EndReactAsyncTest()
        {
            //Arrange
            var game = Creator.GetGame("elso");
            var character = game.GetCurrentCharacter().Map(null);
            var cardLogic = Creator.GetCard("General Store").Map();
            var choosenCard = Creator.GetCard("Bang!");
            var target = game.Characters.ElementAt(2).Map(null);
            game.NextCard = cardLogic.Card;
            game.Deck.Add(choosenCard);
            game.Event = GameEvent.AllReact;
            game.NextUserId = target.Character.Id;
            //Act
            int targetPlayerDeck = target.Character.Deck.Count;
            int gameDeck = game.Deck.Count;

            await cardLogic.AnswearAsync(target, new OptionDto
            {
                CardIds = new List<string> { choosenCard.Id }
            });

            //Result
            Assert.Equal(targetPlayerDeck + 1, target.Character.Deck.Count);
            Assert.Equal(gameDeck - 1, game.Deck.Count);
            Assert.Null(game.NextCard);
            Assert.Null(game.NextUserId);
            Assert.Equal(GameEvent.None, game.Event);
            Assert.Single(game.DiscardPile);
        }
    }
}
