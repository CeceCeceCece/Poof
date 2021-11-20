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
    public class GeneralStoreCardLogic : CardLogic
    {
        public GeneralStoreCardLogic(GameCard card) : base(card)
        {
        }

        public override async Task OptionAsync(BaseCharacterLogic character)
        {
            await character.ActivateCardAsync(Card.Id, null);

            var option = new OptionViewModel
            {
                Description = CardMessages.GENERAL_STORE_OPTION,
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
            await character.Character.Game.SetAllReactAsync(character.Character.Id, character.Hub, Card, true);
            await character.LeaveCardAsync(Card.Id);
            //HUB miből lehet választani.
        }

        public override async Task AnswearAsync(BaseCharacterLogic character, OptionDto dto)
        {
            if (dto.CardIds is null && dto.CardIds.Count == 0)
                throw new PoofException(CardMessages.GENERAL_STORE_ERROR);

            var target = character.Character.Game.GetReactionCharacter().Map(character.Hub);
            await target.GetCardFromGameAsync(dto.CardIds.First());
            await character.Character.Game.AllReactNextAsync(character.Hub);
            //HUB miből választhat
        }
    }
}
