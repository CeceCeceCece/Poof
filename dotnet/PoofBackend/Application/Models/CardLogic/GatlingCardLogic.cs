using Application.Constants;
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
    public class GatlingCardLogic : CardLogic
    {
        public GatlingCardLogic(GameCard card) : base(card)
        {
        }

        public override async Task OptionAsync(BaseCharacterLogic character)
        {
            var option = new OptionViewModel
            {
                Description = CardMessages.GATLING_OPTION,
                RequireAnswear = false,
                RequireCards = false,
                PossibleTargets = null,
                PossibleCards = null,
                NumberOfCards = 0
            };

            await character.ShowOptionAsync(option);
        }

        public override async Task ActivateAsync(BaseCharacterLogic character, OptionDto dto)
        {
            await character.Character.Game.SetAllReactAsync(character.Character.Id, character.Hub, Card);
            await character.LeaveCardAsync(Card.Id);
        }

        public override async Task AnswearAsync(BaseCharacterLogic character, OptionDto dto)
        {
            var target = character.Character.Game.GetReactionCharacter().Map(character.Hub);

            if (dto.CardIds is null || dto.CardIds.Count == 0)
            {
                await target.DecreaseLifepointAsync(1);
            }
            else
            {
                var hasCard = await target.TryHasCardAsync(dto.CardIds.First(), "Missed!");
                if (!hasCard)
                {
                    await target.DecreaseLifepointAsync(1);
                }
            }
            await character.Character.Game.AllReactNextAsync(character.Hub);
        }
    }
}
