using Domain.Constants.Enums;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace UnitTests
{
    public class TestInstanceCreator
    {
        public Game GetGame(string name) 
        {
            var game = new Game
            {
                Id = Guid.NewGuid().ToString(),
                Deck = new List<GameCard>
                {
                    GetCard("Missed!"),
                    GetCard("Bang!"),
                    GetCard("Dinamite")
                },
                DiscardPile = new List<GameCard>(),
                Event = GameEvent.None,
                Messages = new List<Message>(),
                Name = name,
                NextCard = null,
                NextUserId = null,
                Password = ""
            };

            game.Characters.AddRange(new List<Character>
            {
                GetCharacter("Black Jack", game),
                GetCharacter("Bart Cassidy", game),
                GetCharacter("Calamity Janet", game)
            });

            game.CurrentUserId = game.Characters.First().Id;
            return game;
        }
        public Character GetCharacter(string name, Game game) 
        {
            return new Character
            {
                Id = Guid.NewGuid().ToString(),
                Name = name,
                AimDistance = 1,
                DistanceFromOthers = 1,
                WeaponDistance = 1,
                Weapon = null,
                BangState = BangState.One,
                ConnectionId = Guid.NewGuid().ToString(),
                Deck = new List<GameCard>
                {
                    GetCard("Bang!"),
                    GetCard("Bang!")
                },
                EquipedCards = new List<GameCard>(),
                LifePoint = 4,
                MaxLifePoint = 4,
                Role = new RoleCard
                {
                    Id = Guid.NewGuid().ToString(),
                    LifePoint = 4,
                    Name = name,
                    Description = ""
                },
                Game = game
            };
        }

        public GameCard GetCard(string name, CardSuits suit = CardSuits.Clubs, CardType type = CardType.Action, CardValues value = CardValues.Ace) 
        {
            return new GameCard(Guid.NewGuid().ToString(), new Card
            {
                Id = Guid.NewGuid().ToString(),
                Description = "",
                Name = name,
                Suite = suit,
                Type = type,
                Value = value
            },
            name
            );
        }
    }
}
