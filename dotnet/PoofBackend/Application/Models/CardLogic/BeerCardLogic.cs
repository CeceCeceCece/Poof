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
    public class BeerCardLogic : CardLogic
    {
        public BeerCardLogic(GameCard card) : base(card)
        {
        }

        public override Option Option(string playerId, Game game)
        {
            var character = game.Characters.SingleOrDefault(x => x.Id == playerId);
            var logic = character.Map();
            logic.ActivateCard(game, Card.Id, new OptionDto { UserId = playerId });

            return new Option
            {
                Description = CardMessages.CARD_PLAYED,
                RequireAnswear = false,
                RequireCards = false,
                PossibleTargets = null,
                PossibleCards = null
            };
        }
        public override void Activate(Game game, OptionDto dto)
        {
            var character = game.GetCharacterById(dto.UserId);
            character.Map().IncreaseLifePont(1);
        }
    }
}
