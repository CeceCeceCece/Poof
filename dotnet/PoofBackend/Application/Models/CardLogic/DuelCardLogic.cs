using Application.Constants;
using Application.Exceptions;
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
    public class DuelCardLogic : CardLogic
    {
        public DuelCardLogic(GameCard card) : base(card)
        {
        }

        public override Option Option(string playerId, Game game)
        {
            return new Option
            {
                Description = CardMessages.CHOOSE_ONE_PLAYER,
                RequireAnswear = true,
                RequireCards = false,
                PossibleTargets = game.GetAllPlayer(),
                PossibleCards = null
            };
        }

        public override void Activate(Game game, OptionDto dto)
        {
            if (!game.Characters.Any(x => x.Id == dto.UserId))
                throw new PoofException(GameMessages.FELHASZNALO_NEM_A_JATEK_RESZE);

            game.Event = GameEvent.SingleReact;
            game.NextUserId = dto.UserId;
            game.NextCard = Card;

            //Értesítés
        }

        public override void Answear(Game game, OptionDto dto)
        {
            
        }
    }
}
