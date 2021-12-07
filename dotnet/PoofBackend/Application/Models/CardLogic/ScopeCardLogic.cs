using Application.Constants;
using Application.Models.CharacterLogic;
using Application.Models.DTOs;
using Application.ViewModels;
using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Models.CardLogic
{
    public class ScopeCardLogic : CardLogic
    {
        public ScopeCardLogic(GameCard card) : base(card)
        {
        }

        public override async Task OptionAsync(BaseCharacterLogic character)
        {
            await character.ActivateCardAsync(Card.Id, null);

            var option = new OptionViewModel
            {
                Description = CardMessages.SALOON_OPTION,
                RequireCards = false,
                PossibleTargets = new List<string> { character.Character.Id }
            };

            await character.ShowOptionAsync(option);
        }

        public override async Task ActivateAsync(BaseCharacterLogic character, OptionDto dto)
        {
            character.Character.AimDistance += 1;
            await character.EquipeCardAsync(Card.Id);
        }

        public override Task DeactivateAsync(BaseCharacterLogic character)
        {
            character.Character.AimDistance -= 1;
            return Task.CompletedTask;
        }
    }
}
