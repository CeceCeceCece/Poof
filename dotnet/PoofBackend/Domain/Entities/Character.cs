using Domain.Constants.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Character
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public int LifePoint { get; set; }
        public int NumberOfBangs { get; set; }
        public int AimDistance { get; set; }
        public int Distance { get; set; }
        public string GameId { get; set; }
        public Game Game{ get; set; }
        public string WeaponId { get; set; }
        public Card Weapon { get; set; }
        public string RoleId { get; set; }
        public RoleCard Role { get; set; }
        public CharacterType Type { get; set; }
    }
}
