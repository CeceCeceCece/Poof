using Domain.Constants.Enums;
using Domain.Entities;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests
{
    public class MustangCardLogicTest
    {
        public TestInstanceCreator Creator { get; set; } = new TestInstanceCreator();

        [Fact]
        public async Task ActivateAsyncTest()
        {
            //Arrange
            var game = Creator.GetGame("elso");
            var character = game.GetCurrentCharacter().Map(null);
            var cardLogic = Creator.GetCard("Mustang").Map();
            character.Character.Deck.Add(cardLogic.Card);
            //Act

            int currentPlayerBefore = character.Character.Deck.Count;
            int currentPlayerDistance = character.Character.DistanceFromOthers;

            await cardLogic.ActivateAsync(character, null);

            //Result
            Assert.Equal(GameEvent.None, game.Event);
            Assert.Single(character.Character.EquipedCards);
            Assert.Equal(cardLogic.Card.Id, character.Character.EquipedCards.First().Id);
            Assert.Equal(currentPlayerBefore, character.Character.Deck.Count + 1);
            Assert.Equal(currentPlayerDistance + 1, character.Character.DistanceFromOthers);
        }

        [Fact]
        public async Task DeActivateAsyncTest()
        {
            //Arrange
            var game = Creator.GetGame("elso");
            var character = game.GetCurrentCharacter().Map(null);
            var cardLogic = Creator.GetCard("Mustang").Map();
            character.Character.EquipedCards.Add(cardLogic.Card);
            //Act

            int currentPlayerDistance = character.Character.DistanceFromOthers;

            await character.DropCardAsync(cardLogic.Card.Id);

            //Result
            Assert.Equal(GameEvent.None, game.Event);
            Assert.Empty(character.Character.EquipedCards);
            Assert.Single(game.DiscardPile);
            Assert.Equal(currentPlayerDistance - 1, character.Character.DistanceFromOthers);
        }


    }
}
