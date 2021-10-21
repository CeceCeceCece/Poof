using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class RoleCard
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int LifePoint { get; set; }

        public Character ToCharacter(Game game, string userId)
        {
            return new Character
            {
                Distance = 0,
                AimDistance = 1,
                Game = game,
                LifePoint = LifePoint,
                NumberOfBangs = 1,
                Type = Constants.Enums.CharacterType.None,
                Id = userId
            };
        }
    }
}
