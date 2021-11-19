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
    public class IndiansCardLogicTest
    {
        public TestInstanceCreator Creator { get; set; } = new TestInstanceCreator();

        [Fact]
        public async Task ActivateAsyncTest()
        {
            //Arrange
            var game = Creator.GetGame("elso");
            var character = game.GetCurrentCharacter().Map(null);
            var cardLogic = Creator.GetCard("Indians!").Map();
            character.Character.Deck.Add(cardLogic.Card);
            //Act
            int currentPlayerDeck = character.Character.Deck.Count;

            await cardLogic.ActivateAsync(character, null);

            //Result
            Assert.Equal(currentPlayerDeck - 1, character.Character.Deck.Count);
            Assert.Equal(game.NextCard.Id, cardLogic.Card.Id);
            Assert.Equal(game.NextUserId, game.Characters.ElementAt(1).Id);
            Assert.Equal(GameEvent.AllReact, game.Event);
        }
        [Fact]
        public async Task AnswearAsyncTest()
        {
            //Arrange
            var game = Creator.GetGame("elso");
            var character = game.GetCurrentCharacter().Map(null);
            var cardLogic = Creator.GetCard("Indians!").Map();
            var target = game.Characters.ElementAt(1).Map(null);
            var card = Creator.GetCard("Bang!");
            target.Character.Deck.Add(card);
            game.NextCard = cardLogic.Card;
            game.NextUserId = target.Character.Id;
            game.Event = GameEvent.AllReact;
            //Act
            int targetPlayerDeck = target.Character.Deck.Count;

            await cardLogic.AnswearAsync(character, new OptionDto 
            {
                CardIds = new List<string> { card.Id }
            });

            //Result
            Assert.Equal(targetPlayerDeck - 1, target.Character.Deck.Count);
            Assert.Equal(game.NextCard.Id, cardLogic.Card.Id);
            Assert.Equal(game.NextUserId, game.Characters.ElementAt(2).Id);
            Assert.Equal(GameEvent.AllReact, game.Event);
            Assert.Single(game.DiscardPile);
            Assert.Equal(4, target.Character.LifePoint);
        }

        [Fact]
        public async Task NoAnswearAsyncTest()
        {
            //Arrange
            var game = Creator.GetGame("elso");
            var character = game.GetCurrentCharacter().Map(null);
            var cardLogic = Creator.GetCard("Indians!").Map();
            var target = game.Characters.ElementAt(1).Map(null);
            game.NextCard = cardLogic.Card;
            game.NextUserId = target.Character.Id;
            game.Event = GameEvent.AllReact;
            //Act

            await cardLogic.AnswearAsync(character, new OptionDto
            {
                CardIds = null
            });

            //Result
            Assert.Equal(game.NextCard.Id, cardLogic.Card.Id);
            Assert.Equal(game.NextUserId, game.Characters.ElementAt(2).Id);
            Assert.Equal(GameEvent.AllReact, game.Event);
            Assert.Empty(game.DiscardPile);
            Assert.Equal(3, target.Character.LifePoint);
        }

        [Fact]
        public async Task EndReactionAsyncTest()
        {
            //Arrange
            var game = Creator.GetGame("elso");
            var character = game.GetCurrentCharacter().Map(null);
            var cardLogic = Creator.GetCard("Indians!").Map();
            var target = game.Characters.ElementAt(2).Map(null);
            var card = Creator.GetCard("Bang!");
            target.Character.Deck.Add(card);
            game.NextCard = cardLogic.Card;
            game.NextUserId = target.Character.Id;
            game.Event = GameEvent.AllReact;
            //Act
            int targetPlayerDeck = target.Character.Deck.Count;

            await cardLogic.AnswearAsync(character, new OptionDto
            {
                CardIds = new List<string> { card.Id }
            });

            //Result
            Assert.Equal(targetPlayerDeck - 1, target.Character.Deck.Count);
            Assert.Null(game.NextCard);
            Assert.Null(game.NextUserId);
            Assert.Equal(GameEvent.None, game.Event);
            Assert.Equal(2, game.DiscardPile.Count);
            Assert.Equal(4, target.Character.LifePoint);
        }
    }
}
