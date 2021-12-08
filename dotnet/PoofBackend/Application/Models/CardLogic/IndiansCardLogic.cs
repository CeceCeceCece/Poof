using Application.Constants;
using Application.Models.CharacterLogic;
using Application.Models.DTOs;
using Application.ViewModels;
using Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Models.CardLogic
{
    public class IndiansCardLogic : CardLogic
    {
        public IndiansCardLogic(GameCard card) : base(card)
        {
        }

        public override async Task OptionAsync(BaseCharacterLogic character)
        {
            var option = new OptionViewModel
            {
                Description = CardMessages.INDIANS_OPTION,
                RequireCards = false,
                PossibleTargets = new List<string> { character.Character.Id }
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
            if(dto.CardIds is null || dto.CardIds.Count <= 0 || !await target.TryHasCardAsync(dto.CardIds.First(), "Bang!")) 
            {
                await target.DecreaseLifepointAsync(1);
            }
            await character.Character.Game.AllReactNextAsync(character.Hub);
        }
    }
}
