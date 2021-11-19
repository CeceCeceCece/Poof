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
    public class WinchesterCardLogicTest
    {
        public TestInstanceCreator Creator { get; set; } = new TestInstanceCreator();

        [Fact]
        public async Task ActivateAsyncTest()
        {
            //Arrange
            var game = Creator.GetGame("elso");
            var character = game.GetCurrentCharacter().Map(null);
            var cardLogic = Creator.GetCard("Winchester").Map();
            character.Character.Deck.Add(cardLogic.Card);
            //Act

            int currentPlayerBefore = character.Character.Deck.Count;

            await cardLogic.ActivateAsync(character, null);

            //Result
            Assert.Equal(GameEvent.None, game.Event);
            Assert.NotNull(character.Character.Weapon);
            Assert.Equal(character.Character.Weapon.Id, cardLogic.Card.Id);
            Assert.Equal(currentPlayerBefore, character.Character.Deck.Count + 1);
            Assert.Equal(5, character.Character.WeaponDistance);
        }

        [Fact]
        public async Task DeActivateAsyncTest()
        {
            //Arrange
            var game = Creator.GetGame("elso");
            var character = game.GetCurrentCharacter().Map(null);
            var cardLogic = Creator.GetCard("Winchester").Map();
            character.Character.Weapon = cardLogic.Card;
            character.Character.WeaponDistance = 5;
            //Act

            await character.DropCardAsync(cardLogic.Card.Id);

            //Result
            Assert.Equal(GameEvent.None, game.Event);
            Assert.Null(character.Character.Weapon);
            Assert.Equal(1, character.Character.WeaponDistance);
            Assert.Single(game.DiscardPile);
        }


    }
}
