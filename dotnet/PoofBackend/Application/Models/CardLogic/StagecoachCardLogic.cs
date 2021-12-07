using Application.Constants;
using Application.Models.CharacterLogic;
using Application.Models.DTOs;
using Application.ViewModels;
using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Models.CardLogic
{
    public class StagecoachCardLogic : CardLogic
    {
        public StagecoachCardLogic(GameCard card) : base(card)
        {
        }

        public override async Task OptionAsync(BaseCharacterLogic character)
        {
            var option = new OptionViewModel
            {
                Description = CardMessages.DRAW_OPTION,
                RequireCards = false,
                PossibleTargets = new List<string> { character.Character.Id }
            };

            await character.ShowOptionAsync(option);
        }

        public override async Task ActivateAsync(BaseCharacterLogic character, OptionDto dto)
        {
            var cards = character.Character.Game.GetAndRemoveCards(2);
            await character.DrawAsync(cards);
            await character.DropCardAsync(Card.Id);
        }
    }
}
