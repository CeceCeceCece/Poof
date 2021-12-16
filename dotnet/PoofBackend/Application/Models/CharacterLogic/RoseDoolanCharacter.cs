using Application.SignalR;
using Domain.Entities;

namespace Application.Models.CharacterLogic
{
    public class RoseDoolanCharacter : BaseCharacterLogic
    {
        public RoseDoolanCharacter(Character character, PoofGameHub hub) : base(character, hub) { }

    }
}
