﻿using Application.Constants;
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
    public class BarrelCardLogic : CardLogic
    {
        public BarrelCardLogic(GameCard card) : base(card)
        {
        }

        public override async Task OptionAsync(BaseCharacterLogic character)
        {
            await character.ActivateCardAsync(Card.Id, null);

            //TODO: hub ertesítés

            //return new Option
            //{
            //    Description = CardMessages.CARD_EQUIPPED,
            //    RequireAnswear = false,
            //    RequireCards = false,
            //    PossibleTargets = null,
            //    PossibleCards = null
            //};
        }

        public override async Task ActivateAsync(BaseCharacterLogic character, OptionDto dto)
        {
            await character.EquipeCardAsync(Card.Id);
        }

        public override async Task OnActiveAsync(BaseCharacterLogic character)
        {
            if(character.Character.Game.NextCard.Name == "Bang!" && await character.Character.Game.CheckNextCardAsync(CardSuits.Hearths, null, character.Hub)) 
            {
                await character.Character.Game.EndReactionAsync(character.Hub);
            }
        }
    }
}
