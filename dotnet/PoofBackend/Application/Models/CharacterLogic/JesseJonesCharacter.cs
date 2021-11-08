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

namespace Application.Models.CharacterLogic
{
    public class JesseJonesCharacter : CharacterLogic
    {

        public JesseJonesCharacter(Character character) : base(character) { }

        public override Option Draw(Game game)
        {
            if (character is null)
                throw new PoofException(CharacterMessages.JATEKOS_NEM_A_JATEK_RESZE);

            game.Event = GameEvent.Draw;

            return new Option
            {
                Description = CardMessages.CHOOSE_ONE_PLAYER,
                NumberOfCards = 0,
                PossibleCards = null,
                PossibleTargets = game.GetAllPlayer(),
                RequireAnswear = true,
                RequireCards = false
            };
        }

        public override void DrawReact(Game game, OptionDto option) 
        {
            if(string.IsNullOrEmpty(option.UserId) || option.UserId == character.Id) 
            {
                var cards = game.GetAndRemoveCards(2);
                character.Deck.AddRange(cards);
            }
            else 
            {
                var target = game.Characters.SingleOrDefault(x => x.Id == option.UserId);
                if (target is null)
                    throw new PoofException(CharacterMessages.JATEKOS_NEM_A_JATEK_RESZE);

                var random = new Random();
                var value = random.Next(target.Deck.Count);
                var card = target.Deck.ElementAt(value);
                target.Deck.RemoveAt(value);

                var secondCard = game.Deck.First();
                game.Deck.Remove(secondCard);

                character.Deck.Add(card);
                character.Deck.Add(secondCard);
            }

            game.Event = GameEvent.None;
        }
    }
}
