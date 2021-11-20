using Application.Constants;
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
    public class CatBalouCardLogic : CardLogic
    {
        public CatBalouCardLogic(GameCard card) : base(card)
        {
        }

        public override async Task OptionAsync(BaseCharacterLogic character)
        {
            var option =  new OptionViewModel
            {
                Description = CardMessages.CHOOSE_ONE_CARD,
                RequireAnswear = true,
                RequireCards = true,
                PossibleTargets = character.Character.Game.GetOtherCharacters(),
                PossibleCards = null,
                NumberOfCards = 1
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
