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
    public class IndiansCardLogic : CardLogic
    {
        public IndiansCardLogic(GameCard card) : base(card)
        {
        }

        public override Option Option(string playerId, Game game)
        {
            Activate(null);
            return new Option
            {
                Description = CardMessages.CARD_PLAYED,
                RequireAnswear = false,
                RequireCards = false,
                PossibleTargets = null,
                PossibleCards = new List<CardViewModel>()
            };
        }
        public override void Activate(string id)
        {
            throw new NotImplementedException();
        }
    }
}
