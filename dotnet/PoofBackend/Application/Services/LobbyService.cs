using Application.Constants;
using Application.Exceptions;
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
    public class LobbyService : ILobbyService
    {
        private readonly PoofDbContext context;

        public LobbyService(PoofDbContext context)
        {
            this.context = context;
        }

        public async Task CreateLobby(Lobby lobby, CancellationToken cancellationToken)
        {
            var hasLobby = await context.Lobbies.AnyAsync(x => x.Name == lobby.Name || x.Vezeto == lobby.Vezeto, cancellationToken);

            if (hasLobby)
                throw new PoofException(LobbyMessages.NEV_MAR_LETEZIK);

            await context.Lobbies.AddAsync(lobby, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
        }

        public Task<Lobby> GetLobby(string id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task RemoveLobby(string id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
