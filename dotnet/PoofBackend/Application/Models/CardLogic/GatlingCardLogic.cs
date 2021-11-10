using Application.Constants;
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
    public class GatlingCardLogic : CardLogic
    {
        public GatlingCardLogic(GameCard card) : base(card)
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
            game.NextCard = Card;
            game.SetAllReact(dto.UserId);
        }

        public override void Answear(Game game, OptionDto dto)
        {
            if(dto.CardIds is null || dto.CardIds.Count == 0) 
            {
                var character = game.GetReactionCharacter();
                character.Map().DecreaseLifepoint(1);
            }
            else 
            {
                var character = game.GetReactionCharacter();
                var hasCard = character.Map().TryHasCard(dto.CardIds.First(), "Bang!");
                if (!hasCard) 
                {
                    character.Map().DecreaseLifepoint(1);
                }
            }
            game.AllReactNext();

        }
    }
}
