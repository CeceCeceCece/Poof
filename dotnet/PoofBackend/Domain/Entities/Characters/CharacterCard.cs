using Domain.Constants.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class CharacterCard
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int LifePoint { get; set; }

        public virtual Character ToCharacter(Connection connection, RoleType role)
        {
            return new Character
            {
                Id = connection.UserId,
                Name = connection.Username,
                DistanceFromOthers = 1,
                BangState = BangState.One,
                ConnectionId = connection.ConnectionId,
                AimDistance = 0,
                Deck = new List<GameCard>(),
                EquipedCards = new List<GameCard>(),
                LifePoint = 4,
                MaxLifePoint = 4,
                PersonalCard = this,
                Role = role,
                Weapon = null,
                WeaponDistance = 1
            };
        }
    }
}
