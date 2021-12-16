using Application.Models.DTOs;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUserService
    {
        public Task Register(RegisterDto dto, CancellationToken cancellationToken = default);
    }
}
