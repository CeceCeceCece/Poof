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
    public class BangCardLogic : CardLogic
    {
        public BangCardLogic(GameCard card) : base(card){}

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
        public override void Activate(Game game, OptionDto dto)
        {
            if (string.IsNullOrEmpty(dto.UserId))
                throw new PoofException(CharacterMessages.NEM_MEGFELELO_JATEKOS_AZONOSITO);

            var character = game.Characters.SingleOrDefault(x => x.Id == dto.UserId);
            if (character is null)
                throw new PoofException(CharacterMessages.JATEKOS_NEM_A_JATEK_RESZE);

            game.Event = GameEvent.SingleReact;
            game.NextUserId = character.Id;
            game.NextCard = Card;
        }
    }
}
