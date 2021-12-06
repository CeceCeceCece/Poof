using Application.Interfaces;
using Application.Models.DTOs;
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
    public class PoofGameHub : Hub<IPoofGameClient>
    {
        private ICurrentPlayerService currentPlayerService;
        private readonly IGameService gameService;

        public PoofGameHub(IGameService gameService)
        {
            this.gameService = gameService;
            gameService.Hub = this;
        }
        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await base.OnDisconnectedAsync(exception);
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

        public async Task JoinGame(string gameId)
        {
            currentPlayerService = new CurrentPlayerService(Context.GetHttpContext());
            await gameService.JoinGameAsync(gameId, currentPlayerService.Player.Id);
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

        public async Task Status(string gameName)
        {
            await Clients.Group(gameName).OnStatus();
        }
    }
}
