using Application.Constants;
using Application.Exceptions;
using Application.Interfaces;
using Application.Services;
using Application.SignalR.ClientInterfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace Application.SignalR
{
    [Authorize]
    public class PoofHub : Hub<IPoofClient>
    {
        private ICurrentPlayerService currentPlayerService;
        private readonly ILobbyService lobbyService;
        private readonly IGameService gameService;

        public PoofHub(ILobbyService lobbyService, IGameService gameService)
        {
            this.lobbyService = lobbyService;
            this.gameService = gameService;
        }
        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
            currentPlayerService = new CurrentPlayerService(Context.GetHttpContext());
            await lobbyService.ReconnectLobbyAsync(this);
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            currentPlayerService = new CurrentPlayerService(Context.GetHttpContext());
            await DisconnectLobby();
            await base.OnDisconnectedAsync(exception);
        }

        public async Task CreateLobby(string name) 
        {
            currentPlayerService = new CurrentPlayerService(Context.GetHttpContext());
            var lobby = new Lobby(name, currentPlayerService.Player.Name);
            lobby.Connections.Add(new Connection(Context.ConnectionId, currentPlayerService.Player.Name, currentPlayerService.Player.Id));
            await lobbyService.CreateLobbyAsync(lobby, this);
        }

        public async Task CreateGame(string lobbyName)
        {
            currentPlayerService = new CurrentPlayerService(Context.GetHttpContext());
            var lobby = await lobbyService.GetLobbyAsync(lobbyName);

            if (lobby.Vezeto != currentPlayerService.Player.Name)
                throw new PoofException(LobbyMessages.ERRE_CSAK_A_SZOBA_TULAJDONOSA_JOGOSULT);

            if(lobby.Connections.Count >= 1) 
            {
                await gameService.CreateGameAsync(lobby, this);
                await lobbyService.DeleteLobbyAsync(lobbyName, currentPlayerService.Player.Name, null);
            }
                
        }

        public async Task JoinLobby(string name) 
        {
            currentPlayerService = new CurrentPlayerService(Context.GetHttpContext());
            await lobbyService.AddConnectionAsync(name, new Connection(Context.ConnectionId, currentPlayerService.Player.Name, currentPlayerService.Player.Id), this);
        }

        public async Task DisconnectLobby()
        {
            currentPlayerService = new CurrentPlayerService(Context.GetHttpContext());
            await lobbyService.RemoveConnectionAsync(currentPlayerService.Player.Id, this);
        }

        public async Task SendMessage(string message, string name) 
        {
            currentPlayerService = new CurrentPlayerService(Context.GetHttpContext());
            var messageInstance = new Message(Guid.NewGuid().ToString(), currentPlayerService.Player.Name, message, DateTime.Now);
            await lobbyService.SendMessageAsync(name, currentPlayerService.Player.Id, messageInstance, this);
        }

        public async Task DeletePlayer(string userId) 
        {
            currentPlayerService = new CurrentPlayerService(Context.GetHttpContext());
            await lobbyService.DeletePlayerAsync(currentPlayerService.Player.Name, userId, this);
        }
        
        public async Task DeleteLobby(string name) 
        {
            currentPlayerService = new CurrentPlayerService(Context.GetHttpContext());
            await lobbyService.DeleteLobbyAsync(name, currentPlayerService.Player.Name, this);
        }
        public async Task Status(string name) 
        {
            await Clients.Group(name).OnStatus();
        }
    }
}
