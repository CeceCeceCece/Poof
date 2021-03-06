using Application.Models.DTOs;
using Application.SignalR;
using Domain.Constants.Enums;
using Domain.Entities;
using System.Threading.Tasks;

namespace Application.Models.CharacterLogic
{
    public class JesseJonesCharacter : BaseCharacterLogic
    {

        public JesseJonesCharacter(Character character, PoofGameHub hub) : base(character, hub) { }

        public override async Task DrawReactAsync(OptionDto option)
        {
            if (string.IsNullOrEmpty(option.UserId) || option.UserId == Character.Id)
            {
                var cards = Character.Game.GetAndRemoveCards(2);
                await DrawAsync(cards);
            }
            else
            {
                var card = await Character.Game.GetCharacterById(option.UserId).Map(Hub).LeaveCardRandomAsync();

                var cards = Character.Game.GetAndRemoveCards(1);
                cards.Add(card);

                await DrawAsync(cards);
            }
            Character.Game.Event = GameEvent.None;
        }
    }
}
