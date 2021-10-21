using Application.Interfaces;
using Domain;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services
{
    public class GameService : IGameService
    {
        private readonly PoofDbContext context;

        public GameService(PoofDbContext context)
        {
            this.context = context;
        }

        public Task CreateGame(Lobby lobby, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Lobby> GetGame(string groupId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Lobby> RemoveGame(string groupId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
