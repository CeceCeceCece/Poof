using Application.Constants;
using Application.Exceptions;
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
    public class BlackJackCharacter : CharacterLogic
    {
        public BlackJackCharacter(Character character) : base(character) {}

        public virtual Option Draw(Game game)
        {
            if (character is null)
                throw new PoofException(CharacterMessages.JATEKOS_NEM_A_JATEK_RESZE);

            var secondCard = game.Deck.ElementAt(1);
            IEnumerable<GameCard> cards;
            if (secondCard.Card.Suite == CardSuits.Diamonds || secondCard.Card.Suite == CardSuits.Hearths) 
            {
                cards = game.GetAndRemoveCards(3);
                character.Deck.AddRange(cards);
            }
            else 
            {
                cards = game.GetCards(2);
                character.Deck.AddRange(cards);
            }
            game.Event = GameEvent.None;

            List<CardViewModel> resultList = new List<CardViewModel>();
            foreach (var card in cards)
            {
                if(card.Id == secondCard.Id) 
                {
                    resultList.Add(new CardViewModel
                    {
                        Id = card.Id,
                        Name = card.Card.Name,
                        Show = true
                    });
                }
                else 
                {
                    resultList.Add(new CardViewModel
                    {
                        Id = card.Id,
                        Name = card.Card.Name
                    });
                }
            }

            return new Option
            {
                Description = CharacterMessages.HUZOTT_LAPOK,
                NumberOfCards = 0,
                PossibleCards = resultList,
                PossibleTargets = null,
                RequireAnswear = false,
                RequireCards = false
            };
        }
    }
}
