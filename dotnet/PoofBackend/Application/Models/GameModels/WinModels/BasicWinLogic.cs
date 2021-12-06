using Domain.Constants.Enums;
using Domain.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Models.GameModels.WinModels
{
    public class BasicWinLogic : IWinLogic
    {
        public Task<bool> CheckWinAsync(Game game, out RoleType winner)
        {
            var renegate = game.Characters.SingleOrDefault(x => x.Role == RoleType.Renegade);
            var outlaws = game.Characters.Where(x => x.Role == RoleType.Outlaw).ToList();
            var deputySheriffs = game.Characters.Where(x => x.Role == RoleType.DeputySheriff).ToList();
            var Sheriff = game.Characters.SingleOrDefault(x => x.Role == RoleType.Sheriff);
            
            if(Sheriff is null && outlaws.Count == 0 && deputySheriffs.Count == 0) 
            {
                winner = RoleType.Renegade;
                return Task.FromResult(true);
            }
            
            if(Sheriff is null) 
            {
                winner = RoleType.Outlaw;
                return Task.FromResult(true);
            }
            
            if(outlaws.Count == 0 && renegate is null) 
            {
                winner = RoleType.Sheriff;
                return Task.FromResult(true);
            }

            winner = RoleType.None;
            return Task.FromResult(false);
        }
    }
}
