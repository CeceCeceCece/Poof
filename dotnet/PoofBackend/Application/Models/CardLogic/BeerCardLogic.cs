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
    public class BeerCardLogic : CardLogic
    {
        public BeerCardLogic(GameCard card) : base(card)
        {
        }

        public override async Task OptionAsync(BaseCharacterLogic character)
        {
            await character.ActivateCardAsync(Card.Id, null);
            //TODO: sikrült aktiválni értesítés
            //return new Option
            //{
            //    Description = CardMessages.CARD_PLAYED,
            //    RequireAnswear = false,
            //    RequireCards = false,
            //    PossibleTargets = null,
            //    PossibleCards = null
            //};
        }

        public override async Task ActivateAsync(BaseCharacterLogic character, OptionDto dto)
        {
            await character.IncreaseLifePontAsync(1);
            await character.DropCardAsync(Card.Id);
        }
    }
}
