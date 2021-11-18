using Application.Constants;
using Application.Exceptions;
using Application.SignalR;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.CharacterLogic
{
    public class SuzyLafayetteCharacter : BaseCharacterLogic
    {
        public SuzyLafayetteCharacter(Character character, PoofGameHub hub) : base(character, hub) { }

        public override async Task DropCardAsync(string cardId)
        {
            await base.DropCardAsync(cardId);
            if(Character.Deck.Count <= 0) 
            {
                await DrawAsync(Character.Game.GetAndRemoveCards(1));
            }
        }

        public override async Task<GameCard> LeaveCardAsync(string cardId)
        {
            var card = await base.LeaveCardAsync(cardId);
            if (Character.Deck.Count <= 0)
            {
                await DrawAsync(Character.Game.GetAndRemoveCards(1));
            }
            return card;
        }
    }
}
