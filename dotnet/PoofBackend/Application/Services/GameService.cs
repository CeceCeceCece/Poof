using Application.Constants;
using Application.Exceptions;
using Application.Interfaces;
using Application.Models.DTOs;
using Application.Models.ViewModels;
using Application.SignalR;
using Domain;
using Domain.Constants.Enums;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public PoofGameHub Hub { get; set; }

        public async Task CreateGameAsync(Lobby lobby, PoofHub lobbyHub, CancellationToken cancellationToken = default)
        {
            var characters = await CreateCharactersAsync(lobby.Connections.ToList(), cancellationToken);
            var cards = await CreateCardsAsync(cancellationToken);

            var game = new Game
            {
                Id = Guid.NewGuid().ToString(),
                DiscardPile = new List<GameCard>(),
                Event = GameEvent.None,
                Win = WinType.BasicBang,
                Messages = new List<Message>(),
                Name = lobby.Name,
                NextCard = null,
                NextUserId = null,
                Characters = characters,
                CurrentUserId = characters.SingleOrDefault(x => x.Role == RoleType.Sheriff).Id,
                Deck = cards
            };

            context.Games.Add(game);
            await context.SaveChangesAsync(cancellationToken);

            if(lobbyHub is not null)
                await lobbyHub.Clients.Group(lobby.Name).GameCreated(game.Id);
        }

        private async Task<List<GameCard>> CreateCardsAsync(CancellationToken cancellationToken = default)
        {
            return await context.Cards.Select(x => new GameCard(Guid.NewGuid().ToString(), x)).ToListAsync(cancellationToken);
        }

        private async Task<List<Character>> CreateCharactersAsync(List<Connection> connections, CancellationToken cancellationToken = default) 
        {
            var characterCards = await context.CharacterCards.ToListAsync(cancellationToken);
            var indexes = GenerateNumbers(connections.Count, connections.Count);
            var characterIndexes = GenerateNumbers(characterCards.Count, connections.Count);

            List<Character> result = new List<Character>();

            for (int i = 0; i < connections.Count; i++)
            {
                if (i == 0)
                {
                    result.Add(characterCards.ElementAt(characterIndexes[i]).ToCharacter(connections.ElementAt(indexes[i]), RoleType.Sheriff));
                }
                else if (i == 1)
                {
                    result.Add(characterCards.ElementAt(characterIndexes[i]).ToCharacter(connections.ElementAt(indexes[i]), RoleType.Renegade));
                }
                else 
                {
                    if(i % 2 == 0 )
                        result.Add(characterCards.ElementAt(characterIndexes[i]).ToCharacter(connections.ElementAt(indexes[i]), RoleType.Outlaw));
                    else
                        result.Add(characterCards.ElementAt(characterIndexes[i]).ToCharacter(connections.ElementAt(indexes[i]), RoleType.DeputySheriff));
                }
            }

            return result;
        }

        private List<int> GenerateNumbers(int max, int count)
        {
            List<int> result = new List<int>();
            var rnd = new Random();
            while (count != 0)
            {
                var number = rnd.Next(max);
                if (!result.Contains(number))
                {
                    count--;
                    result.Add(number);
                }
            }
            return result;
        }

        public Task<Game> GetGameAsync(string groupId, CancellationToken cancellationToken = default)
        {
            return context.Games.Include(x => x.Deck).ThenInclude(deck => deck.Card)
                .Include(x => x.DiscardPile).ThenInclude(disc => disc.Card)
                .Include(X => X.NextCard).ThenInclude(next => next.Card)
                .Include(x => x.Messages)
                .Include(x => x.Characters)
                    .ThenInclude(x => x.Deck).ThenInclude(deck => deck.Card)
                .Include(x => x.Characters)
                    .ThenInclude(x => x.Role)
                .Include(x => x.Characters)
                    .ThenInclude(x => x.EquipedCards).ThenInclude(equiped => equiped.Card)
                .Include(x => x.Characters)
                    .ThenInclude(x => x.Weapon).ThenInclude(weapon => weapon.Card)
                .SingleOrDefaultAsync(x => x.Id == groupId, cancellationToken);
        }

        public async Task<Game> RemoveGame(string groupId, CancellationToken cancellationToken = default)
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

        public async Task SendMessageAsync(string gameId, string playerId , Message message, CancellationToken cancellationToken = default)
        {
            var game = await context.Games.Where(x => x.Characters.Any(c => c.Id == playerId)).Include(x => x.Messages).SingleOrDefaultAsync(x => x.Id == gameId);
            if (game is null)
                throw new PoofException(GameMessages.JATEK_NEM_LETEZIK + " vagy " + GameMessages.FELHASZNALO_NEM_A_JATEK_RESZE);

            game.Messages.Add(message);
            await context.SaveChangesAsync(cancellationToken);

            if (Hub is not null)
                await Hub.Clients.Group(game.Name).MessageReceieved(new MessageViewModel(message.Kuldo, message.Tartalom, message.Datum));
        }

        private async Task DrawAsync(string gameId, string playerId, CancellationToken cancellationToken) 
        {
            var game = await GetGameAsync(gameId, cancellationToken);
            await game.GetCharacterById(playerId).Map(Hub).DrawAsync();
            await context.SaveChangesAsync(cancellationToken);
        }

        public async Task DrawReactAsync(string gameId, string playerId, OptionDto dto, CancellationToken cancellationToken = default)
        {
            var game = await GetGameAsync(gameId, cancellationToken);

            if (game.Event != GameEvent.Draw || game.CurrentUserId != playerId)
                throw new PoofException(GameMessages.MOST_NEM_LEHET_LAPOT_HUZNI);

            await game.GetCharacterById(playerId).Map(Hub).DrawReactAsync(dto);
            await context.SaveChangesAsync(cancellationToken);
        }

        public async Task CardOptionAsync(string gameId, string playerId, string cardId, CancellationToken cancellationToken = default) 
        {
            var game = await GetGameAsync(gameId, cancellationToken);

            if (game.Event != GameEvent.None || game.CurrentUserId != playerId)
                throw new PoofException(GameMessages.NEM_JATSZHATSZ_KI_KARTYAT);

            await game.GetCharacterById(playerId).Map(Hub).CardOptionAsync(cardId);
        }

        public async Task CardActivateAsync(string gameId, string playerId, string cardId, OptionDto dto, CancellationToken cancellationToken = default)
        {
            var game = await GetGameAsync(gameId, cancellationToken);

            if (game.Event != GameEvent.None || game.CurrentUserId != playerId)
                throw new PoofException(GameMessages.NEM_JATSZHATSZ_KI_KARTYAT);

            await game.GetCharacterById(playerId).Map(Hub).ActivateCardAsync(cardId, dto);
            await context.SaveChangesAsync(cancellationToken);
        }

        public async Task CardAnswearAsync(string gameId, string playerId, OptionDto dto, CancellationToken cancellationToken = default)
        {
            var game = await GetGameAsync(gameId, cancellationToken);

            if (game.Event == GameEvent.None && ((game.Event == GameEvent.SingleReact && game.NextUserId != playerId) || (game.Event == GameEvent.CallerReact && game.CurrentUserId != playerId)))
                throw new PoofException(GameMessages.NEM_REAGALHATSZ);

            await game.GetCharacterById(playerId).Map(Hub).AnswearCardAsync(dto);
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
