using Application.Models.DTOs;
using Domain.Constants.Enums;
using Domain.Entities;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests
{
    public class JailCardLogicTest
    {
        public TestInstanceCreator Creator { get; set; } = new TestInstanceCreator();

        [Fact]
        public async Task ActivateAsyncTest()
        {
            //Arrange
            var game = Creator.GetGame("elso");
            var character = game.GetCurrentCharacter().Map(null);
            var cardLogic = Creator.GetCard("Jail").Map();
            character.Character.Deck.Add(cardLogic.Card);
            var target = game.Characters.ElementAt(1).Map(null);
            //Act

            int currentPlayerBefore = character.Character.Deck.Count;

            await cardLogic.ActivateAsync(character, new OptionDto
            {
                UserId = target.Character.Id
            });

            //Result
            Assert.Equal(GameEvent.None, game.Event);
            Assert.Single(target.Character.EquipedCards);
            Assert.Equal(cardLogic.Card.Id, target.Character.EquipedCards.First().Id);
            Assert.Equal(currentPlayerBefore, character.Character.Deck.Count + 1);
        }

        //TODO: more
    }
}
