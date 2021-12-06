using Application.Models.DTOs;
using Application.SignalR;
using Application.ViewModels;
using Domain.Constants.Enums;
using Domain.Entities;
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

            //HUB

            //return new Option
            //{
            //    Description = CharacterMessages.A_PAKLIBOL_VAGY_AZ_ELDOBOTT_LAPOK_KOZUL,
            //    NumberOfCards = 0,
            //    PossibleCards = null,
            //    PossibleTargets = new List<string> { Character.Id },
            //    RequireAnswear = true,
            //    RequireCards = false,
            //};
        }

        public override async Task DrawReactAsync(OptionDto option)
        {
            if (string.IsNullOrEmpty(option.UserId)) 
            {
                await base.DrawAsync();
            }
            else 
            {
                var cards = Character.Game.GetAndRemoveCards(1);
                var card = Character.Game.DiscardPile.Last();
                Character.Game.DiscardPile.Remove(card);
                cards.Add(card);
                await DrawAsync(cards);

                //HUB az eldobott lapok tetején új lap az alsó
                var last = Character.Game.DiscardPile.LastOrDefault();
                await Hub.Clients.Group(Character.Game.Name).SetDiscardPile(last is null ? null : new CardViewModel(last.Id, last.Card.Name, last.Card.Type, last.Card.Suite, last.Card.Value));
            }
        }
    }
}
