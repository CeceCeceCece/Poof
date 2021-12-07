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
            Character.Game.Event = GameEvent.Draw;
            await Hub.Clients.Client(Character.ConnectionId).DrawOption(new DrawOptionViewModel(false, new List<CardViewModel>()));
        }

        public async Task DrawAsync(List<GameCard> cards) 
        {
            Character.Deck.AddRange(cards);

            if(Hub is not null) 
            {
                //Értesítés mindenkinek hogy hány lapot kapott
                await Hub.Clients.Group(Character.Game.Name).CardsAdded(
                        cards.Select(x => new CardIdViewModel(x.Id, Character.Id)).ToList()
                    );

                //Értesítés hogy milyen lapokat kapott
                await Hub.Clients.Client(Character.ConnectionId).CardsReceieved(
                        cards.Select(x => new CardViewModel(x.Id, x.Card.Name, x.Card.Type, x.Card.Suite, x.Card.Value)).ToList()
                    );
            }
               
        }

        public virtual async Task<GameCard> LeaveCardAsync(string cardId, bool inEquiped = false)
        {
            if (inEquiped)
            {
                var equiped = Character.EquipedCards.SingleOrDefault(x => x.Id == cardId);
                if(equiped is not null) 
                {
                    await equiped.Map().DeactivateAsync(this);
                    Character.EquipedCards.Remove(equiped);

                    if (Hub is not null)
                        await Hub.Clients.Group(Character.Game.Name).CardUnequiped(new CardIdViewModel(cardId, Character.Id));

                    return equiped;
                }
                else if(Character.Weapon is not null && Character.Weapon.Id == cardId) 
                {
                    await Character .Weapon.Map().DeactivateAsync(this);
                    var weapon = Character.Weapon;
                    Character.Weapon = null;

                    if (Hub is not null)
                        await Hub.Clients.Group(Character.Game.Name).SetWeapon(Character.Id, null);
                    
                    return weapon;
                }
            }
            var card = Character.Deck.SingleOrDefault(x => x.Id == cardId) ?? throw new PoofException(CharacterMessages.JATEKOS_ILYEN_LAPPAL_NEM_RENDELKEZIK);
            Character.Deck.Remove(card);

            if (Hub is not null)
                await Hub.Clients.Group(Character.Game.Name).CardsDroped(new List<CardIdViewModel> { new CardIdViewModel(cardId, Character.Id) });

            return card;
        }
        public async Task<GameCard> LeaveCardRandomAsync()
        {
            var rand = new Random().Next(Character.Deck.Count);
            return await LeaveCardAsync(Character.Deck.ElementAt(rand).Id);
        }

        public virtual async Task DropCardsFromDeckAsync(List<string> cardIds) 
        {
            var cards = Character.Deck.Where(x => cardIds.Contains(x.Id)).ToList();
            if (cards.Count != cardIds.Count)
                throw new PoofException(CharacterMessages.JATEKOS_ILYEN_LAPPAL_NEM_RENDELKEZIK);

            foreach (var card in cards)
            {
                Character.Deck.Remove(card);
                if (Hub is not null)
                    await Hub.Clients.Group(Character.Game.Name).CardsDroped(new List<CardIdViewModel> { new CardIdViewModel(card.Id, Character.Id) });
            }

            Character.Game.DiscardPile.AddRange(cards);

            if (Hub is not null) 
            {
                var last = Character.Game.DiscardPile.Last();
                await Hub.Clients.Group(Character.Game.Name).SetDiscardPile(new CardViewModel(last.Id, last.Card.Name, last.Card.Type, last.Card.Suite, last.Card.Value));
            }
          
        }

        public virtual async Task DrawReactAsync(OptionDto option) 
        {
            await DrawAsync(Character.Game.GetAndRemoveCards(2));
            Character.Game.Event = GameEvent.None;
        }

        public virtual async Task DecreaseLifepointAsync(int point)
        {
            Character.LifePoint -= point;
            if(Character.LifePoint <= 0) 
            {
                await DeadAsync();
            }

            if (Hub is not null)
                await Hub.Clients.Group(Character.Game.Name).SetLifePoint(new LifePointViewModel(Character.Id, Character.LifePoint));
        }

        protected virtual async Task DeadAsync()
        {
            foreach (var card in Character.Deck)
            {
                await Character.Game.AddToDiscardPileAsync(Hub, card);
            }
            foreach (var card in Character.EquipedCards)
            {
                await Character.Game.AddToDiscardPileAsync(Hub, card);
            }
            if (Character.Weapon is not null)
                await Character.Game.AddToDiscardPileAsync(Hub, Character.Weapon);

            Character.Deck.Clear();
            Character.EquipedCards.Clear();
            Character.Weapon = null;

            if (Hub is not null)
                await Hub.Clients.Group(Character.Game.Name).PlayerDied(new CharacterDiedViewModel(Character.Id, Character.Role));

            Character.Game.Characters.Remove(Character);
            Character.Game = null;

            await Character.Game.CheckWinAsync(Hub);
        }

        public virtual async Task IncreaseLifePontAsync(int point)
        {
            if (Character.LifePoint + point > Character.MaxLifePoint)
                Character.LifePoint = Character.MaxLifePoint;
            else
                Character.LifePoint += point;

            if (Hub is not null)
                await Hub.Clients.Group(Character.Game.Name).SetLifePoint(new LifePointViewModel(Character.Id, Character.LifePoint));
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
            var deckCard = Character.Deck.SingleOrDefault(x => x.Id == cardId);
            var game = Character.Game;
            if(deckCard is not null) 
            {
                Character.Deck.Remove(deckCard);
                await game.AddToDiscardPileAsync(Hub, deckCard);

                if (Hub is not null)
                    await Hub.Clients.Group(Character.Game.Name).CardsDroped(new List<CardIdViewModel> { new CardIdViewModel(cardId, Character.Id) });
            }
            else if(Character.Weapon != null && Character.Weapon.Id == cardId) 
            {
                await Character.Weapon.Map().DeactivateAsync(this);
                await game.AddToDiscardPileAsync(Hub, Character.Weapon);
                Character.Weapon = null;
                if (Hub is not null) 
                {
                    await Hub.Clients.Group(Character.Game.Name).CardsDroped(new List<CardIdViewModel> { new CardIdViewModel(cardId, Character.Id) });
                }
                    
            }
            else 
            {
                var equipedCard = Character.EquipedCards.SingleOrDefault(x => x.Id == cardId) ?? throw new PoofException(CharacterMessages.JATEKOS_ILYEN_LAPPAL_NEM_RENDELKEZIK);
                await equipedCard.Map().DeactivateAsync(this);
                Character.EquipedCards.Remove(equipedCard);
                await game.AddToDiscardPileAsync(Hub, equipedCard);

                if (Hub is not null)
                    await Hub.Clients.Group(Character.Game.Name).CardUnequiped(new CardIdViewModel(cardId, Character.Id));
            }
        }

        public virtual async Task UnequipeCard(string cardId) 
        {
            var equipedCard = Character.EquipedCards.SingleOrDefault(x => x.Id == cardId) ?? throw new PoofException(CharacterMessages.JATEKOS_ILYEN_LAPPAL_NEM_RENDELKEZIK);
            await equipedCard.Map().DeactivateAsync(this);
            Character.EquipedCards.Remove(equipedCard);

            if (Hub is not null)
                await Hub.Clients.Group(Character.Name).CardUnequiped(new CardIdViewModel(cardId, Character.Id));
        }

        public virtual async Task EquipeCardAsync(string cardId) 
        {
            var card = Character.Deck.SingleOrDefault(x => x.Id == cardId) ?? throw new PoofException(CharacterMessages.JATEKOS_ILYEN_LAPPAL_NEM_RENDELKEZIK);
            
            if(Character.EquipedCards.Any(x => x.Card.Name == card.Card.Name))
                throw new PoofException(CharacterMessages.ILYEN_LAPPAL_MAR_RENDELKEZIK);
            Character.EquipedCards.Add(card);
            await LeaveCardAsync(cardId);

            if (Hub is not null)
                await Hub.Clients.Group(Character.Game.Name).CardEquiped(Character.Id, new CardViewModel(card.Id, card.Card.Name, card.Card.Type, card.Card.Suite, card.Card.Value));
        }

        public virtual async Task EquipeWeaponAsync(string cardId)
        {
            var card = Character.Deck.SingleOrDefault(x => x.Id == cardId) ?? throw new PoofException(CharacterMessages.JATEKOS_ILYEN_LAPPAL_NEM_RENDELKEZIK);
            if (Character.Weapon is not null)
                await DropCardAsync(Character.Weapon.Id);
            Character.Weapon = card;
            await LeaveCardAsync(cardId);
            if (Hub is not null)
                await Hub.Clients.Group(Character.Game.Name).SetWeapon(Character.Id, new CardViewModel(card.Id, card.Card.Name, card.Card.Type, card.Card.Suite, card.Card.Value));
        }

        public virtual async Task EquipeCardAsync(GameCard card)
        {
            if(card is null) 
                throw new PoofException(CharacterMessages.JATEKOS_ILYEN_LAPPAL_NEM_RENDELKEZIK);
            Character.EquipedCards.Add(card);

            if (Hub is not null)
                await Hub.Clients.Group(Character.Game.Name).CardEquiped(Character.Id, new CardViewModel(card.Id, card.Card.Name, card.Card.Type, card.Card.Suite, card.Card.Value));
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

        public virtual List<string> GetNeighbours(bool withWeapon) 
        {
            return Character.Game.Neigbours(withWeapon);
        }

        public virtual async Task GetCardFromGameAsync(string cardId)
        {
            await DrawAsync(new List<GameCard> {Character.Game.GetCard(cardId)});
        }

        public virtual async Task ShowOptionAsync(OptionViewModel option) 
        {
            if (Hub != null)
                await Hub.Clients.Client(Character.ConnectionId).ShowOption(option);
        }
    }
}
