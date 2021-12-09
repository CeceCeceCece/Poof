using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests
{
    public class StagecoachCardLogicTest
    {
        public TestInstanceCreator Creator { get; set; } = new TestInstanceCreator();

        [Fact]
        public async Task ActivateAsyncTest()
        {
            //Arrange
            var game = Creator.GetGame("elso");
            var character = game.GetCurrentCharacter().Map(null);
            var cardLogic = Creator.GetCard("Stagecoach").Map();
            character.Character.Deck.Add(cardLogic.Card);
            game.Deck.AddRange(new List<GameCard>
            {
                Creator.GetCard("Bang!"),
                Creator.GetCard("Bang!"),
                Creator.GetCard("Bang!"),
                Creator.GetCard("Bang!"),
                Creator.GetCard("Bang!")
            });
            //Act

            int currentPlayerDeck = character.Character.Deck.Count;
            int gameDeck = game.Deck.Count;

            await cardLogic.ActivateAsync(character, null);

            //Result
            Assert.Single(game.DiscardPile);
            Assert.Equal(currentPlayerDeck - 1 + 2, character.Character.Deck.Count);
            Assert.Equal(gameDeck - 2, game.Deck.Count);
        }
    }
}
