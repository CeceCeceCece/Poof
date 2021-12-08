using Application.Constants;
using Application.Exceptions;
using Application.Interfaces;
using Application.Models.DTOs;
using Application.Models.ViewModels;
using Application.SignalR;
using Application.ViewModels;
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
        private PoofDbContext context;
        public GameService(PoofDbContext context)
        {
            this.context = context;
        }
        public PoofGameHub Hub { get; set; }

        public async Task JoinGameAsync(string gameId, string userId, CancellationToken cancellationToken = default) 
        {
            var game = await GetGameAsync(gameId, cancellationToken);
            var character = game.Characters.SingleOrDefault(x => x.Id == userId) ?? throw new PoofException(GameMessages.FELHASZNALO_NEM_A_JATEK_RESZE);
            character.ConnectionId = Hub.Context.ConnectionId;
            await context.SaveChangesAsync(cancellationToken);
     
            if(Hub is not null) 
            {
                await Hub.Groups.AddToGroupAsync(Hub.Context.ConnectionId, game.Name);
                await Hub.Clients.Client(Hub.Context.ConnectionId).GameJoined(new GameStartViewModel 
                {
                    SelfId = character.Id,
                    Name = character.PersonalCard.Name,
                    Role = character.Role,
                    LifePoint = character.LifePoint,
                    SheriffId = game.Characters.Where(x => x.Role == RoleType.Sheriff).Select(x => x.Id).SingleOrDefault(),
                    Cards = character.Deck.Select(x => new CardViewModel(x.Id, x.Card.Name, x.Card.Type, x.Card.Suite, x.Card.Value)).ToList(),
                    Characters = game.Characters.Select(x => new CharacterViewModel 
                    {
                        UserId = x.Id,
                        UserName = x.Name,
                        Name = x.PersonalCard.Name,
                        LifePoint = x.LifePoint,
                        CardIds = x.Deck.Select(c => c.Id).ToList()
                    }).ToList()
                });

                if (character.Role == RoleType.Sheriff)
                {
                    await character.Map(Hub).DrawAsync();
                    await context.SaveChangesAsync(cancellationToken);
                }
            }
        }

        public async Task CreateGameAsync(Lobby lobby, PoofHub lobbyHub, CancellationToken cancellationToken = default)
        {
            var characters = await CreateCharactersAsync(lobby.Connections.ToList(), cancellationToken);
            var cards = await CreateCardsAsync(cancellationToken);
            cards.Shuffle();

            foreach (var character in characters)
            {
                character.Deck.AddRange(cards.GetRange(0,character.LifePoint));
                cards.RemoveRange(0, character.LifePoint);
            }

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
                    var sheriff = characterCards.ElementAt(characterIndexes[i]).ToCharacter(connections.ElementAt(indexes[i]), RoleType.Sheriff);
                    sheriff.LifePoint++;
                    sheriff.MaxLifePoint++;
                    result.Add(sheriff);
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
                    .ThenInclude(x => x.PersonalCard)
                .Include(x => x.Characters)
                    .ThenInclude(x => x.EquipedCards).ThenInclude(equiped => equiped.Card)
                .Include(x => x.Characters)
                    .ThenInclude(x => x.Weapon).ThenInclude(weapon => weapon.Card)
                .SingleOrDefaultAsync(x => x.Id == groupId, cancellationToken);
        }

        public async Task<Game> RemoveGameAsync(string gameId, CancellationToken cancellationToken = default)
        {
            var game = await GetGameAsync(gameId, cancellationToken);
            foreach (var character in game.Characters)
            {
                await Hub.Groups.RemoveFromGroupAsync(character.ConnectionId, game.Name, cancellationToken);
                context.GameCards.RemoveRange(character.Deck);
                context.GameCards.RemoveRange(character.EquipedCards);
                context.GameCards.Remove(character.Weapon);
            }
            context.Messages.RemoveRange(game.Messages);
            context.Characters.RemoveRange(game.Characters);
            context.GameCards.RemoveRange(game.Deck);
            context.GameCards.RemoveRange(game.DiscardPile);
            context.Games.Remove(game);
            context.Characters.RemoveRange(await context.Characters.Where(x => x.Game == null).ToListAsync(cancellationToken));
            await context.SaveChangesAsync(cancellationToken);
            return game;
        }

        public async Task SendMessageAsync(string gameId, string playerId , Message message, CancellationToken cancellationToken = default)
        {
            var game = await context.Games.Where(x => x.Characters.Any(c => c.Id == playerId) && x.Id == gameId).Include(x => x.Messages).SingleOrDefaultAsync(cancellationToken);
            if (game is null)
                throw new PoofException(GameMessages.JATEK_NEM_LETEZIK + " vagy " + GameMessages.FELHASZNALO_NEM_A_JATEK_RESZE);

            game.Messages.Add(message);
            await context.SaveChangesAsync(cancellationToken);

            if (Hub is not null)
                await Hub.Clients.Group(game.Name).MessageRecieved(new MessageViewModel(message.Kuldo, message.Tartalom, message.Datum));
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
            await context.SaveChangesAsync(cancellationToken);
        }

        public async Task CardActivateAsync(string gameId, string playerId, string cardId, OptionDto dto, CancellationToken cancellationToken = default)
        {
            var game = await GetGameAsync(gameId, cancellationToken);

            if (game.Event != GameEvent.None || game.CurrentUserId != playerId)
                throw new PoofException(GameMessages.NEM_JATSZHATSZ_KI_KARTYAT);

            await game.GetCharacterById(playerId).Map(Hub).ActivateCardAsync(cardId, dto);
            await CheckGameEnd(game);
            await context.SaveChangesAsync(cancellationToken);
        }

        public async Task CardAnswearAsync(string gameId, string playerId, OptionDto dto, CancellationToken cancellationToken = default)
        {
            var game = await GetGameAsync(gameId, cancellationToken);

            if (game.Event == GameEvent.None && ((game.Event == GameEvent.SingleReact && game.NextUserId != playerId) || (game.Event == GameEvent.CallerReact && game.CurrentUserId != playerId)))
                throw new PoofException(GameMessages.NEM_REAGALHATSZ);

            await game.GetCharacterById(playerId).Map(Hub).AnswearCardAsync(dto);
            await CheckGameEnd(game);
            await context.SaveChangesAsync(cancellationToken);
        }

        public async Task CheckGameEnd(Game game) 
        {
            if(game.Win == WinType.None) 
            {
                await RemoveGameAsync(game.Id);
            }
        }

        public async Task NextTurnAsync(string gameId, string playerId, CancellationToken cancellationToken = default)
        {
            var game = await GetGameAsync(gameId, cancellationToken);
            if (game.CurrentUserId != playerId)
                throw new PoofException(GameMessages.KOR_VEGE_NEM_LEHETSEGES);
            await game.EndTurnAsync(Hub);
            await context.SaveChangesAsync(cancellationToken);
        }

        public async Task DiscardAsync(string gameId, string playerId, List<string> cardIds, CancellationToken cancellationToken = default)
        {
            var game = await GetGameAsync(gameId, cancellationToken);
            if(game.CurrentUserId != playerId)
                throw new PoofException(GameMessages.NEM_DOHATSZ_EL_KARTYAT);
            await game.GetCurrentCharacter().Map(Hub).DropCardsFromDeckAsync(cardIds);
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
