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
    public class KitCarlsonCharacter : BaseCharacterLogic
    {
        public KitCarlsonCharacter(Character character, PoofGameHub hub) : base(character, hub) { }

        public override async Task DrawAsync()
        {
            Character.Game.Event = GameEvent.Draw;
            await Hub.Clients.Client(Character.ConnectionId).DrawOption(new DrawOptionViewModel(false, Character.Game.GetCards(3).Select(x => new CardViewModel(x.Id, x.Card.Name, x.Card.Type, x.Card.Suite, x.Card.Value)).ToList()));
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
