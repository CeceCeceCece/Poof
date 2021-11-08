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
    public class KitCarlsonCharacter : CharacterLogic
    {
        public KitCarlsonCharacter(Character character) : base(character) { }

        public override Option Draw(Game game)
        {
            if (character is null)
                throw new PoofException(CharacterMessages.JATEKOS_NEM_A_JATEK_RESZE);

            game.Event = GameEvent.Draw;

            return new Option
            {
                Description = CardMessages.CHOOSE_ONE_PLAYER,
                NumberOfCards = 2,
                PossibleCards = game.GetCards(3)
                    .Select(x => new CardViewModel 
                    {
                        Id = x.Id,
                        Name = x.Card.Name
                    })
                    .ToList(),
                PossibleTargets = null,
                RequireAnswear = true,
                RequireCards = true,
            };
        }

        public override void DrawReact(Game game, OptionDto option)
        {
            if (option.CardIds.Count != 2)
                throw new PoofException(CharacterMessages.NEM_MEGFELELO_HUZAS);

            var cards = game.Deck.Where(x => option.CardIds.Contains(x.Id)).ToList();
            foreach (var card in cards)
            {
                game.Deck.Remove(card);
            }
            game.Event = GameEvent.None;

            character.Deck.AddRange(cards);
        }
    }
}
