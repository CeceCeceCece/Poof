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
    public class CatBalouCardLogicTest
    {
        public TestInstanceCreator Creator { get; set; } = new TestInstanceCreator();

        [Fact]
        public async Task ActivateAsyncTest()
        {
            //Arrange
            var game = Creator.GetGame("elso");
            var character = game.GetCurrentCharacter().Map(null);
            var cardLogic = Creator.GetCard("Cat Balou").Map();
            character.Character.Deck.Add(cardLogic.Card);
            var targetCard = Creator.GetCard("Bang!");
            //Act

            var target = game.Characters.Last().Map(null);
            target.Character.Deck.Add(targetCard);

            int currentPlayerDeck = character.Character.Deck.Count;
            int targetPlayerDeck = character.Character.Deck.Count;

            await cardLogic.ActivateAsync(character, new OptionDto 
            {
                UserId = target.Character.Id,
                CardIds = new List<string> 
                {
                    targetCard.Id
                }
            });

            //Result
            Assert.Equal(2, game.DiscardPile.Count);
            Assert.Equal(targetPlayerDeck - 1, target.Character.Deck.Count);
            Assert.Equal(currentPlayerDeck - 1, character.Character.Deck.Count);
            Assert.Equal(targetCard.Id, game.DiscardPile.First().Id);
            Assert.Equal(cardLogic.Card.Id, game.DiscardPile.Last().Id);
        }
    }
}
