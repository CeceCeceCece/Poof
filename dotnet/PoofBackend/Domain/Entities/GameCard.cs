using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class GameCard
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public Card Card{ get; set; }
        public GameCard()
        {
        }
        public GameCard(string id, Card card, string name)
        {
            Id = id;
            Card = card;
            Name = name;
        }
    }
}
