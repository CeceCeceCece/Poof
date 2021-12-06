using Application.Models.ViewModels;
using Application.ViewModels;
using Domain.Constants.Enums;
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
        //A kijátszott kártyák paklijának tetején lévő kártyát állítja be
        public Task SetDiscardPile(CardViewModel card);
        //Megadja egy játéko életerejét, mindkinek szól és a megfelelő id-ju játékos életerejét átálítja mindenki.

        //Group
        public Task SetWeapon(string characterId, CardViewModel weapon); //lehet null
        public Task SetLifePoint(LifePointViewModel pointModel);
        public Task CardsDroped(List<CardIdViewModel> cards);
        public Task CardUnequiped(CardIdViewModel card);
        public Task CardEquiped(string characterId, CardViewModel card);
        public Task CardsAdded(List<CardIdViewModel> cards);
        public Task ShowCard(CardViewModel card);
        public Task SetGameEvent(GameEventViewModel gameEvent); // lehet null a kártya pl húzásnál.
        public Task MessageRecieved(MessageViewModel message);

        public Task WinnerIs(RoleType winnert);
        public Task PlayerDied(string userId);
        public Task OnStatus();

        //Client
        public Task CardsReceieved(List<CardViewModel> cards);
        public Task ShowOption(OptionViewModel option);
        public Task DrawOption(DrawOptionViewModel option);
        public Task GameJoined(GameStartViewModel start);

    }
}
