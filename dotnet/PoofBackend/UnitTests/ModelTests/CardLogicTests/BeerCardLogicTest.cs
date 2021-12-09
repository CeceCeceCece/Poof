using Domain.Entities;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests
{
    public class BeerCardLogicTest
    {
        public TestInstanceCreator Creator { get; set; } = new TestInstanceCreator();

        [Fact]
        public async Task ActivateAsyncTest()
        {
            //Arrange
            var game = Creator.GetGame("elso");
            var character = game.GetCurrentCharacter().Map(null);
            var cardLogic = Creator.GetCard("Beer").Map();
            character.Character.Deck.Add(cardLogic.Card);
            character.Character.LifePoint = 2;
            //Act

            int currentPlayerLife = character.Character.LifePoint;
            int currentPlayerDeck = character.Character.Deck.Count;

            await cardLogic.ActivateAsync(character, null);

            //Result
            Assert.Single(game.DiscardPile);
            Assert.Equal(currentPlayerLife + 1, character.Character.LifePoint);
            Assert.Equal(currentPlayerDeck - 1, character.Character.Deck.Count);
        }

        [Fact]
        public async Task ActivateAsyncWithMaxLifeTest()
        {
            //Arrange
            var game = Creator.GetGame("elso");
            var character = game.GetCurrentCharacter().Map(null);
            var cardLogic = Creator.GetCard("Beer").Map();
            character.Character.Deck.Add(cardLogic.Card);
            //Act

            int currentPlayerLife = character.Character.LifePoint;
            int currentPlayerDeck = character.Character.Deck.Count;

            await cardLogic.ActivateAsync(character, null);

            //Result
            Assert.Single(game.DiscardPile);
            Assert.Equal(currentPlayerLife, character.Character.LifePoint);
            Assert.Equal(currentPlayerDeck - 1, character.Character.Deck.Count);
        }
    }
}
