using Domain.Constants.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Game
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public GameEvent Event { get; set; }
        public string CurrentUserId { get; set; }
        public string NextUserId { get; set; }
        public WinType Win { get; set; }
        public GameCard NextCard { get; set; }
        public List<GameCard> Deck { get; set; } = new List<GameCard>();
        public List<GameCard> DiscardPile { get; set; } = new List<GameCard>();
        public List<Character> Characters { get; set; } = new List<Character>();
        public List<Message> Messages { get; set; } = new List<Message>();
    }
}
