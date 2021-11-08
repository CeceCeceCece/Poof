using Application.Models.DTOs;
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
        public GameCard Card { get; set; }

        public CardLogic(GameCard card)
        {
            Card = card;
        }

        //Felhasználási lehetőségek
        public abstract Option Option(string playerId, Game game);
        //Kártya aktiválása
        public abstract void Activate(Game game, OptionDto dto);
        //Kártyára érkező válasz
        public virtual void Answear(Game game, OptionDto dto) { }
        //Kártya hatástalanítása
        public virtual void Deactivate(Character character) { }
        //Kártya aktív állapotban kifejtett hatása
        public virtual void OnActive() { }
    }
}
