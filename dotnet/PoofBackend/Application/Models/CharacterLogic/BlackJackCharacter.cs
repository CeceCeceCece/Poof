using Application.Constants;
using Application.Exceptions;
using Application.SignalR;
using Application.ViewModels;
using Domain.Constants.Enums;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.CharacterLogic
{
    public class BlackJackCharacter : BaseCharacterLogic
    {
        public BlackJackCharacter(Character character, PoofGameHub hub) : base(character,hub) {}

        public virtual async Task Draw(Game game)
        {
            var cards = Character.Game.GetAndRemoveCards(2);
            if(cards.ElementAt(1).Card.Suite == CardSuits.Diamonds || cards.ElementAt(1).Card.Suite == CardSuits.Hearths) 
            {
                cards.AddRange(Character.Game.GetAndRemoveCards(1));
                //TODO: Megmutatni a második lapot.
            }
            await DrawAsync(cards);
            await Character.Game.EndReactionAsync(Hub);
        }
    }
}
