using Application.Interfaces;
using Application.Models.DTOs;
using Application.Services;
using Application.SignalR.ClientInterfaces;
using Domain.Entities;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace Application.SignalR
{
    public class PoofGameHub : Hub<IPoofGameClient>
    {
        private readonly PoofTracker tracker;
        private ICurrentPlayerService currentPlayerService;
        private readonly IGameService gameService;

        public PoofGameHub(PoofTracker tracker, IGameService gameService)
        {
            this.tracker = tracker;
            //Létrehpzni a playert servicet

            this.gameService = gameService;
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

        public async Task SendMessage(string gameId, string message)
        {
            currentPlayerService = new CurrentPlayerService(Context.GetHttpContext());
            await gameService.SendMessageAsync(gameId, currentPlayerService.Player.Id, new Message(Guid.NewGuid().ToString(), currentPlayerService.Player.Name, message, DateTime.Now));
        }
        
        public async Task DrawReact(string gameId, OptionDto option)
        {
            currentPlayerService = new CurrentPlayerService(Context.GetHttpContext());
            await gameService.DrawReactAsync(gameId, currentPlayerService.Player.Id, option);
        }
        
        public async Task ActiveCard(string gameId, string cardId, OptionDto option)
        {
            currentPlayerService = new CurrentPlayerService(Context.GetHttpContext());
            await gameService.CardActivateAsync(gameId, currentPlayerService.Player.Id, cardId, option);
        }
        
        public async Task AnswearCard(string gameId, OptionDto option)
        {
            currentPlayerService = new CurrentPlayerService(Context.GetHttpContext());
            await gameService.CardAnswearAsync(gameId, currentPlayerService.Player.Id, option);
        }
    }
}
