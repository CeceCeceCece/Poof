using Application.Models.CharacterLogic;
using Application.SignalR;
using Domain.Entities;
using System.Threading.Tasks;

namespace Application.Models.CharacterLogics
{
    public class BartCassidyCharacter : BaseCharacterLogic
    {
        public BartCassidyCharacter(Character character, PoofGameHub hub) : base(character, hub)
        {
        }

        public override async Task DecreaseLifepointAsync(int point)
        {
            if(point == 1) 
            {
                await DrawAsync(Character.Game.GetAndRemoveCards(1));
            }
            await base.DecreaseLifepointAsync(point);
        }
    }
}
