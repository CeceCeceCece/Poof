using Application.Models;
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
        public Task CreateLobby(Lobby lobby, CancellationToken cancellationToken = default);
        public Task AddConnection(string name, Connection connection, CancellationToken cancellationToken = default);
        public Task SendMessage(string name, string connectionId, Message message, CancellationToken cancellationToken = default);
        public Task<Lobby> GetLobby(string name, CancellationToken cancellationToken = default);
        public Task<Lobby> DeleteLobby(string name, string userName, CancellationToken cancellationToken = default);
        public Task<Lobby> RemoveConnection(string connectionId, CancellationToken cancellationToken = default);
    }
}
