using Application.Constants;
using Application.Exceptions;
using Application.Interfaces;
using Application.Models.ViewModels;
using Application.SignalR;
using Domain;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
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

        public async Task CreateLobbyAsync(Lobby lobby, PoofHub hub,  CancellationToken cancellationToken = default)
        {
            var hasLobby = await context.Lobbies.AnyAsync(x => x.Name == lobby.Name || x.Vezeto == lobby.Vezeto, cancellationToken);

            if (hasLobby)
                throw new PoofException(LobbyMessages.NEV_MAR_LETEZIK);

            await context.Lobbies.AddAsync(lobby, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);

            if(hub is not null) 
            {
                await hub.Groups.AddToGroupAsync(hub.Context.ConnectionId, lobby.Name);
                var viewModel = new LobbyViewModel(lobby.Name, lobby.Vezeto);
                viewModel.Users = lobby.Connections.Select(x => new UserViewModel(x.UserId, x.Username)).ToList();
                await hub.Clients.Caller.LobbyCreated(viewModel);
            }
        }

        public async Task ReconnectLobbyAsync(PoofHub hub, CancellationToken cancellationToken = default)
        {
            var lobby = await context.Lobbies.Include(x =>x.Connections).Include(x => x.Messages).SingleOrDefaultAsync(x => x.Connections.Any(c => c.ConnectionId == hub.Context.ConnectionId), cancellationToken);

            if (lobby is null)
                return;

            var connection = lobby.Connections.SingleOrDefault(x => x.ConnectionId == hub.Context.ConnectionId);
            connection.ConnectionId = hub.Context.ConnectionId;
            await context.SaveChangesAsync(cancellationToken);

            if (hub is not null)
            {
                await hub.Groups.AddToGroupAsync(connection.ConnectionId, lobby.Name);
                var viewModel = new LobbyViewModel(lobby.Name, lobby.Vezeto);
                viewModel.Users = lobby.Connections.Select(x => new UserViewModel(x.UserId, x.Username)).ToList();
                await hub.Clients.Client(connection.ConnectionId).LobbyJoined(viewModel);
                await hub.Clients.Client(connection.ConnectionId).SetMessages(lobby.Messages.Select(x => new MessageViewModel(x.Kuldo, x.Tartalom, x.Datum)).ToList());
            }
        }

        public async Task SendMessageAsync(string name, string userId, Message message, PoofHub hub, CancellationToken cancellationToken = default)
        {
            var lobby = await GetLobbyAsync(name, cancellationToken);

            var isMemeber = lobby.Connections.Any(x => x.UserId == userId);
            if (!isMemeber)
                throw new PoofException(LobbyMessages.FELHASZNALO_NEM_A_LOBBY_RESZE);

            lobby.Messages.Add(message);
            await context.SaveChangesAsync(cancellationToken);

            if (hub is not null)
                await hub.Clients.Group(lobby.Name).RecieveMessage(new MessageViewModel(message.Kuldo, message.Tartalom, message.Datum));
        }

        public async Task AddConnectionAsync(string name, Connection connection, PoofHub hub, CancellationToken cancellationToken = default)
        {
            var lobby = await GetLobbyAsync(name, cancellationToken);

            lobby.Connections.Add(connection);
            await context.SaveChangesAsync(cancellationToken);

            if (hub is not null) 
            {
                await hub.Clients.Group(lobby.Name).UserEntered(new UserViewModel(connection.UserId, connection.Username));
                await hub.Groups.AddToGroupAsync(connection.ConnectionId, lobby.Name);
                var viewModel = new LobbyViewModel(lobby.Name, lobby.Vezeto);
                viewModel.Users = lobby.Connections.Select(x => new UserViewModel(x.UserId, x.Username)).ToList();
                await hub.Clients.Client(connection.ConnectionId).LobbyJoined(viewModel);
                await hub.Clients.Client(connection.ConnectionId).SetMessages(lobby.Messages.Select(x => new MessageViewModel(x.Kuldo, x.Tartalom, x.Datum)).ToList());
            }

        }

        public async Task<Lobby> GetLobbyAsync(string name, CancellationToken cancellationToken = default)
        {
            var lobby = await context.Lobbies.Include(x => x.Connections).Include(x => x.Messages).SingleOrDefaultAsync(x => x.Name == name, cancellationToken);
            if(lobby is null)
                throw new PoofException(LobbyMessages.LOBBY_NEM_LETEZIK);
            return lobby;
        }

        public async Task DeleteLobbyAsync(string name, string userName, PoofHub hub, CancellationToken cancellationToken = default)
        {
            var lobby = await GetLobbyAsync(name, cancellationToken);
            if (lobby.Vezeto != userName)
                throw new PoofException(LobbyMessages.ERRE_CSAK_A_SZOBA_TULAJDONOSA_JOGOSULT);

            context.Connections.RemoveRange(lobby.Connections);
            context.Lobbies.Remove(lobby);
            context.Messages.RemoveRange(lobby.Messages);
            await context.SaveChangesAsync(cancellationToken);

            if (hub is not null) 
            {
                await hub.Clients.All.LobbyDeleted(lobby.Name);

                foreach (var conncetion in lobby.Connections)
                {
                    await hub.Groups.RemoveFromGroupAsync(conncetion.ConnectionId, lobby.Name);
                }
            }
                
        }

        public async Task RemoveConnectionAsync(string userId, PoofHub hub, CancellationToken cancellationToken = default)
        {
            var lobby = await context.Lobbies.Include(x => x.Connections).SingleOrDefaultAsync(x => x.Connections.Any(c => c.UserId == userId), cancellationToken);
            if (lobby is null)
                throw new PoofException(LobbyMessages.LOBBY_NEM_LETEZIK);

            var connection = lobby.Connections.FirstOrDefault(x => x.UserId == userId);

            if(lobby.Vezeto == connection.Username) 
            {
                context.Connections.RemoveRange(lobby.Connections);
                context.Messages.RemoveRange(lobby.Messages);
                context.Lobbies.Remove(lobby);

                if (hub is not null) 
                {
                    await hub.Clients.Group(lobby.Name).LobbyDeleted(lobby.Name);

                    foreach (var conncetion in lobby.Connections)
                    {
                        await hub.Groups.RemoveFromGroupAsync(conncetion.ConnectionId, lobby.Name);
                    }
                }
            }
            else 
            {
                context.Connections.Remove(connection);

                if (hub is not null) 
                {
                    await hub.Clients.Group(lobby.Name).UserLeft(userId);
                    await hub.Groups.RemoveFromGroupAsync(connection.ConnectionId, lobby.Name, cancellationToken);
                }
            
            }

            await context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeletePlayerAsync(string userName, string deleteUserId, PoofHub hub, CancellationToken cancellationToken = default)
        {
            if(await context.Lobbies.AnyAsync(x => x.Vezeto == userName && x.Connections.Any(x => x.UserId == deleteUserId && x.Username != userName), cancellationToken)) 
            {
                await RemoveConnectionAsync(deleteUserId, hub, cancellationToken);
            }
        }
    }
}
