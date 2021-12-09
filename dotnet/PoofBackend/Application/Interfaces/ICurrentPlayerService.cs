using Application.Models;

namespace Application.Interfaces
{
    public interface ICurrentPlayerService
    {
        public CurrentPlayer Player { get; set; }
    }
}
