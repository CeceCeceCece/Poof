using Application.Constants;
using Application.Exceptions;
using Application.Models.CharacterLogic;
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

        public override Task OptionAsync(BaseCharacterLogic character)
        {
            return Task.CompletedTask;
            //Semmi nem torténjen
            //return new Option
            //{
            //    Description = CardMessages.KARTYA_NEM_KIJATSZATO,
            //    RequireAnswear = false,
            //    RequireCards = false,
            //    PossibleTargets = null,
            //    PossibleCards = null
            //};
        }

        public override Task ActivateAsync(BaseCharacterLogic character, OptionDto dto)
        {
            throw new PoofException(CardMessages.KARTYA_NEM_KIJATSZATO);
        }
    }
}
