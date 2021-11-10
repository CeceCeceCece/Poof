using Application.Constants;
using Application.Exceptions;
using Application.Models.DTOs;
using Application.ViewModels;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.CardLogic
{
    public class MissedCardLogic : CardLogic
    {
        public MissedCardLogic(GameCard card) : base(card)
        {
        }

        public override Option Option(string playerId, Game game)
        {
            return new Option
            {
                Description = CardMessages.KARTYA_NEM_KIJATSZATO,
                RequireAnswear = false,
                RequireCards = false,
                PossibleTargets = null,
                PossibleCards = null
            };
        }

        public override void Activate(Game game, OptionDto dto)
        {
            throw new PoofException(CardMessages.KARTYA_NEM_KIJATSZATO);
        }
    }
}
