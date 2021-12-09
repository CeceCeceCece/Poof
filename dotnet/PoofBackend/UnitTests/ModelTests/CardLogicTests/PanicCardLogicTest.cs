using Application.Models.DTOs;
using Domain.Constants.Enums;
using Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests
{
    public class PanicCardLogicTest
    {
        public TestInstanceCreator Creator { get; set; } = new TestInstanceCreator();

        [Fact]
        public async Task ActivateAsyncTest()
        {
            //Arrange
            var game = Creator.GetGame("elso");
            var character = game.GetCurrentCharacter().Map(null);
            var cardLogic = Creator.GetCard("Panic!").Map();
            character.Character.Deck.Add(cardLogic.Card);
            var target = game.Characters.ElementAt(1).Map(null);
            var targetCard = Creator.GetCard("Bang!");
            target.Character.Deck.Add(targetCard);
            //Act

            int currentPlayerBefore = character.Character.Deck.Count;
            int targetPlayerBefore = target.Character.Deck.Count;

            await cardLogic.ActivateAsync(character, new OptionDto
            {
                UserId = target.Character.Id,
                CardIds = new List<string> { targetCard.Id }
            });

            //Result
            Assert.Equal(GameEvent.None, game.Event);
            Assert.Equal(currentPlayerBefore, character.Character.Deck.Count);
            Assert.Equal(targetPlayerBefore - 1, target.Character.Deck.Count);
            Assert.Single(game.DiscardPile);
        }

        [Fact]
        public async Task ActivateAsyncFromEquipedTest()
        {
            //Arrange
            var game = Creator.GetGame("elso");
            var character = game.GetCurrentCharacter().Map(null);
            var cardLogic = Creator.GetCard("Panic!").Map();
            character.Character.Deck.Add(cardLogic.Card);
            var target = game.Characters.ElementAt(1).Map(null);
            var targetCard = Creator.GetCard("Bang!");
            target.Character.EquipedCards.Add(targetCard);
            //Act

            int currentPlayerBefore = character.Character.Deck.Count;
            int targetPlayerEquiped = target.Character.EquipedCards.Count;

            await cardLogic.ActivateAsync(character, new OptionDto
            {
                UserId = target.Character.Id,
                CardIds = new List<string> { targetCard.Id }
            });

            //Result
            Assert.Equal(GameEvent.None, game.Event);
            Assert.Equal(currentPlayerBefore, character.Character.Deck.Count);
            Assert.Equal(targetPlayerEquiped - 1, target.Character.EquipedCards.Count);
            Assert.Single(game.DiscardPile);
        }
    }
}
