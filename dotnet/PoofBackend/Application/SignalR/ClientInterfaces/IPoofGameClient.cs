using Application.Models.ViewModels;
using Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SignalR.ClientInterfaces
{
    public interface IPoofGameClient
    {
        //Egy játékos kártyáit állítja be
        public Task SetDeck(List<CardViewModel> cards);
        //Egy játékos felszerelt kártyáit állítja be
        public Task SetEquipedDeck(List<CardViewModel> cards);
        //Egy játékos fegyverét állítja be, lehet null
        public Task SetWeapon(string characterId, CardViewModel weapon);
        //A kijátszott kártyák paklijának tetején lévő kártyát állítja be
        public Task SetDiscardPile(CardViewModel card);
        //Megadja egy játéko életerejét, mindkinek szól és a megfelelő id-ju játékos életerejét átálítja mindenki.
        public Task SetLifePoint(LifePointViewModel pointModel);
        public Task CardDroped(CardIdViewModel card);
        public Task CardUnequiped(CardIdViewModel card);
        public Task CardEquiped(string characterId, CardViewModel card);
        public Task CardsAdded(List<CardIdViewModel> cards);
    }
}
