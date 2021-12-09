using Application.Models.DTOs;
using Application.SignalR;
using Application.ViewModels;
using Domain.Constants.Enums;
using Domain.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Models.CharacterLogic
{
    public class BlackJackCharacter : BaseCharacterLogic
    {
        public BlackJackCharacter(Character character, PoofGameHub hub) : base(character, hub) { }

        public override async Task DrawReactAsync(OptionDto options)
        {
            var cards = Character.Game.GetAndRemoveCards(2);
            var second = cards.ElementAt(1);
            if (second.Card.Suite == CardSuits.Diamonds || second.Card.Suite == CardSuits.Hearths)
            {
                cards.AddRange(Character.Game.GetAndRemoveCards(1));
            }
            await DrawAsync(cards);
            Character.Game.Event = GameEvent.None;

            if (Hub is not null)
                await Hub.Clients.Group(Character.Game.Name).ShowCard(new CardViewModel(second.Id, second.Card.Name, second.Card.Type, second.Card.Suite, second.Card.Value));
        }
    }
}
