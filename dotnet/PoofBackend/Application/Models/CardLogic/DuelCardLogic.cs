using Application.Constants;
using Application.Exceptions;
using Application.Models.CharacterLogic;
using Application.Models.DTOs;
using Application.ViewModels;
using Domain.Constants.Enums;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.CardLogic
{
    public class DuelCardLogic : CardLogic
    {
        public DuelCardLogic(GameCard card) : base(card)
        {
        }

        public override async Task OptionAsync(BaseCharacterLogic character)
        {
            var option = new OptionViewModel
            {
                Description = CardMessages.DUEL_OPTION,
                RequireAnswear = true,
                RequireCards = false,
                PossibleTargets = character.Character.Game.GetOtherCharacters(),
                PossibleCards = null
            };
            await character.ShowOptionAsync(option);
        }

        public override async Task ActivateAsync(BaseCharacterLogic character, OptionDto dto)
        {
            if (string.IsNullOrEmpty(dto.UserId))
                throw new PoofException(CharacterMessages.NEM_MEGFELELO_JATEKOS_AZONOSITO);
            var target = character.Character.Game.GetCharacterById(dto.UserId);
            await character.LeaveCardAsync(Card.Id);

            await character.Character.Game.SetSingleReactAsync(Card, dto.UserId, character.Hub);
        }

        public override async Task AnswearAsync(BaseCharacterLogic character, OptionDto dto)
        {
            var target = character.Character.Game.GetReactionCharacter().Map(character.Hub);
            if (dto.CardIds != null && await target.TryHasCardAsync(dto.CardIds.First(), "Bang!"))
            {
                await character.Character.Game.CallerSingleReactAsync(character.Hub);
            }
            else
            {
                await target.DecreaseLifepointAsync(1);
                await character.Character.Game.EndReactionAsync(character.Hub);
            }
        }
    }
}
