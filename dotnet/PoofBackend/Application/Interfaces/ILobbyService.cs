using Application.Models;
using Application.Models.ViewModels;
using Application.SignalR;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ILobbyService
    {
        public Task CreateLobbyAsync(Lobby lobby, PoofHub hub, CancellationToken cancellationToken = default);
        public Task ReconnectLobbyAsync(PoofHub hub, CancellationToken cancellationToken = default);
        public Task AddConnectionAsync(string name, Connection connection, PoofHub hub, CancellationToken cancellationToken = default);
        public Task SendMessageAsync(string name, string userId, Message message, PoofHub hub, CancellationToken cancellationToken = default);
        public Task<Lobby> GetLobbyAsync(string name, CancellationToken cancellationToken = default);
        public Task DeleteLobbyAsync(string name, string userName, PoofHub hub, CancellationToken cancellationToken = default);
        public Task RemoveConnectionAsync(string userId, PoofHub hub, CancellationToken cancellationToken = default);
        public Task DeletePlayerAsync(string userName, string deleteUserId, PoofHub hub, CancellationToken cancellationToken = default);
    }
}
