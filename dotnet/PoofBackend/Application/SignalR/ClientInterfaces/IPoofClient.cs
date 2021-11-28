using Application.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SignalR.ClientInterfaces
{
    public interface IPoofClient
    {
        public Task LobbyCreated(LobbyViewModel lobby);
        public Task LobbyJoined(LobbyViewModel lobby);
        public Task LobbyDeleted(string name);
        public Task SetUsers(List<UserViewModel> users);
        public Task SetMessages(List<MessageViewModel> messages);
        public Task UserEntered(UserViewModel user);
        public Task UserLeft(string userId);
        public Task RecieveMessage(MessageViewModel message);
        public Task GameCreated(string gameId);
        public Task OnStatus();
    }
}
