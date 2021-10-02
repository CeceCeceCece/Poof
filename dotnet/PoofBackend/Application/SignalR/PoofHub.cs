using Application.Interfaces;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.SignalR
{
    public class PoofHub : Hub
    {
        private readonly PoofTracker tracker;
        private readonly ICurrentPlayerService currentPlayerService;
        private readonly IGameService gameService;

        public PoofHub(PoofTracker tracker, ICurrentPlayerService currentPlayerService, IGameService gameService)
        {
            this.tracker = tracker;
            this.currentPlayerService = currentPlayerService;
            this.gameService = gameService;
        }
        public override async Task OnConnectedAsync()
        {
            var httpContext = Context.GetHttpContext();
            var gameName = httpContext.Request.Query["game"].ToString();
            await Groups.AddToGroupAsync(Context.ConnectionId, gameName);
            await tracker.UserConnected(currentPlayerService.Player.Id, Context.ConnectionId);

            await Clients.Groups(gameName).SendAsync("Player Joined", currentPlayerService.Player.Name);
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await base.OnDisconnectedAsync(exception);
            await tracker.UserDisconnected(currentPlayerService.Player.Id, Context.ConnectionId);
        }
    }
}
