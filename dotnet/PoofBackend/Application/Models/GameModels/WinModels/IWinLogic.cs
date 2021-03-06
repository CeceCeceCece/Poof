using Domain.Constants.Enums;
using Domain.Entities;
using System.Threading.Tasks;

namespace Application.Models.GameModels.WinModels
{
    public interface IWinLogic
    {
        public Task<bool> CheckWinAsync(Game game, out RoleType winner);
    }
}
