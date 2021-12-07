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
    public class DynamiteCardLogic : CardLogic
    {
        public DynamiteCardLogic(GameCard card) : base(card)
        {
        }

        public override async Task OptionAsync(BaseCharacterLogic character)
        {
            var option = new OptionViewModel
            {
                Description = CardMessages.DYNAMITE_OPTION,
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
            if (character.Character.Game.Event != GameEvent.Draw)
                return;

            List<CardValues> values = new List<CardValues> { CardValues.Two, CardValues.Three, CardValues.Four, CardValues.Five, CardValues.Six, CardValues.Seven, CardValues.Eight, CardValues.Nine };
            if (await character.Character.Game.CheckNextCardAsync(CardSuits.Spades, values, character.Hub)) 
            {
                await character.DecreaseLifepointAsync(3);
                await character.DropCardAsync(Card.Id);
            }
            else 
            {
                await character.Character.Game.GetNextCharacter().Map(character.Hub).EquipeCardAsync(Card);
                await character.UnequipeCard(Card.Id);
            }
        }
    }
}
