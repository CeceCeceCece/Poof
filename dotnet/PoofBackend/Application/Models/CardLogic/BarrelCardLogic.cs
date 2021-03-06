using Application.Constants;
using Application.Models.CharacterLogic;
using Application.Models.DTOs;
using Application.ViewModels;
using Domain.Constants.Enums;
using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Models.CardLogic
{
    public class BarrelCardLogic : CardLogic
    {
        public BarrelCardLogic(GameCard card) : base(card)
        {
        }

        public override async Task OptionAsync(BaseCharacterLogic character)
        {
            var option = new OptionViewModel
            {
                Description = CardMessages.BARREL_OPTION,
                RequireCards = false,
                PossibleTargets = new List<string> { character.Character.Id },
            };

            await character.ShowOptionAsync(option);
        }

        public override async Task ActivateAsync(BaseCharacterLogic character, OptionDto dto)
        {
            await character.EquipeCardAsync(Card.Id);
        }

        public override async Task OnActiveAsync(BaseCharacterLogic character)
        {
            if (character.Character.Game.Event != GameEvent.SingleReact || character.Character.Game.NextCard is null)
                return;

            if (character.Character.Game.NextCard.Card.Name == "Bang!" && await character.Character.Game.CheckNextCardAsync(CardSuits.Hearths, null, character.Hub))
            {
                await character.Character.Game.EndReactionAsync(character.Hub);
            }
        }
    }
}
