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
    public class MustangCardLogic : CardLogic
    {
        public MustangCardLogic(GameCard card) : base(card)
        {
        }

        public override async Task OptionAsync(BaseCharacterLogic character)
        {
            await character.ActivateCardAsync(Card.Id, null);
        }

        public override async Task ActivateAsync(BaseCharacterLogic character, OptionDto dto)
        {
            await character.EquipeCardAsync(Card);
            character.Character.DistanceFromOthers += 1;
        }

        public override Task DeactivateAsync(BaseCharacterLogic character)
        {
            character.Character.DistanceFromOthers -= 1;
            return Task.CompletedTask;
        }
    }
}
