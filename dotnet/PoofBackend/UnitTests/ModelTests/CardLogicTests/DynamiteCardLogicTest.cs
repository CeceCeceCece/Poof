using Domain.Constants.Enums;
using Domain.Entities;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests
{
    public class DynamiteCardLogicTest
    {
        public TestInstanceCreator Creator { get; set; } = new TestInstanceCreator();

        [Fact]
        public async Task ActivateAsyncTest()
        {
            //Arrange
            var game = Creator.GetGame("elso");
            var character = game.GetCurrentCharacter().Map(null);
            var cardLogic = Creator.GetCard("Dynamite").Map();
            character.Character.Deck.Add(cardLogic.Card);
            //Act

            int currentPlayerBefore = character.Character.Deck.Count;

            await cardLogic.ActivateAsync(character, null);

            //Result
            Assert.Equal(GameEvent.None, game.Event);
            Assert.Single(character.Character.EquipedCards);
            Assert.Equal(cardLogic.Card.Id, character.Character.EquipedCards.First().Id);
            Assert.Equal(currentPlayerBefore, character.Character.Deck.Count + 1);
        }

        [Fact]
        public async Task OnActiveAsyncTest()
        {
            //Arrange
            var game = Creator.GetGame("elso");
            var character = game.GetCurrentCharacter().Map(null);
            var cardLogic = Creator.GetCard("Dynamite").Map();
            character.Character.EquipedCards.Add(cardLogic.Card);
            game.Event = GameEvent.Draw;
            var card = Creator.GetCard("Bang!", CardSuits.Spades, value: CardValues.Four);
            game.Deck.Insert(0, card);
            //Act

            await cardLogic.OnActiveAsync(character);

            //Result
            Assert.Empty(character.Character.EquipedCards);
            Assert.Empty(game.Characters.ElementAt(1).EquipedCards);
            Assert.Equal(1, character.Character.LifePoint);
            Assert.Equal(2, game.DiscardPile.Count);
        }
        [Fact]
        public async Task OnActiveAsyncPassTest()
        {
            //Arrange
            var game = Creator.GetGame("elso");
            var character = game.GetCurrentCharacter().Map(null);
            var cardLogic = Creator.GetCard("Dynamite").Map();
            character.Character.EquipedCards.Add(cardLogic.Card);
            game.Event = GameEvent.Draw;
            var card = Creator.GetCard("Bang!", CardSuits.Spades, value: CardValues.Ace);
            game.Deck.Insert(0, card);
            //Act

            await cardLogic.OnActiveAsync(character);

            //Result
            Assert.Empty(character.Character.EquipedCards);
            Assert.Single(game.Characters.ElementAt(1).EquipedCards);
            Assert.Equal(4, character.Character.LifePoint);
            Assert.Single(game.DiscardPile);
        }

    }
}
