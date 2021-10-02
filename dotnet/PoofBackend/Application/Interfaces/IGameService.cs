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
        public Task AddGame(Group group, CancellationToken cancellationToken);
        public Task RemoveConnection(Connection connection, CancellationToken cancellationToken);
        public Task<Connection> GetConnection(string conncetionId, CancellationToken cancellationToken);
        public Task<Group> GetGameGroup(string groupId, CancellationToken cancellationToken);
    }
}
