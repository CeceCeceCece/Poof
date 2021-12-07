using Application.Constants;
using Application.Exceptions;
using Application.Models.DTOs;
using Application.SignalR;
using Application.ViewModels;
using Domain.Constants.Enums;
using Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Models.CharacterLogic
{
    public class PedroRamirezCharacter : BaseCharacterLogic
    {
        public PedroRamirezCharacter(Character character, PoofGameHub hub) : base(character, hub) { }

        public override async Task DrawAsync()
        {
            if (Character.Game.DiscardPile.Count <= 0)
            {
                await base.DrawAsync();
            }
            Character.Game.Event = GameEvent.Draw;
            var card = Character.Game.DiscardPile.Last();
            await Hub.Clients.Client(Character.ConnectionId).DrawOption(new DrawOptionViewModel(false, new List<CardViewModel> { new CardViewModel(card.Id, card.Card.Name, card.Card.Type, card.Card.Suite, card.Card.Value) }));
        }

        public override async Task DrawReactAsync(OptionDto option)
        {
            if (option.CardIds is null || option.CardIds.Count == 0) 
            {
                await base.DrawAsync();
            }
            else 
            {
                var cards = Character.Game.GetAndRemoveCards(1);
                var card = Character.Game.DiscardPile.Last();

                if (card.Id != option.CardIds.First())
                    throw new PoofException(CharacterMessages.NEM_MEGFELELO_HUZAS);

                Character.Game.DiscardPile.Remove(card);
                cards.Add(card);
                await DrawAsync(cards);

                var last = Character.Game.DiscardPile.LastOrDefault();
                await Hub.Clients.Group(Character.Game.Name).SetDiscardPile(last is null ? null : new CardViewModel(last.Id, last.Card.Name, last.Card.Type, last.Card.Suite, last.Card.Value));
            }
        }
    }
}
