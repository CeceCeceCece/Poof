using Application.Models.CardLogic;
using Application.Models.DTOs;
using Domain.Constants.Enums;
using Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests
{
    public class BangCardLogicTest
    {
        public TestInstanceCreator Creator { get; set; } = new TestInstanceCreator();

        [Fact]
        public async Task ActivateAsyncTest()
        {
            //Arrange
            var game = Creator.GetGame("elso");
            var character = game.GetCurrentCharacter().Map(null);
            var logic = new BangCardLogic(character.Character.Deck.First());
            //Act

            int currentPlayerBefore = game.Characters.Last().Deck.Count;
            await logic.ActivateAsync(character, new OptionDto
            {
                CardIds = null,
                UserId = game.Characters.Last().Id
            });

            //Result
            Assert.Equal(GameEvent.SingleReact, game.Event);
            Assert.Equal(game.Characters.Last().Id, game.NextUserId);
            Assert.Equal(currentPlayerBefore, character.Character.Deck.Count + 1);
            Assert.Equal(logic.Card, game.NextCard);
            Assert.Single(game.DiscardPile);
        }

        [Fact]
        public async Task AnswearAsyncTest()
        {
            //Arrange
            var game = Creator.GetGame("elso");
            var character = game.GetCurrentCharacter().Map(null);
            var logic = new BangCardLogic(character.Character.Deck.First());
            var card = Creator.GetCard("Missed!");
            game.Characters.Last().Deck.Add(card);
            //Act

            int before = game.Characters.Last().Deck.Count;
            await logic.ActivateAsync(character, new OptionDto
            {
                CardIds = null,
                UserId = game.Characters.Last().Id
            });

            await logic.AnswearAsync(character, new OptionDto
            {
                CardIds = new List<string> { card.Id },
                UserId = null
            });

            //Result
            Assert.Equal(GameEvent.None, game.Event);
            Assert.Null(game.NextUserId);
            Assert.Null(game.NextCard);
            Assert.Equal(before, game.Characters.Last().Deck.Count + 1);
        }

        [Fact]
        public async Task AnswearAsyncLostTest()
        {
            //Arrange
            var game = Creator.GetGame("elso");
            var character = game.GetCurrentCharacter().Map(null);
            var logic = new BangCardLogic(character.Character.Deck.First());
            var target = game.Characters.Last();
            //Act

            int lifepoint = game.Characters.Last().LifePoint;
            await logic.ActivateAsync(character, new OptionDto
            {
                CardIds = null,
                UserId = game.Characters.Last().Id
            });

            await logic.AnswearAsync(character, new OptionDto
            {
                CardIds = null,
                UserId = null
            });

            //Result
            Assert.Equal(GameEvent.None, game.Event);
            Assert.Null(game.NextUserId);
            Assert.Null(game.NextCard);
            Assert.Equal(lifepoint ,target.LifePoint + 1);
        }
    }
}
