using Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IConnectionService
    {
        public Task RemoveConnection(Connection connection, CancellationToken cancellationToken = default);
        public Task<Connection> GetConnection(string conncetionId, CancellationToken cancellationToken = default);
    }
}
