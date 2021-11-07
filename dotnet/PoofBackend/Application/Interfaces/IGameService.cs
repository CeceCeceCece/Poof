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
    public interface IGameService
    {
        public Task CreateGame(Lobby lobby, CancellationToken cancellationToken);
        public Task<Game> GetGameAsync(string groupId, CancellationToken cancellationToken);
        public Task<Game> RemoveGame(string groupId, CancellationToken cancellationToken);
    }
}
