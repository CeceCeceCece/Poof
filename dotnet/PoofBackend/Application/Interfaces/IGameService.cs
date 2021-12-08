using Application.Models;
using Application.Models.DTOs;
using Application.SignalR;
using Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IGameService
    {
        public PoofGameHub Hub { get; set; }
        public Task JoinGameAsync(string gameId, string userId, CancellationToken cancellationToken = default);
        public Task CreateGameAsync(Lobby lobby, PoofHub lobbyHub, CancellationToken cancellationToken = default);
        public Task RemoveGameAsync(string gameId, CancellationToken cancellationToken = default);
        public Task SendMessageAsync(string gameId, string playerId, Message message, CancellationToken cancellationToken = default);
        public Task DrawReactAsync(string gameId, string playerId, OptionDto dto, CancellationToken cancellationToken = default);
        public Task CardActivateAsync(string gameId, string playerId, string cardId, OptionDto dto, CancellationToken cancellationToken = default);
        public Task CardAnswearAsync(string gameId, string playerId, OptionDto dto, CancellationToken cancellationToken = default);
        public Task CardOptionAsync(string gameId, string playerId, string cardId, CancellationToken cancellationToken = default);
        public Task NextTurnAsync(string gameId, string playerId, CancellationToken cancellationToken = default);
        public Task DiscardAsync(string gameId, string playerId, List<string> cardIds, CancellationToken cancellationToken = default);
    }
}
