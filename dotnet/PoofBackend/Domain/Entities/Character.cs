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
        public string Name { get; set; }
        public int LifePoint { get; set; }
        public int MaxLifePoint { get; set; }
        public int NumberOfBangs { get; set; }
        public int AimDistance { get; set; }
        public int Distance { get; set; }
        public int GameId { get; set; }
        public Game Game{ get; set; }
        public CharacterType Type { get; set; }
    }
}
