using Application.Constants;
using Application.Exceptions;
using Application.Models.DTOs;
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

        public CharacterLogic(Character character)
        {
            this.character = character;
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
            character.Deck.Remove(card);
        }

        public void IncreaseLifePont(int point)
        {
            if (character.LifePoint + point > character.MaxLifePoint)
                character.LifePoint = character.MaxLifePoint;
            else
                character.LifePoint += point;
        }

        public void DropCard(string cardId, Game game) 
        {
            var deckCard = character.Deck.SingleOrDefault(x => x.Id == cardId);
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
    }
}
