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
    public class PanicCardLogic : CardLogic
    {
        public PanicCardLogic(GameCard card) : base(card)
        {
        }

        public override async Task OptionAsync(BaseCharacterLogic character)
        {
            var option = new OptionViewModel
            {
                Description = CardMessages.PANIC_OPTION,
                RequireAnswear = true,
                RequireCards = false,
                PossibleTargets = character.GetNeighbours(),
                PossibleCards = null
            };
            await character.ShowOptionAsync(option);
        }

        public override async Task ActivateAsync(BaseCharacterLogic character, OptionDto dto)
        {
            var target = character.Character.Game.GetCharacterById(dto.UserId).Map(character.Hub);
            var card = await target.LeaveCardAsync(dto.CardIds.First(), true);
            await character.DrawAsync(new List<GameCard> { card });
            await character.DropCardAsync(Card.Id);
        }
    }
}
