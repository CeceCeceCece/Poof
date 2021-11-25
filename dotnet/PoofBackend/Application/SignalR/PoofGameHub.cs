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
    public class PoofGameHub : Hub<IPoofGameClient>
    {
        private readonly PoofTracker tracker;
        private readonly ICurrentPlayerService currentPlayerService;
        private readonly ILobbyService lobbyService;

        public PoofGameHub(PoofTracker tracker, ILobbyService lobbyService)
        {
            this.tracker = tracker;
            //Létrehpzni a playert servicet

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
            await base.OnDisconnectedAsync(exception);
            await tracker.UserDisconnected(currentPlayerService.Player.Id, Context.ConnectionId);
        }
    }
}
