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

        public override Task OptionAsync(BaseCharacterLogic character)
        {
            return Task.CompletedTask;
            //TODO: értasíteni hogy küldjön egy kártyát.
            //return new Option
            //{
            //    Description = CardMessages.CHOOSE_ONE_CARD,
            //    RequireAnswear = true,
            //    RequireCards = true,
            //    PossibleTargets = game.GetAllPlayer(),
            //    PossibleCards = null,
            //    NumberOfCards = 1
            //};
        }

        public override async Task ActivateAsync(BaseCharacterLogic character, OptionDto dto)
        {
            var target = character.Character.Game.GetCharacterById(dto.UserId).Map(character.Hub);
            await target.DropCardAsync(dto.CardIds.First());
            await character.DropCardAsync(Card.Id);
        }
    }
}
