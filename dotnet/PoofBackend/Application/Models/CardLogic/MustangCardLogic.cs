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
    public class MustangCardLogic : CardLogic
    {
        public MustangCardLogic(GameCard card, Game game, ) : base(card)
        {
        }

        public override Option Option(string playerId, Game game)
        {
            game.GetCurrentCharacter().Map().ActivateCard(game, Card.Id, null);

            return new Option
            {
                Description = CardMessages.CARD_EQUIPPED,
                RequireAnswear = false,
                RequireCards = false,
                PossibleTargets = null,
                PossibleCards = null
            };
        }

        public override void Activate(Game game, OptionDto dto)
        {
            var character = game.GetCurrentCharacter();
            character.Map().EquipeCard(Card.Id);
            character.DistanceFromOthers += 1;
        }

        public override void Deactivate(Game game)
        {
            character.DistanceFromOthers -= 1;
        }
    }
}
