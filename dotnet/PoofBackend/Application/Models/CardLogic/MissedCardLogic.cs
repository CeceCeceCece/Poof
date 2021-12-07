using Application.Constants;
using Application.Exceptions;
using Application.Models.CharacterLogic;
using Application.Models.DTOs;
using Application.ViewModels;
using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Models.CardLogic
{
    public class MissedCardLogic : CardLogic
    {
        public MissedCardLogic(GameCard card) : base(card)
        {
        }

        public override async Task OptionAsync(BaseCharacterLogic character)
        {   
            var option = new OptionViewModel
            {
                Description = CardMessages.MISSED_OPTION,
                RequireCards = false,
                PossibleTargets = new List<string>()
            };
            await character.ShowOptionAsync(option);

        }

        public override Task ActivateAsync(BaseCharacterLogic character, OptionDto dto)
        {
            throw new PoofException(CardMessages.MISSED_ERROR);
        }
    }
}
