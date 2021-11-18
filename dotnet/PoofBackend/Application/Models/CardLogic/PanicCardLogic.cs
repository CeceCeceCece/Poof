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

        public override Task OptionAsync(BaseCharacterLogic character)
        {
            //Hub értesítani hogy kinek tudja elvenni a kártyáját.
            return Task.CompletedTask;
        }

        public override async Task ActivateAsync(BaseCharacterLogic character, OptionDto dto)
        {
            var target = character.Character.Game.GetCharacterById(dto.UserId).Map(character.Hub);
            var card = await target.LeaveCardAsync(dto.CardIds.First());
            await character.DrawAsync(new List<GameCard> { card });
        }
    }
}
