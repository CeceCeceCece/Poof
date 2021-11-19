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
    public class VolcanicCardLogic : CardLogic
    {
        public VolcanicCardLogic(GameCard card) : base(card)
        {
        }

        public override async Task OptionAsync(BaseCharacterLogic character)
        {
            await character.ActivateCardAsync(Card.Id, null);
        }

        public override async Task ActivateAsync(BaseCharacterLogic character, OptionDto dto)
        {
            await character.EquipeWeaponAsync(Card.Id);
            character.Character.WeaponDistance = 1;
            if (character.Character.BangState != BangState.All)
                character.Character.BangState = BangState.WeaponAll;
        }

        public override Task DeactivateAsync(BaseCharacterLogic character)
        {
            character.Character.WeaponDistance = 1;
            if (character.Character.BangState != BangState.All)
                character.Character.BangState = BangState.One;
            return Task.CompletedTask;
        }
    }
}
