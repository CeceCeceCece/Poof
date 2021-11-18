using Application.Constants;
using Application.Exceptions;
using Application.Models.DTOs;
using Application.SignalR;
using Application.ViewModels;
using Domain.Constants.Enums;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.CharacterLogic
{
    public class BaseCharacterLogic
    {
        public Character Character { get; init; }
        public PoofGameHub Hub { get; init; }

        public BaseCharacterLogic(Character character, PoofGameHub hub)
        {
            this.Character = character;
            this.Hub = hub;
        }
        public virtual async Task DrawAsync() 
        {   
            await DrawAsync(Character.Game.GetAndRemoveCards(2));
            Character.Game.Event = GameEvent.None;

            //TODO: hub eretsites
            //return new Option
            //{
            //    Description = CharacterMessages.HUZOTT_LAPOK,
            //    NumberOfCards = 0,
            //    PossibleCards = cards.Select(x => new CardViewModel
            //    {
            //        Id = x.Id,
            //        Name = x.Card.Name
            //    })
            //    .ToList(),
            //    PossibleTargets = null,
            //    RequireAnswear = false,
            //    RequireCards = false
            //};
        }

        public Task DrawAsync(List<GameCard> cards) 
        {
            Character.Deck.AddRange(cards);
            //Hub
            return Task.CompletedTask;
        }

        public virtual Task<GameCard> LeaveCardAsync(string cardId)
        {
            var card = Character.Deck.SingleOrDefault(x => x.Id == cardId) ?? throw new PoofException(CharacterMessages.JATEKOS_ILYEN_LAPPAL_NEM_RENDELKEZIK);
            Character.Deck.Remove(card);
            //Hub
            return Task.FromResult(card);
        }
        public async Task<GameCard> LeaveCardRandomAsync()
        {
            var rand = new Random().Next(Character.Deck.Count);
            return await LeaveCardAsync(Character.Deck.ElementAt(rand).Id);
        }

        public virtual Task DropCardsFromDeckAsync(List<string> cardIds) 
        {
            var cards = Character.Deck.Where(x => cardIds.Contains(x.Id)).ToList();
            if (cards.Count != cardIds.Count)
                throw new PoofException(CharacterMessages.JATEKOS_ILYEN_LAPPAL_NEM_RENDELKEZIK);

            foreach (var card in cards)
            {
                Character.Deck.Remove(card);
            }
            //hub értesítés és betenni a kártyákat az eldobot pakliba
            Character.Game.DiscardPile.AddRange(cards);
            return Task.CompletedTask;
        }

        public virtual Task DrawReactAsync(OptionDto option) { return Task.CompletedTask; }

        public virtual async Task DecreaseLifepointAsync(int point)
        {
            Character.LifePoint -= point;
            if(Character.LifePoint <= 0) 
            {
                await DeadAsync();
            }
        }

        protected virtual Task DeadAsync()
        {
            return Task.CompletedTask;
        }

        public virtual Task IncreaseLifePontAsync(int point)
        {
            //TODO: hu ertesítés
            if (Character.LifePoint + point > Character.MaxLifePoint)
                Character.LifePoint = Character.MaxLifePoint;
            else
                Character.LifePoint += point;

            return Task.CompletedTask;
        }

        public virtual async Task<bool> TryHasCardAsync(string cardId, string cardName) 
        {
            var card = Character.Deck.SingleOrDefault(x => x.Id == cardId && x.Card.Name == cardName);
            if (card is null)
                return false;
            await DropCardAsync(cardId);
            return true;
        }

        public virtual async Task DropCardAsync(string cardId) 
        {
            //TODO: hub értesítés a kártya eldobásról.
            var deckCard = Character.Deck.SingleOrDefault(x => x.Id == cardId);
            var game = Character.Game;
            if(deckCard is not null) 
            {
                Character.Deck.Remove(deckCard);
                game.DiscardPile.Add(deckCard);
            }
            else if(Character.Weapon.Id == cardId) 
            {
                await Character.Weapon.Map().DeactivateAsync(this);
                game.DiscardPile.Add(Character.Weapon);
                Character.Weapon = null;
            }
            else 
            {
                var equipedCard = Character.EquipedCards.SingleOrDefault(x => x.Id == cardId) ?? throw new PoofException(CharacterMessages.JATEKOS_ILYEN_LAPPAL_NEM_RENDELKEZIK);
                await equipedCard.Map().DeactivateAsync(this);
                Character.EquipedCards.Remove(equipedCard);
                game.DiscardPile.Add(equipedCard);
            }
        }

        public virtual async Task EquipeCardAsync(string cardId) 
        {
            var card = Character.Deck.SingleOrDefault(x => x.Id == cardId) ?? throw new PoofException(CharacterMessages.JATEKOS_ILYEN_LAPPAL_NEM_RENDELKEZIK);
            Character.EquipedCards.Add(card);
            //hiba nem kell
            await LeaveCardAsync(cardId);
        }

        public virtual Task EquipeCardAsync(GameCard card)
        {
            Character.EquipedCards.Add(card);
            Character.Deck.Remove(card);
            return Task.CompletedTask;
        }

        public virtual async Task CardOptionAsync(string cardId) 
        {
            var card = Character.Deck.SingleOrDefault(x => x.Id == cardId) ?? throw new PoofException(CharacterMessages.JATEKOS_ILYEN_LAPPAL_NEM_RENDELKEZIK);
            await card.Map().OptionAsync(this);
        }

        public virtual async Task ActivateCardAsync(string cardId, OptionDto option)
        {
            var card = Character.Deck.SingleOrDefault(x => x.Id == cardId);
            if (card is null)
                throw new PoofException(CharacterMessages.JATEKOS_ILYEN_LAPPAL_NEM_RENDELKEZIK);
            var logic = card.Map();
            await logic.ActivateAsync(this, option);
        }
        public virtual async Task CheckAnswearCardAsync(OptionDto dto) 
        {
            await Character.Game.NextCard.Map().AnswearAsync(this, dto);
        }
        
        public virtual async Task AnswearCardAsync(OptionDto dto) 
        {
            await Character.Game.AnswearCardAsync(Hub, dto);
        }

        public virtual List<string> GetNeighbours() 
        {
            return Character.Game.Neigbours(Character.Id);
        }

        public virtual async Task GetCardFromGameAsync(string cardId)
        {
            await DrawAsync(new List<GameCard> {Character.Game.GetCard(cardId)});
        }
    }
}
