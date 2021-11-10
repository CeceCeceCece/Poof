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
    public class CharacterLogic
    {
        protected readonly Character character;
        protected readonly PoofGameHub hub;

        public CharacterLogic(Character character, PoofGameHub hub)
        {
            this.character = character;
            this.hub = hub;
        }
        public virtual Option Draw(Game game) 
        {
            if (character is null)
                throw new PoofException(CharacterMessages.JATEKOS_NEM_A_JATEK_RESZE);

            var cards = game.GetAndRemoveCards(2);
            character.Deck.AddRange(cards);
            game.Event = GameEvent.None;

            return new Option
            {
                Description = CharacterMessages.HUZOTT_LAPOK,
                NumberOfCards = 0,
                PossibleCards = cards.Select(x => new CardViewModel
                {
                    Id = x.Id,
                    Name = x.Card.Name
                })
                .ToList(),
                PossibleTargets = null,
                RequireAnswear = false,
                RequireCards = false
            };
        }

        public virtual void DrawReact(Game game, OptionDto option) { }

        public virtual void ActivateCard(Game game, string cardId, OptionDto option) 
        {
            var card = character.Deck.SingleOrDefault(x => x.Id == cardId);
            if (card is null)
                throw new PoofException(CharacterMessages.JATEKOS_ILYEN_LAPPAL_NEM_RENDELKEZIK);
            var logic = card.Map();
            logic.Activate(game, option);
            //TODO:Megvizsgálni hogy kell e ez a törlés.
            character.Deck.Remove(card);
        }

        internal void DecreaseLifepoint(int point)
        {
            character.LifePoint -= 1;
            if(character.LifePoint <= 0) 
            {
                Dead();
            }
        }

        private void Dead()
        {
            throw new NotImplementedException();
        }

        public void IncreaseLifePont(int point)
        {
            if (character.LifePoint + point > character.MaxLifePoint)
                character.LifePoint = character.MaxLifePoint;
            else
                character.LifePoint += point;
        }

        public bool TryHasCard(string cardId, string cardName) 
        {
            var card = character.Deck.SingleOrDefault(x => x.Id == cardId && x.Card.Name == cardName);
            if (card is null)
                return false;
            DropCard(cardId);
            return true;
        }

        public void DropCard(string cardId) 
        {
            var deckCard = character.Deck.SingleOrDefault(x => x.Id == cardId);
            var game = character.Game;
            if(deckCard is not null) 
            {
                character.Deck.Remove(deckCard);
                game.DiscardPile.Add(deckCard);
            }
            else if(character.Weapon.Id == cardId) 
            {   
                game.DiscardPile.Add(character.Weapon);
                character.Weapon = null;
            }
            else 
            {
                var equipedCard = character.EquipedCards.SingleOrDefault(x => x.Id == cardId) ?? throw new PoofException(CharacterMessages.JATEKOS_ILYEN_LAPPAL_NEM_RENDELKEZIK);
                equipedCard.Map().Deactivate(character);
                character.EquipedCards.Remove(equipedCard);
                game.DiscardPile.Add(equipedCard);
            }
        }

        public void EquipeCard(string cardId) 
        {
            var card = character.Deck.SingleOrDefault(x => x.Id == cardId) ?? throw new PoofException(CharacterMessages.JATEKOS_ILYEN_LAPPAL_NEM_RENDELKEZIK);
            character.EquipedCards.Add(card);
            DropCard(cardId);
        }
    }
}
