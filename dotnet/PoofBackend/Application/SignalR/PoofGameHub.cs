using Application.Interfaces;
using Application.Models.DTOs;
using Application.Services;
using Application.SignalR.ClientInterfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.SignalR
{
    [Authorize]
    public class PoofGameHub : Hub<IPoofGameClient>
    {
        private ICurrentPlayerService currentPlayerService;
        private readonly IGameService GameService;

        public PoofGameHub(IGameService gameService)
        {
            this.GameService = gameService;
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
            await GameService.SendMessageAsync(gameId, currentPlayerService.Player.Id, new Message(Guid.NewGuid().ToString(), currentPlayerService.Player.Name, message, DateTime.Now));
        }

        public async Task DrawReact(string gameId, OptionDto option)
        {
            currentPlayerService = new CurrentPlayerService(Context.GetHttpContext());
            await GameService.DrawReactAsync(gameId, currentPlayerService.Player.Id, option);
        }

        public async Task JoinGame(string gameId)
        {
            currentPlayerService = new CurrentPlayerService(Context.GetHttpContext());
            await GameService.JoinGameAsync(gameId, currentPlayerService.Player.Id);
        }

        public async Task ActiveCard(string gameId, string cardId, OptionDto option)
        {
            currentPlayerService = new CurrentPlayerService(Context.GetHttpContext());
            await GameService.CardActivateAsync(gameId, currentPlayerService.Player.Id, cardId, option);
        }

        public async Task AnswearCard(string gameId, OptionDto option)
        {
            currentPlayerService = new CurrentPlayerService(Context.GetHttpContext());
            await GameService.CardAnswearAsync(gameId, currentPlayerService.Player.Id, option);
        }

        public async Task CardOption(string cardId, string gameId)
        {
            currentPlayerService = new CurrentPlayerService(Context.GetHttpContext());
            await GameService.CardOptionAsync(gameId, currentPlayerService.Player.Id, cardId);
        }

        public async Task NextTurn(string gameId)
        {
            currentPlayerService = new CurrentPlayerService(Context.GetHttpContext());
            await GameService.NextTurnAsync(gameId, currentPlayerService.Player.Id);
        }

        public async Task Discard(string gameId, List<string> cardIds)
        {
            currentPlayerService = new CurrentPlayerService(Context.GetHttpContext());
            await GameService.DiscardAsync(gameId, currentPlayerService.Player.Id, cardIds);
        }

        public async Task Status(string gameName)
        {
            await Clients.Group(gameName).OnStatus();
        }
    }
}
