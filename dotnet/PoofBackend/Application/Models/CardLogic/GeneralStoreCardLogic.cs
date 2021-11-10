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
    public class GeneralStoreCardLogic : CardLogic
    {
        public GeneralStoreCardLogic(GameCard card) : base(card)
        {
        }

        public override Option Option(string playerId, Game game)
        {
            var character = game.GetCharacterById(playerId);
            character.Map().ActivateCard(game, Card.Id, new OptionDto { UserId = playerId });

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
            var character = game.GetCurrentCharacter();
            character.Deck.AddRange(game.GetAndRemoveCards(3));
        }
    }
}
