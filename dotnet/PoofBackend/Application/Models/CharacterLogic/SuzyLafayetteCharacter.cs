using Application.SignalR;
using Domain.Entities;
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

        public override async Task<GameCard> LeaveCardAsync(string cardId, bool inEquiped = false)
        {
            var card = await base.LeaveCardAsync(cardId, inEquiped);
            if (Character.Deck.Count <= 0)
            {
                await DrawAsync(Character.Game.GetAndRemoveCards(1));
            }
            return card;
        }
    }
}
