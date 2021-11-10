using Application.Constants;
using Application.Exceptions;
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
    public class JailCardLogic : CardLogic
    {
        public JailCardLogic(GameCard card) : base(card)
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
            var character = game.GetCharacterById(dto.UserId);
            if (character is null)
                throw new PoofException(GameMessages.FELHASZNALO_NEM_A_JATEK_RESZE);

            character.EquipedCards.Add(Card);
        }

        public override void OnActive()
        {
            //TODO: Kör végénél
        }
    }
}
