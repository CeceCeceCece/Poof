using Application.Constants;
using Application.Exceptions;
using Application.Models.CharacterLogic;
using Application.Models.DTOs;
using Application.ViewModels;
using Domain.Constants.Enums;
using Domain.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Models.CardLogic
{
    public class BangCardLogic : CardLogic
    {
        public BangCardLogic(GameCard card) : base(card){}

        public override async Task OptionAsync(BaseCharacterLogic character)
        {
            var option = new OptionViewModel
            {
                Description = CardMessages.BANG_OPTION,
                RequireAnswear = true,
                RequireCards = false,
                PossibleTargets = character.GetNeighbours(true),
                PossibleCards = null
            };
            await character.ShowOptionAsync(option);
        }

        public override async Task ActivateAsync(BaseCharacterLogic character, OptionDto dto)
        {
            if(character.Character.BangState == BangState.None)
                throw new PoofException(CardMessages.BANG_USING_AGAIN);

            if (string.IsNullOrEmpty(dto.UserId))
                throw new PoofException(CharacterMessages.NEM_MEGFELELO_JATEKOS_AZONOSITO);

            var targetCharacter = character.Character.Game.GetCharacterById(dto.UserId);

            await character.Character.Game.SetSingleReactAsync(Card, targetCharacter.Id, character.Hub);
            await character.LeaveCardAsync(Card.Id);

            if (character.Character.BangState == BangState.One)
                character.Character.BangState = BangState.None;

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
                if (!await target.TryHasCardAsync(dto.CardIds.First(), "Missed!"))
                    throw new PoofException(CardMessages.BANG_ANSWEAR_ERROR);
            }
            await character.Character.Game.EndReactionAsync(character.Hub);
        }
    }
}
