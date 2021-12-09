using Application.Models.ViewModels;
using Application.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.SignalR.ClientInterfaces
{
    public interface IPoofGameClient
    {
        //Group
        public Task SetDiscardPile(CardViewModel card);
        public Task SetWeapon(string characterId, CardViewModel weapon);
        public Task SetLifePoint(LifePointViewModel pointModel);
        public Task CardsDroped(List<CardIdViewModel> cards);
        public Task CardEquiped(string characterId, CardViewModel card);
        public Task CardsAdded(List<CardIdViewModel> cards);
        public Task ShowCard(CardViewModel card);
        public Task SetGameEvent(GameEventViewModel gameEvent);
        public Task MessageRecieved(MessageViewModel message);
        public Task WinnerIs(WinnerIsViewModel winner);
        public Task PlayerDied(CharacterDiedViewModel user);
        public Task TurnStarted(string userId);
        public Task OnStatus();

        //Client
        public Task CardsReceieved(List<CardViewModel> cards);
        public Task ShowOption(OptionViewModel option);
        public Task DrawOption(DrawOptionViewModel option);
        public Task GameJoined(GameStartViewModel start);

    }
}
