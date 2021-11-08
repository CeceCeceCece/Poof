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
    public class PanicCardLogic : CardLogic
    {
        public PanicCardLogic(GameCard card) : base(card)
        {
        }

        public override Option Option(string playerId, Game game)
        {
            return new Option
            {
                Description = CardMessages.CHOOSE_ONE_PLAYER,
                RequireAnswear = true,
                RequireCards = true,
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
