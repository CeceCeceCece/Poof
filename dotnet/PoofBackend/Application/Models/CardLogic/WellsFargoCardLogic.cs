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
    public class WellsFargoCardLogic : CardLogic
    {
        public WellsFargoCardLogic(GameCard card) : base(card)
        {
        }

        public override async Task OptionAsync(BaseCharacterLogic character)
        {
            await character.ActivateCardAsync(Card.Id, null);

            var option = new OptionViewModel
            {
                Description = CardMessages.DRAW_OPTION,
                RequireAnswear = false,
                RequireCards = false,
                PossibleTargets = null,
                PossibleCards = null
            };

            await character.ShowOptionAsync(option);

        }

        public override async Task ActivateAsync(BaseCharacterLogic character, OptionDto dto)
        {
            var cards = character.Character.Game.GetAndRemoveCards(3);
            await character.DrawAsync(cards);
            await character.DropCardAsync(Card.Id);
        }
    }
}
