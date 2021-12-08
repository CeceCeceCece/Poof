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
    public class BarrelCardLogicTest
    {
        public TestInstanceCreator Creator { get; set; } = new TestInstanceCreator();

        [Fact]
        public async Task ActivateAsyncTest()
        {
            //Arrange
            var game = Creator.GetGame("elso");
            var character = game.GetCurrentCharacter().Map(null);
            var cardLogic = Creator.GetCard("Barrel").Map();
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
            var cardLogic = Creator.GetCard("Barrel").Map();
            character.Character.Deck.Add(cardLogic.Card);
            game.Deck.Insert(0, Creator.GetCard("Bang!", suit: CardSuits.Hearths));
            game.NextCard = Creator.GetCard("Bang!");
            game.NextUserId = game.Characters.Last().Id;
            game.Event = GameEvent.SingleReact;
            //Act

            var target = game.GetReactionCharacter().Map(null);
            target.Character.Deck.Add(cardLogic.Card);

            await cardLogic.ActivateAsync(target, null);
            await cardLogic.OnActiveAsync(target);

            //Result
            Assert.Equal(GameEvent.None, game.Event);
            Assert.Single(target.Character.EquipedCards);
            Assert.Equal(cardLogic.Card.Id, target.Character.EquipedCards.First().Id);
            Assert.Null(game.NextUserId);
            Assert.Null(game.NextCard);
            Assert.Single(game.DiscardPile);
        }
    }
}
