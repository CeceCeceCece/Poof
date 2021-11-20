using Application.Interfaces;
using Application.Models.ViewModels;
using Application.SignalR.ClientInterfaces;
using Domain.Entities;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.SignalR
{
    public class PoofHub : Hub<IPoofClient>
    {
        private readonly PoofTracker tracker;
        private readonly ICurrentPlayerService currentPlayerService;
        private readonly ILobbyService lobbyService;

        public PoofHub(PoofTracker tracker, ICurrentPlayerService currentPlayerService, ILobbyService lobbyService)
        {
            this.tracker = tracker;
            this.currentPlayerService = currentPlayerService;
            this.lobbyService = lobbyService;
        }
        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
            await tracker.UserConnected(currentPlayerService.Player.Id, Context.ConnectionId);
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await DisconnectLobby();
            await base.OnDisconnectedAsync(exception);
            await tracker.UserDisconnected(currentPlayerService.Player.Id, Context.ConnectionId);
        }

        public async Task CreateLobby(string name) 
        {
            var lobby = new Lobby(name, currentPlayerService.Player.Name);
            lobby.Connections.Add(new Connection(Context.ConnectionId, currentPlayerService.Player.Name, currentPlayerService.Player.Id));
            await lobbyService.CreateLobbyAsync(lobby, this);
        }

        public async Task JoinLobby(string name) 
        {
            await lobbyService.AddConnectionAsync(name, new Connection(Context.ConnectionId, currentPlayerService.Player.Name, currentPlayerService.Player.Id), this);
        }

        public async Task DisconnectLobby()
        {
            await lobbyService.RemoveConnectionAsync(currentPlayerService.Player.Id, this);
        }

        public async Task SendMessage(string message, string name) 
        {
            var messageInstance = new Message(Guid.NewGuid().ToString(), currentPlayerService.Player.Name, message, DateTime.Now);
            await lobbyService.SendMessageAsync(name, currentPlayerService.Player.Id, messageInstance, this);
        }

        public async Task DeleteLobby(string name) 
        {
            await lobbyService.DeleteLobbyAsync(name, currentPlayerService.Player.Name, this);
        }
    }
}
