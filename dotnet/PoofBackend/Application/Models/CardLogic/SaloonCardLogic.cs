using Application.Constants;
using Application.Models.CharacterLogic;
using Application.Models.DTOs;
using Application.ViewModels;
using Domain.Entities;
using System.Threading.Tasks;

namespace Application.Models.CardLogic
{
    public class SaloonCardLogic : CardLogic
    {
        public SaloonCardLogic(GameCard card) : base(card)
        {
        }

        public override async Task OptionAsync(BaseCharacterLogic character)
        {
            await character.ActivateCardAsync(Card.Id, null);

            var option = new OptionViewModel
            {
                Description = CardMessages.SALOON_OPTION,
                RequireAnswear = false,
                RequireCards = false,
                PossibleTargets = null,
                PossibleCards = null
            };

            await character.ShowOptionAsync(option);
        }

        public override async Task ActivateAsync(BaseCharacterLogic character, OptionDto dto)
        {
            foreach(var target in character.Character.Game.Characters) 
            {
                await target.Map(character.Hub).IncreaseLifePontAsync(1);
            }
            await character.DropCardAsync(Card.Id);
        }
    }
}
