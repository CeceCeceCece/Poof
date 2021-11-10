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
    public class PoofGameHub : Hub<IPoofClient>
    {
        private readonly PoofTracker tracker;
        private readonly ICurrentPlayerService currentPlayerService;
        private readonly ILobbyService lobbyService;

        public PoofGameHub(PoofTracker tracker, ICurrentPlayerService currentPlayerService, ILobbyService lobbyService)
        {
            this.tracker = tracker;
            this.currentPlayerService = currentPlayerService;
            this.lobbyService = lobbyService;
        }
        public override async Task OnConnectedAsync()
        {
            //var httpContext = Context.GetHttpContext();
            //var gameName = httpContext.Request.Query["game"].ToString();
            //await Groups.AddToGroupAsync(Context.ConnectionId, gameName);
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
            await lobbyService.CreateLobby(lobby);

            var lobbyModel = new LobbyViewModel(lobby.Name, lobby.Vezeto);
            lobbyModel.Users.Add(new UserViewModel(currentPlayerService.Player.Id, currentPlayerService.Player.Name));
            await Clients.All.LobbyCreated(lobbyModel);
        }

        public async Task JoinLobby(string name) 
        {
            await lobbyService.AddConnection(name, new Connection(Context.ConnectionId, currentPlayerService.Player.Name, currentPlayerService.Player.Id));
            await Clients.Groups(name).UserEntered(new UserViewModel(currentPlayerService.Player.Id, currentPlayerService.Player.Name));
            await Groups.AddToGroupAsync(Context.ConnectionId, name);

            var lobby = await lobbyService.GetLobby(name);
            await Clients.Caller.SetUsers(lobby.Connections.Select(x => new UserViewModel(x.UserId, x.Username)).ToList());
            await Clients.Caller.SetMessages(lobby.Messages.Select(x => new MessageViewModel(x.Kuldo, x.Tartalom, x.Datum)).ToList());
        }

        public async Task DisconnectLobby()
        {
            var lobby = await lobbyService.RemoveConnection(Context.ConnectionId);

            if(currentPlayerService.Player.Name == lobby.Vezeto) 
            {
                await Clients.All.LobbyDeleted(lobby.Name);
                foreach(var conncetion in lobby.Connections) 
                {
                    await Groups.RemoveFromGroupAsync(conncetion.ConnectionId, lobby.Name);
                }
            }
            else 
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, lobby.Name);
                await Clients.Group(lobby.Name).UserLeft(currentPlayerService.Player.Id);
            }
        }

        public async Task SendMessage(string message, string name) 
        {
            var messageInstance = new Message(Guid.NewGuid().ToString(), currentPlayerService.Player.Name, message, DateTime.Now);
            await lobbyService.SendMessage(name, Context.ConnectionId, messageInstance);
            await Clients.Groups(name).RecieveMessage(new MessageViewModel(currentPlayerService.Player.Name, message, messageInstance.Datum));
        }

        public async Task DeleteLobby(string name) 
        {
            var lobby = await lobbyService.DeleteLobby(name, currentPlayerService.Player.Name);
            await Clients.All.LobbyDeleted(lobby.Name);
            foreach (var conncetion in lobby.Connections)
            {
                await Groups.RemoveFromGroupAsync(conncetion.ConnectionId, lobby.Name);
            }
        }
    }
}
