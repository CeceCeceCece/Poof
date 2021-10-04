using Domain.ApplicationViewModel;
using Domain.Constants.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public abstract class Card
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public CardValues Value{ get; set; }
        public CardSuits Suite { get; set; }
        public CardType Type { get; set; }

        public abstract CardOption Option(string playerId, Game game);
    }
}
