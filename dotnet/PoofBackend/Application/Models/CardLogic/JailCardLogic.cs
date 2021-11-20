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
    public class JailCardLogic : CardLogic
    {
        public JailCardLogic(GameCard card) : base(card)
        {
        }

        public override async Task OptionAsync(BaseCharacterLogic character)
        {
            var option = new OptionViewModel
            {
                Description = CardMessages.CHOOSE_ONE_PLAYER,
                RequireAnswear = true,
                RequireCards = false,
                PossibleTargets = character.Character.Game.GetOtherCharacters(),
                PossibleCards = null,
                NumberOfCards = 0
            };

            await character.ShowOptionAsync(option);
        }

        public override async Task ActivateAsync(BaseCharacterLogic character, OptionDto dto)
        {
            var target = character.Character.Game.GetCharacterById(dto.UserId).Map(character.Hub);
            await target.EquipeCardAsync(Card);
            await character.LeaveCardAsync(Card.Id);
        }
        public override Task OnActiveAsync(BaseCharacterLogic character)
        {
            //TODO kör vége.
            return base.OnActiveAsync(character);
        }
    }
}
