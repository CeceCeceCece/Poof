using Application.Constants;
using Application.Exceptions;
using Application.Interfaces;
using Application.ViewModels;
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
        private readonly ICurrentPlayerService playerService;

        public GameService(PoofDbContext context, ICurrentPlayerService playerService)
        {
            this.context = context;
            this.playerService = playerService;
        }

        public Task CreateGame(Lobby lobby, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Game> GetGameAsync(string groupId, CancellationToken cancellationToken)
        {
            return context.Games.Include(x => x.Messages).Include(x => x.Characters).SingleOrDefaultAsync(x => x.Id == groupId, cancellationToken);
        }

        public async Task<Game> RemoveGame(string groupId, CancellationToken cancellationToken)
        {
            var game = await GetGameAsync(groupId, cancellationToken);
            context.Messages.RemoveRange(game.Messages);
            context.Characters.RemoveRange(game.Characters);
            context.Games.Remove(game);
            await context.SaveChangesAsync(cancellationToken);
            return game;
        }

        public async Task SendMessage(string groupId, string connectionId, Message message, CancellationToken cancellationToken = default)
        {
            var game = await GetGameAsync(groupId, cancellationToken);
            if (game is null)
                throw new PoofException(GameMessages.JATEK_NEM_LETEZIK);

            var isMemeber = game.Characters.Any(x => x.ConnectionId == connectionId);
            if (!isMemeber)
                throw new PoofException(GameMessages.FELHASZNALO_NEM_A_JATEK_RESZE);

            game.Messages.Add(message);
            await context.SaveChangesAsync(cancellationToken);
        }

        private async Task<Option> DrawAsync(string gameId, CancellationToken cancellationToken) 
        {
            var game = await GetGameAsync(gameId, cancellationToken);
            var character = game.Characters.SingleOrDefault(x => x.Id == playerService.Player.Id);
            var logic = character.Map();

            var option = logic.Draw(game);
            await context.SaveChangesAsync(cancellationToken);

            return option;
        }

        private async Task DrawReactAsync(string gameId, CancellationToken cancellationToken)
        {
            var game = await GetGameAsync(gameId, cancellationToken);
            var character = game.Characters.SingleOrDefault(x => x.Id == playerService.Player.Id);
            var logic = character.Map();

            logic.DrawReact(game);
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
