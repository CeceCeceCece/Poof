using Domain.ApplicationViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Cards
{
    public class BangCard : Card
    {
        public override CardOption Option(string playerId, Game game)
        {
            return new CardOption
            {
                Description = "Choose one player",
                RequireAnswear = true,
                RequireCards = false,
                PossibleTargets = game.Neigbours(playerId)
            };
        }
    }
}
