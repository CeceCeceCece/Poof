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
    public class DynamiteCardLogic : CardLogic
    {
        public override Option Option(string playerId, Game game)
        {
            Activate();

            return new Option
            {
                Description = CardMessages.CARD_EQUIPPED,
                RequireAnswear = false,
                RequireCards = false,
                PossibleTargets = null,
                PossibleCards = null
            };
        }
        public override void Activate()
        {
            throw new NotImplementedException();
        }
    }
}
