using Application.SignalR;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.CharacterLogic
{
    public class SidKetchumCharacter : BaseCharacterLogic
    {
        public SidKetchumCharacter(Character character, PoofGameHub hub) : base(character, hub) { }

        public override async Task DropCardsFromDeckAsync(List<string> cardIds)
        {
            await base.DropCardsFromDeckAsync(cardIds);
            if(cardIds.Count > 2) 
            {
                await DecreaseLifepointAsync(1);
            }
        }

    }
}
