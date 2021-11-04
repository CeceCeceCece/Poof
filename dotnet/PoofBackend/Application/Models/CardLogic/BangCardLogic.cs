using Application.Constants;
using Application.ViewModels;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.CardLogic
{
    public class BangCardLogic : CardLogic
    {
        public override Option Option(string playerId, Game game)
        {
            return new Option
            {
                Description = CardMessages.CHOOSE_ONE_PLAYER,
                RequireAnswear = true,
                RequireCards = false,
                PossibleTargets = game.Neigbours(playerId),
                PossibleCards = null
            };
        }
        public override void Activate(string id)
        {
            throw new NotImplementedException();
        }
    }
}
