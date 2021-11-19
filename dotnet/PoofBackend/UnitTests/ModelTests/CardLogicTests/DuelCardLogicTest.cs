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
    public class DuelCardLogicTest
    {
        public TestInstanceCreator Creator { get; set; } = new TestInstanceCreator();

        [Fact]
        public async Task ActivateAsyncTest()
        {
            //Arrange
            var game = Creator.GetGame("elso");
            var character = game.GetCurrentCharacter().Map(null);
            var cardLogic = Creator.GetCard("Duel").Map();
            character.Character.Deck.Add(cardLogic.Card);
            //Act

            var target = game.Characters.Last().Map(null);

            int currentPlayerDeck = character.Character.Deck.Count;

            await cardLogic.ActivateAsync(character, new OptionDto 
            {
                UserId = target.Character.Id,
                CardIds = null
            });

            //Result
            Assert.Equal(currentPlayerDeck - 1, character.Character.Deck.Count);
            Assert.Equal(game.NextCard.Id, cardLogic.Card.Id);
            Assert.Equal(game.NextUserId, target.Character.Id);
            Assert.Equal(GameEvent.SingleReact, game.Event);
        }
        [Fact]
        public async Task AnswearAsyncTest()
        {
            //Arrange
            var game = Creator.GetGame("elso");
            var character = game.GetCurrentCharacter().Map(null);
            var target = game.Characters.Last().Map(null);
            var cardLogic = Creator.GetCard("Duel").Map();
            var targetCard = Creator.GetCard("Bang!").Map();
            target.Character.Deck.Add(targetCard.Card);
            game.NextCard = cardLogic.Card;
            game.NextUserId = target.Character.Id;
            game.Event = GameEvent.SingleReact;
            //Act

            int targetPlayerDeck = target.Character.Deck.Count;

            await cardLogic.AnswearAsync(character, new OptionDto
            {
                UserId = null,
                CardIds = new List<string> { targetCard.Card.Id }
            });

            //Result
            Assert.Equal(4, target.Character.LifePoint);
            Assert.Equal(targetPlayerDeck - 1, target.Character.Deck.Count);
            Assert.Equal(game.NextCard.Id, cardLogic.Card.Id);
            Assert.Equal(game.NextUserId, target.Character.Id);
            Assert.Equal(GameEvent.CallerReact, game.Event);
            Assert.Single(game.DiscardPile);
        }

        [Fact]
        public async Task NoAnswearAsyncTest()
        {
            //Arrange
            var game = Creator.GetGame("elso");
            var character = game.GetCurrentCharacter().Map(null);
            var target = game.Characters.Last().Map(null);
            var cardLogic = Creator.GetCard("Duel").Map();
            var targetCard = Creator.GetCard("Bang!").Map();
            target.Character.Deck.Add(targetCard.Card);
            game.NextCard = cardLogic.Card;
            game.NextUserId = target.Character.Id;
            game.Event = GameEvent.SingleReact;
            //Act

            int targetPlayerDeck = target.Character.Deck.Count;

            await cardLogic.AnswearAsync(character, new OptionDto
            {
                UserId = null,
                CardIds = null
            });

            //Result
            Assert.Equal(3, target.Character.LifePoint);
            Assert.Equal(targetPlayerDeck, target.Character.Deck.Count);
            Assert.Null(game.NextCard);
            Assert.Null(game.NextUserId);
            Assert.Equal(GameEvent.None, game.Event);
            Assert.Single(game.DiscardPile);
        }

        [Fact]
        public async Task CurrentAnswearAsyncTest()
        {
            //Arrange
            var game = Creator.GetGame("elso");
            var character = game.GetCurrentCharacter().Map(null);
            var target = game.Characters.Last().Map(null);
            var cardLogic = Creator.GetCard("Duel").Map();
            var targetCard = Creator.GetCard("Bang!").Map();
            character.Character.Deck.Add(targetCard.Card);
            game.NextCard = cardLogic.Card;
            game.NextUserId = target.Character.Id;
            game.Event = GameEvent.CallerReact;
            //
            int characterPlayerDeck = character.Character.Deck.Count;

            await cardLogic.AnswearAsync(character, new OptionDto
            {
                UserId = null,
                CardIds = new List<string> { targetCard.Card.Id }
            });

            //Result
            Assert.Equal(4, target.Character.LifePoint);
            Assert.Equal(characterPlayerDeck - 1, target.Character.Deck.Count);
            Assert.Equal(game.NextCard.Id, cardLogic.Card.Id);
            Assert.Equal(game.NextUserId, target.Character.Id);
            Assert.Equal(GameEvent.SingleReact, game.Event);
            Assert.Single(game.DiscardPile);
        }

        [Fact]
        public async Task NoCurrentAnswearAsyncTest()
        {
            //Arrange
            var game = Creator.GetGame("elso");
            var character = game.GetCurrentCharacter().Map(null);
            var target = game.Characters.Last().Map(null);
            var cardLogic = Creator.GetCard("Duel").Map();
            game.NextCard = cardLogic.Card;
            game.NextUserId = target.Character.Id;
            game.Event = GameEvent.CallerReact;
            //Act

            await cardLogic.AnswearAsync(character, new OptionDto
            {
                UserId = null,
                CardIds = null
            });

            //Result
            Assert.Equal(3, character.Character.LifePoint);
            Assert.Null(game.NextCard);
            Assert.Null(game.NextUserId);
            Assert.Equal(GameEvent.None, game.Event);
            Assert.Single(game.DiscardPile);
            Assert.Equal(game.DiscardPile.First().Id , cardLogic.Card.Id);
        }


    }
}
