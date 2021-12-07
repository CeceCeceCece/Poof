using Application.Constants;
using Application.Exceptions;
using Application.Models.DTOs;
using Application.SignalR;
using Application.ViewModels;
using Domain.Constants.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public static async Task<bool> CheckNextCardAsync(this Game game, CardSuits suit, List<CardValues> values, PoofGameHub hub)
        {
            var firstCard = game.Deck.ElementAt(0);
            game.Deck.Remove(firstCard);
            game.DiscardPile.Add(firstCard);

            //Értesítés hogy milyen kártyát húztunk. (pl, Dinamit vagy börtön)
            if (hub is not null)
                await hub.Clients.Group(game.Name).ShowCard(new CardViewModel(firstCard.Id, firstCard.Card.Name, firstCard.Card.Type, firstCard.Card.Suite, firstCard.Card.Value));

            if (firstCard.Card.Suite == suit && (values is null || values.Contains(firstCard.Card.Value)))
                return true;
            return false;
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

        public static async Task SetSingleReactAsync(this Game game, GameCard card, string userId, PoofGameHub hub)
        {
            game.Event = GameEvent.SingleReact;
            game.NextCard = card;
            game.NextUserId = userId;

            //Értesíteni hogy válaszolni kell
            if(hub is not null)
                await hub.Clients.Group(game.Name).SetGameEvent(new GameEventViewModel(game.Event, userId, card == null ? null : new CardViewModel(card.Id, card.Card.Name, card.Card.Type, card.Card.Suite, card.Card.Value)));

            var nextCharacter = game.GetReactionCharacter();
            foreach (var equipedCard in nextCharacter.EquipedCards)
            {
                await equipedCard.Map().OnActiveAsync(nextCharacter.Map(hub));
            }
        }

        public static async Task CallerSingleReactAsync(this Game game, PoofGameHub hub)
        {
            game.Event = game.Event == GameEvent.CallerReact ? GameEvent.SingleReact : GameEvent.CallerReact;
            //Értesíteni hogy válaszolni kell
            if (hub is not null)
                await hub.Clients.Group(game.Name).SetGameEvent(new GameEventViewModel(game.Event, game.Event == GameEvent.CallerReact ? game.CurrentUserId : game.NextUserId, game.NextCard == null ? null : new CardViewModel(game.NextCard.Id, game.NextCard.Card.Name, game.NextCard.Card.Type, game.NextCard.Card.Suite, game.NextCard.Card.Value)));
        }

        public static async Task SetAllReactAsync(this Game game, string currentUserId, PoofGameHub hub, GameCard card, bool currentStart = false)
        {
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

            //Értesíteni hogy válaszolni kell
            if (hub is not null)
                await hub.Clients.Group(game.Name).SetGameEvent(new GameEventViewModel(game.Event, game.NextUserId, game.NextCard == null ? null : new CardViewModel(game.NextCard.Id, game.NextCard.Card.Name, game.NextCard.Card.Type, game.NextCard.Card.Suite, game.NextCard.Card.Value)));
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

                //Értesíteni hogy válaszolni kell
                if (hub is not null)
                    await hub.Clients.Group(game.Name).SetGameEvent(new GameEventViewModel(game.Event, game.NextUserId, game.NextCard == null ? null : new CardViewModel(game.NextCard.Id, game.NextCard.Card.Name, game.NextCard.Card.Type, game.NextCard.Card.Suite, game.NextCard.Card.Value)));
            }
        }

        public static async Task EndReactionAsync(this Game game, PoofGameHub hub)
        {
            game.Event = GameEvent.None;
            game.NextUserId = null;
            if (game.NextCard is not null)
            {
                game.DiscardPile.Add(game.NextCard);
                game.NextCard = null;
            }

            //Értesíteni hogy válaszolni kell
            if (hub is not null)
                await hub.Clients.Group(game.Name).SetGameEvent(new GameEventViewModel(game.Event, game.CurrentUserId, null));
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

        public static List<string> Neigbours(this Game game, bool withWeapon)
        {
            var character = game.GetCurrentCharacter();

            int indx = game.Characters.IndexOf(character);
            int count = game.Characters.Count;
            int weaponCount = withWeapon ? character.WeaponDistance : 1;

            List<string> result = new List<string>();

            for (int i = 0; i < count; i++) 
            {
                if(i != indx) 
                {
                    int next;
                    int previous;
                    if(i < indx) 
                    {
                        next = indx - i;
                        previous = count - indx + i;
                    }
                    else 
                    {
                        next = i - indx;
                        previous = count - i + indx;
                    }

                    int min = next < previous ? next : previous;

                    var target = game.Characters.ElementAt(i);

                    min += target.DistanceFromOthers - 1;

                    if(min <= weaponCount + character.AimDistance) 
                    {
                        result.Add(target.Id);
                    }
                }
            }

            return result;
        }

        public static List<string> GetAllPlayer(this Game game)
        {
            return game.Characters.Select(x => x.Id).ToList();
        }

        public static List<string> GetOtherCharacters(this Game game)
        {
            return game.Characters.Where(x => x.Id != game.CurrentUserId).Select(x => x.Id).ToList();
        }

        public static async Task ShowRactOptionAsync(this Game game, OptionViewModel option, PoofGameHub hub)
        {
            if(hub != null)
                await hub.Clients.Client(game.GetReactionCharacter().ConnectionId).ShowOption(option);
        }

        public static async Task CheckWinAsync(this Game game, PoofGameHub hub) 
        {
            var winLogic = game.Win.GetWinLogic();
            if(await winLogic.CheckWinAsync(game, out var winner)) 
            {
                if (hub is not null)
                    await hub.Clients.Group(game.Name).WinnerIs(winner);

                game.Win = WinType.None;
            }
        }

        public static async Task AddToDiscardPileAsync(this Game game, PoofGameHub hub, GameCard card)
        {
            game.DiscardPile.Add(card);
            if(hub is not null)
                await hub.Clients.Group(game.Name).SetDiscardPile(new CardViewModel(card.Id, card.Card.Name, card.Card.Type, card.Card.Suite, card.Card.Value));
        }

        public static async Task EndTurnAsync(this Game game, PoofGameHub hub)
        {
            var current = game.GetCurrentCharacter();
            if (current.Deck.Count > current.LifePoint)
                throw new PoofException(GameMessages.KOR_VEGE_NEM_LEHETSEGES);
            await game.EndReactionAsync(hub);

            var next = game.GetNextCharacter().Map(hub);
            game.CurrentUserId = next.Character.Id;
            foreach (var card in next.Character.EquipedCards)
            {
                await card.Map().OnActiveAsync(next);
            }
            if(game.CurrentUserId == next.Character.Id) 
            {
                //await hub.Clients.Group(game.Name).SetGameEvent(new GameEventViewModel(GameEvent.None, game.CurrentUserId, null));
                await next.DrawAsync();
            }
                
        }
    }
}
