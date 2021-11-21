using Domain.Entities;
using System;
using System.Threading.Tasks;

namespace Application.Models.GameModels.WinModels
{
    public class BasicWinLogic : IWinLogic
    {
        public Task CheckWinAsync(Game game)
        {
            throw new NotImplementedException();
        }
        public Task CheckDeadAsync(Game game)
        {
            throw new NotImplementedException();
        }
    }
}
