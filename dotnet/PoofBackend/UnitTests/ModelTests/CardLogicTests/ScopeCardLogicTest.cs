using Domain.Constants.Enums;
using Domain.Entities;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests
{
    public class ScopeCardLogicTest
    {
        public TestInstanceCreator Creator { get; set; } = new TestInstanceCreator();

        [Fact]
        public async Task ActivateAsyncTest()
        {
            //Arrange
            var game = Creator.GetGame("elso");
            var character = game.GetCurrentCharacter().Map(null);
            var cardLogic = Creator.GetCard("Scope").Map();
            character.Character.Deck.Add(cardLogic.Card);
            //Act

            int currentPlayerBefore = character.Character.Deck.Count;
            int currentPlayerAim = character.Character.AimDistance;

            await cardLogic.ActivateAsync(character, null);

            //Result
            Assert.Equal(GameEvent.None, game.Event);
            Assert.Single(character.Character.EquipedCards);
            Assert.Equal(character.Character.EquipedCards.First().Id, cardLogic.Card.Id);
            Assert.Equal(currentPlayerBefore, character.Character.Deck.Count + 1);
            Assert.Equal(currentPlayerAim, character.Character.AimDistance - 1);
        }

        [Fact]
        public async Task DeActivateAsyncTest()
        {
            //Arrange
            var game = Creator.GetGame("elso");
            var character = game.GetCurrentCharacter().Map(null);
            var cardLogic = Creator.GetCard("Scope").Map();
            character.Character.EquipedCards.Add(cardLogic.Card);
            //Act

            int currentPlayerAim = character.Character.AimDistance;
            await character.DropCardAsync(cardLogic.Card.Id);

            //Result
            Assert.Equal(GameEvent.None, game.Event);
            Assert.Empty(character.Character.EquipedCards);
            Assert.Equal(currentPlayerAim, character.Character.AimDistance + 1);
            Assert.Single(game.DiscardPile);
        }


    }
}
