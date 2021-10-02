﻿using Application.Interfaces;
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
        public async Task AddGame(Group group, CancellationToken cancellationToken = default)
        {
            context.Games.Add(group);
            await context.SaveChangesAsync(cancellationToken);
        }

        public async Task<Connection> GetConnection(string conncetionId, CancellationToken cancellationToken = default)
        {
            return await context.Connections.FirstOrDefaultAsync(x => x.ConnectionId == conncetionId, cancellationToken);
        }

        public async Task<Group> GetGameGroup(string groupId, CancellationToken cancellationToken = default)
        {
            return await context.Games.FirstOrDefaultAsync(x => x.Name == groupId, cancellationToken);
        }

        public async Task RemoveConnection(Connection connection, CancellationToken cancellationToken = default)
        {
            context.Connections.Remove(connection);
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
