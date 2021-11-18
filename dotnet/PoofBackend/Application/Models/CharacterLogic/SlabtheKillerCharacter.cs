using Application.SignalR;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.CharacterLogic
{
    public class SlabtheKillerCharacter : BaseCharacterLogic
    {
        public SlabtheKillerCharacter(Character character, PoofGameHub hub) : base(character, hub) { }

    }
}
