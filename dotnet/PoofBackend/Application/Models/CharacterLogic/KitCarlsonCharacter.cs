using Application.Constants;
using Application.Exceptions;
using Application.Models.DTOs;
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
    public class KitCarlsonCharacter : BaseCharacterLogic
    {
        public KitCarlsonCharacter(Character character, PoofGameHub hub) : base(character, hub) { }

        public override Task DrawAsync()
        {
            Character.Game.Event = GameEvent.Draw;
            return Task.CompletedTask;   
            //Hub értesítés hogy mit kell tenni.

            //return new Option
            //{
            //    Description = CardMessages.CHOOSE_ONE_PLAYER,
            //    NumberOfCards = 2,
            //    PossibleCards = game.GetCards(3)
            //        .Select(x => new CardViewModel
            //        {
            //            Id = x.Id,
            //            Name = x.Card.Name
            //        })
            //        .ToList(),
            //    PossibleTargets = null,
            //    RequireAnswear = true,
            //    RequireCards = true,
            //};
        }

        public override async Task DrawReactAsync(OptionDto option)
        {
            if (option.CardIds.Count != 2)
                throw new PoofException(CharacterMessages.NEM_MEGFELELO_HUZAS);

            List<GameCard> cards = new List<GameCard>();
            foreach (var cardId in option.CardIds)
            {
                cards.Add(Character.Game.GetCard(cardId));
            }
            await DrawAsync(cards);
            await Character.Game.EndReactionAsync(Hub);
        }
    }
}
