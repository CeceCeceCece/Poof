using Application.ViewModels;
using Domain.Constants.Enums;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.CardLogic
{
    public abstract class CardLogic
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public CardValues Value { get; set; }
        public CardSuits Suite { get; set; }
        public CardType Type { get; set; }

        public abstract Option Option(string playerId, Game game);

        public abstract void Activate();
    }
}
