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
    public class IndiansCardLogic : CardLogic
    {
        public IndiansCardLogic(GameCard card) : base(card)
        {
        }

        public override async Task OptionAsync(BaseCharacterLogic character)
        {
            await character.ActivateCardAsync(Card.Id, null);
        }

        public override async Task ActivateAsync(BaseCharacterLogic character, OptionDto dto)
        {
            await character.Character.Game.SetAllReactAsync(character.Character.Id, character.Hub, Card);
            //Hub értesítés hogy reagáljon bang-al.
        }
        public override async Task AnswearAsync(BaseCharacterLogic character, OptionDto dto)
        {
            if(dto.CardIds is null || dto.CardIds.Count <= 0 || !await character.TryHasCardAsync(dto.CardIds.First(), "Bang!")) 
            {
                await character.DecreaseLifepointAsync(1);
            }
            await character.Character.Game.AllReactNextAsync(character.Hub);
        }
    }
}
