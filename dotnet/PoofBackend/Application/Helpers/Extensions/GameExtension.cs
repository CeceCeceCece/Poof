using Application.Constants;
using Application.Exceptions;
using Application.Models.DTOs;
using Application.SignalR;
using Domain.Constants.Enums;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities 
{
    public static class GameExtension
    {
        public static Character GetCharacterById(this Game game, string userId) 
        {
            var character = game.Characters.SingleOrDefault(x => x.Id == userId);
            if (character is null)
                throw new PoofException(CharacterMessages.JATEKOS_NEM_A_JATEK_RESZE);
            return character;
        }

        public static List<GameCard> GetCards(this Game game, int count)
        {
            if (game.Deck.Count < count)
            {
                var lastCard = game.DiscardPile.Last();
                game.DiscardPile.Remove(lastCard);
                var rnd = new Random();
                game.Deck.AddRange(game.DiscardPile.OrderBy(item => rnd.Next()).ToList());
                game.DiscardPile = new List<GameCard> { lastCard };
            }

            return game.Deck.Take(count).ToList();
        }

        public static GameCard GetCard(this Game game, string cardId)
        {
            var card = game.Deck.SingleOrDefault(x => x.Id == cardId) ?? throw new PoofException(GameMessages.KARTYA_NEM_A_PAKLI_RESZE);
            game.Deck.Remove(card);
            if (game.Deck.Count <= 0)
            {
                var lastCard = game.DiscardPile.Last();
                game.DiscardPile.Remove(lastCard);
                var rnd = new Random();
                game.Deck.AddRange(game.DiscardPile.OrderBy(item => rnd.Next()).ToList());
                game.DiscardPile = new List<GameCard> { lastCard };
            }

            return card;
        }

        public static Task<bool> CheckNextCardAsync(this Game game, CardSuits suit, List<CardValues> values, PoofGameHub hub)
        {
            //TODO hub ertesítés hogy mit húztunk az adott eseményre.
            var firstCard = game.Deck.ElementAt(0);
            game.Deck.Remove(firstCard);
            game.DiscardPile.Add(firstCard);
            if (firstCard.Card.Suite == suit && (values is null || values.Contains(firstCard.Card.Value)))
                return Task.FromResult(true);
            return Task.FromResult(false);
        }

        public static List<GameCard> GetAndRemoveCards(this Game game, int count)
        {
            var result = game.GetCards(count);
            game.Deck.RemoveRange(0, count);
            return result;
        }

        public static Character GetCurrentCharacter(this Game game)
        {
            return game.GetCharacterById(game.CurrentUserId);
        }

        public static async Task AnswearCardAsync(this Game game, PoofGameHub hub, OptionDto dto)
        {
            await game.GetCurrentCharacter().Map(hub).CheckAnswearCardAsync(dto);
        }

        public static Task SetSingleReactAsync(this Game game, GameCard card, string userId, PoofGameHub hub)
        {
            //TODO: hub react
            game.Event = GameEvent.SingleReact;
            game.NextCard = card;
            game.NextUserId = userId;
            return Task.CompletedTask;
        }

        public static Task CallerSingleReactAsync(this Game game, PoofGameHub hub)
        {
            //TODO: hub react
            game.Event = game.Event == GameEvent.CallerReact ? GameEvent.SingleReact : GameEvent.CallerReact;
            return Task.CompletedTask;
        }

        public static Task CallerSingleReactLostAsync(this Game game)
        {
            //TODO: hub react
            game.Event = game.Event == GameEvent.CallerReact ? GameEvent.SingleReact : GameEvent.CallerReact;
            return Task.CompletedTask;
        }

        public static Task SetAllReactAsync(this Game game, string currentUserId, PoofGameHub hub, GameCard card, bool currentStart = false)
        {
            //hub
            if (currentStart) 
            {
                game.NextUserId = currentUserId;
            }
            else 
            {
                var index = game.Characters.IndexOf(game.Characters.SingleOrDefault(x => x.Id == currentUserId));
                if (index + 1 >= game.Characters.Count)
                {
                    index = 0;
                }
                else
                {
                    index++;
                }

                game.NextUserId = game.Characters.ElementAt(index).Id;
            }
            game.Event = GameEvent.AllReact;
            game.NextCard = card;
            return Task.CompletedTask;
        }

        public static async Task AllReactNextAsync(this Game game, PoofGameHub hub)
        {
            var index = game.Characters.IndexOf(game.GetCharacterById(game.NextUserId));
            if (index + 1 >= game.Characters.Count)
            {
                index = 0;
            }
            else
            {
                index++;
            }

            var userId = game.Characters.ElementAt(index).Id;
            if (userId == game.CurrentUserId)
            {
                await game.EndReactionAsync(hub);
            }
            else
            {
                game.NextUserId = userId;
            }
        }

        public static Task EndReactionAsync(this Game game, PoofGameHub hub)
        {
            game.Event = GameEvent.None;
            game.NextUserId = null;
            if (game.NextCard is not null)
            {
                game.DiscardPile.Add(game.NextCard);
                game.NextCard = null;
            }
            return Task.CompletedTask;
        }

        public static Character GetReactionCharacter(this Game game) => game.Event switch
        {
            GameEvent.AllReact => game.GetCharacterById(game.NextUserId),
            GameEvent.CallerReact => game.GetCharacterById(game.CurrentUserId),
            GameEvent.SingleReact => game.GetCharacterById(game.NextUserId),
            GameEvent.Draw => game.GetCharacterById(game.CurrentUserId),
            GameEvent.None => game.GetCharacterById(game.CurrentUserId),
            _ => null
        };

        public static Character GetNextCharacter(this Game game) 
        {
            var index = game.Characters.IndexOf(game.GetCurrentCharacter()) + 1;
            if (index >= game.Characters.Count)
                index = 0;
            return game.Characters.ElementAt(index);
        }

        public static List<string> Neigbours(this Game game, string playerId)
        {
            var playerCharacter = game.Characters.Where(x => x.Id == playerId).SingleOrDefault();

            if (playerCharacter is null)
            {
                //TODO: Exception
            }

            List<string> result = new List<string>();

            if (playerCharacter.AimDistance >= game.Characters.Count())
            {
                result.AddRange(game.Characters.Select(x => x.Id).ToList());
                return result;
            }

            var indexof = game.Characters.IndexOf(playerCharacter);
            var startpos = indexof - playerCharacter.AimDistance < 0 ? indexof - playerCharacter.AimDistance - game.Characters.Count : indexof - playerCharacter.AimDistance;
            var stoppos = indexof + playerCharacter.AimDistance >= game.Characters.Count ? indexof + playerCharacter.AimDistance - game.Characters.Count : indexof + playerCharacter.AimDistance;

            for (int i = startpos; i == stoppos; i++)
            {
                if (i >= game.Characters.Count)
                    i = i - game.Characters.Count;
                //Megnézni hogy a sima vagy a plusz listahossz a közelebbi érték if nagyobbak vagyunk az indexnél akkor + count ha nem akkor count - index
            }

            return result;
        }

        public static List<string> GetAllPlayer(this Game game)
        {
            return game.Characters.Select(x => x.Id).ToList();
        }
    }
}
