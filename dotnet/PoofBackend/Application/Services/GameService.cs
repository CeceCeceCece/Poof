using Application.Constants;
using Application.Exceptions;
using Application.Interfaces;
using Application.Models.DTOs;
using Application.SignalR;
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

        public PoofGameHub Hub { get; set; }

        public Task CreateGame(Lobby lobby, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Game> GetGameAsync(string groupId, CancellationToken cancellationToken)
        {
            return context.Games.Include(x => x.Deck).Include(x => x.Messages).Include(x => x.Characters).ThenInclude(x => x.Deck).SingleOrDefaultAsync(x => x.Id == groupId, cancellationToken);
        }

        public async Task<Game> RemoveGame(string groupId, CancellationToken cancellationToken)
        {
            var game = await GetGameAsync(groupId, cancellationToken);
            context.Messages.RemoveRange(game.Messages);
            context.Characters.RemoveRange(game.Characters);
            context.GameCards.RemoveRange(game.Deck);
            context.GameCards.RemoveRange(game.DiscardPile);
            context.Games.Remove(game);
            await context.SaveChangesAsync(cancellationToken);
            return game;
        }

        public async Task SendMessage(string gameId, Message message, CancellationToken cancellationToken = default)
        {
            var game = await context.Games.Include(x => x.Messages).SingleOrDefaultAsync(x => x.Id == gameId);
            if (game is null)
                throw new PoofException(GameMessages.JATEK_NEM_LETEZIK);

            var isMemeber = game.Characters.Any(x => x.Id == playerService.Player.Id);
            if (!isMemeber)
                throw new PoofException(GameMessages.FELHASZNALO_NEM_A_JATEK_RESZE);

            game.Messages.Add(message);
            await context.SaveChangesAsync(cancellationToken);

            //TODO: hub ertesítés az üzenetről
        }

        private async Task DrawAsync(string gameId, CancellationToken cancellationToken) 
        {
            var game = await GetGameAsync(gameId, cancellationToken);
            await game.GetCharacterById(playerService.Player.Id).Map(Hub).DrawAsync();
            await context.SaveChangesAsync(cancellationToken);
        }

        public async Task DrawReactAsync(string gameId, OptionDto dto, CancellationToken cancellationToken)
        {
            var game = await GetGameAsync(gameId, cancellationToken);
            await game.GetCharacterById(playerService.Player.Id).Map(Hub).DrawReactAsync(dto);
            await context.SaveChangesAsync(cancellationToken);
        }

        public async Task CardOptionAsync(string gameId, string cardId, CancellationToken cancellationToken) 
        {
            var game = await GetGameAsync(gameId, cancellationToken);
            await game.GetCharacterById(playerService.Player.Id).Map(Hub).CardOptionAsync(cardId);
        }

        public async Task CardActivateAsync(string gameId, string cardId, OptionDto dto, CancellationToken cancellationToken)
        {
            var game = await GetGameAsync(gameId, cancellationToken);
            await game.GetCharacterById(playerService.Player.Id).Map(Hub).ActivateCardAsync(cardId, dto);
            await context.SaveChangesAsync(cancellationToken);
        }

        public async Task CardAnswearAsync(string gameId, OptionDto dto, CancellationToken cancellationToken)
        {
            var game = await GetGameAsync(gameId, cancellationToken);
            await game.GetCharacterById(playerService.Player.Id).Map(Hub).AnswearCardAsync(dto);
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
