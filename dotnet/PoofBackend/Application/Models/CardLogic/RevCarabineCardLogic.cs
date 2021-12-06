using Application.Constants;
using Application.Models.CharacterLogic;
using Application.Models.DTOs;
using Application.ViewModels;
using Domain.Entities;
using System;
using System.Threading.Tasks;

namespace Application.Models.CardLogic
{
    public class RevCarabineCardLogic : CardLogic
    {
        public RevCarabineCardLogic(GameCard card) : base(card)
        {
        }

        public override async Task OptionAsync(BaseCharacterLogic character)
        {
            var option = new OptionViewModel
            {
                Description = CardMessages.WEAPONS_OPTION,
                RequireAnswear = false,
                RequireCards = false,
                PossibleTargets = null,
                PossibleCards = null
            };

            await character.ShowOptionAsync(option);
        }

        public override async Task ActivateAsync(BaseCharacterLogic character, OptionDto dto)
        {
            await character.EquipeWeaponAsync(Card.Id);
            character.Character.WeaponDistance = 4;
        }

        public override Task DeactivateAsync(BaseCharacterLogic character)
        {
            character.Character.WeaponDistance = 1;
            return Task.CompletedTask;
        }
    }
}
