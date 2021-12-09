using Application.Constants;
using Application.Models.CharacterLogic;
using Application.Models.DTOs;
using Application.ViewModels;
using Domain.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Models.CardLogic
{
    public class CatBalouCardLogic : CardLogic
    {
        public CatBalouCardLogic(GameCard card) : base(card)
        {
        }

        public override async Task OptionAsync(BaseCharacterLogic character)
        {
            var option = new OptionViewModel
            {
                Description = CardMessages.CHOOSE_ONE_CARD,
                RequireCards = true,
                PossibleTargets = character.Character.Game.GetAllPlayer()
            };

            await character.ShowOptionAsync(option);
        }

        public override async Task ActivateAsync(BaseCharacterLogic character, OptionDto dto)
        {
            var target = character.Character.Game.GetCharacterById(dto.UserId).Map(character.Hub);
            await target.DropCardAsync(dto.CardIds.First());
            await character.DropCardAsync(Card.Id);
        }
    }
}
