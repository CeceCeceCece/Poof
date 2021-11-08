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
    public class PedroRamirezCharacter : CharacterLogic
    {
        public PedroRamirezCharacter(Character character) : base(character) { }

        public override Option Draw(Game game)
        {
            if (game.DiscardPile.Count <= 0)
            {
                return base.Draw(game);
            }

            if (character is null)
                throw new PoofException(CharacterMessages.JATEKOS_NEM_A_JATEK_RESZE);

            game.Event = GameEvent.Draw;

            return new Option
            {
                Description = CharacterMessages.A_PAKLIBOL_VAGY_AZ_ELDOBOTT_LAPOK_KOZUL,
                NumberOfCards = 0,
                PossibleCards = null,
                PossibleTargets = new List<string> { character.Id },
                RequireAnswear = true,
                RequireCards = false,
            };
        }

        public override void DrawReact(Game game, OptionDto option)
        {
            List<GameCard> cards = new List<GameCard>();
            if (option.UserId == null) 
            {
                cards.AddRange(game.Deck.Take(2));
                game.Deck.RemoveRange(0, 2);
            }
            else 
            {
                cards.AddRange(game.GetAndRemoveCards(1));
                var discardCard = game.DiscardPile.Last();
                cards.Add(discardCard);
                game.DiscardPile.Remove(discardCard);
            }
            character.Deck.AddRange(cards);
            game.Event = GameEvent.None;
        }
    }
}
