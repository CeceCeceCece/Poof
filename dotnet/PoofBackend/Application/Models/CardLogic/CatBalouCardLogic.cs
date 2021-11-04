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
    public class CatBalouCardLogic : CardLogic
    {
        public override Option Option(string playerId, Game game)
        {
            Activate(null);
            return new Option
            {
                Description = CardMessages.CHOOSE_ONE_PLAYER,
                RequireAnswear = false,
                RequireCards = true,
                PossibleTargets = game.GetAllPlayer(),
                PossibleCards = null
            };
        }
        public override void Activate(string id)
        {
            throw new NotImplementedException();
        }
    }
}
