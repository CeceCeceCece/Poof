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

            //TODO ha lapok elfogynak
            var cards = game.Deck.Take(2);
            game.Deck.RemoveRange(0,2);
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
    }
}
