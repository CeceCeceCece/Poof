using Application.SignalR;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.CharacterLogic
{
    public class ElGringoCharacter : BaseCharacterLogic
    {
        public ElGringoCharacter(Character character, PoofGameHub hub) : base(character, hub) { }

        public override async Task DecreaseLifepointAsync(int point)
        {
            if(point == 1 && Character.Game.CurrentUserId != Character.Id) 
            {
                var card = await Character.Game.GetCurrentCharacter().Map(Hub).LeaveCardRandomAsync();
                await DrawAsync(new List<GameCard> { card });
            }
            else 
            {
                await base.DecreaseLifepointAsync(point);
            }
        }
    }
}
