using Domain.Constants.Enums;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Character
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ConnectionId { get; set; }
        public int MaxLifePoint { get; set; }
        public int LifePoint { get; set; }
        public BangState BangState { get; set; }
        public RoleType Role { get; set; }
        public int WeaponDistance { get; set; }
        public int AimDistance { get; set; }
        public int DistanceFromOthers { get; set; }
        public Game Game { get; set; }
        public GameCard Weapon { get; set; }
        public CharacterCard PersonalCard { get; set; }
        public List<GameCard> Deck { get; set; } = new List<GameCard>();
        public List<GameCard> EquipedCards { get; set; } = new List<GameCard>();
    }
}
