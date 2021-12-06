using Application.SignalR;
using Domain.Entities;

namespace Application.Models.CharacterLogic
{
    public class PaulRegretCharacter : BaseCharacterLogic
    {
        public PaulRegretCharacter(Character character, PoofGameHub hub) : base(character, hub) { }

    }
}
