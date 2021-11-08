using Application.Constants;
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
    public class CatBalouCardLogic : CardLogic
    {
        public CatBalouCardLogic(GameCard card) : base(card)
        {
        }

        public override Option Option(string playerId, Game game)
        {
            return new Option
            {
                Description = CardMessages.CHOOSE_ONE_CARD,
                RequireAnswear = true,
                RequireCards = true,
                PossibleTargets = game.GetAllPlayer(),
                PossibleCards = null,
                NumberOfCards = 1
            };
        }

        public override void Activate(Game game, OptionDto dto)
        {
            var character = game.GetCharacterById(dto.UserId);
            character.Map().DropCard(dto.CardIds.First(), game);
        }
    }
}
