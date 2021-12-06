using Application.SignalR;
using Domain.Entities;

namespace Application.Models.CharacterLogic
{
    public class WillytheKidCharacter : BaseCharacterLogic
    {
        public WillytheKidCharacter(Character character, PoofGameHub hub) : base(character, hub) { }

    }
}
