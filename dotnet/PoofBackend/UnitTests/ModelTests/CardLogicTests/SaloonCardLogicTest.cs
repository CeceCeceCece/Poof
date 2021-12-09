using Domain.Constants.Enums;
using Domain.Entities;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests
{
    public class SaloonCardLogicTest
    {
        public TestInstanceCreator Creator { get; set; } = new TestInstanceCreator();

        [Fact]
        public async Task ActivateAsyncTest()
        {
            //Arrange
            var game = Creator.GetGame("elso");
            var character = game.GetCurrentCharacter().Map(null);
            var cardLogic = Creator.GetCard("Saloon").Map();
            character.Character.Deck.Add(cardLogic.Card);
            //Act

            foreach (var item in game.Characters)
            {
                item.LifePoint = 3;
            }

            int currentPlayerBefore = character.Character.Deck.Count;

            await cardLogic.ActivateAsync(character, null);

            //Result
            Assert.Equal(GameEvent.None, game.Event);
            Assert.Single(game.DiscardPile);
            Assert.Equal(currentPlayerBefore, character.Character.Deck.Count + 1);
            Assert.Equal(4, game.Characters.ElementAt(0).LifePoint);
            Assert.Equal(4, game.Characters.ElementAt(1).LifePoint);
            Assert.Equal(4, game.Characters.ElementAt(2).LifePoint);
        }
    }
}
