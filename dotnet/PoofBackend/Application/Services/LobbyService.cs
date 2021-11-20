using Application.Constants;
using Application.Exceptions;
using Application.Interfaces;
using Application.Models.ViewModels;
using Application.SignalR;
using Domain;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
                await hub.Clients.All.LobbyCreated(new LobbyViewModel(lobby.Name, lobby.Vezeto));
            }
        }

        public async Task SendMessageAsync(string name, string userId, Message message, PoofHub hub, CancellationToken cancellationToken = default)
        {
            var lobby = await GetLobbyAsync(name, cancellationToken);
            if (lobby is null)
                throw new PoofException(LobbyMessages.LOBBY_NEM_LETEZIK);

            var isMemeber = lobby.Connections.Any(x => x.UserId == userId);
            if (!isMemeber)
                throw new PoofException(LobbyMessages.FELHASZNALO_NEM_A_LOBBY_RESZE);

            lobby.Messages.Add(message);
            await context.SaveChangesAsync(cancellationToken);

            if (hub is not null)
                await hub?.Clients.Group(lobby.Name).RecieveMessage(new MessageViewModel(message.Kuldo, message.Tartalom, message.Datum));
        }

        public async Task AddConnectionAsync(string name, Connection connection, PoofHub hub, CancellationToken cancellationToken = default)
        {
            var lobby = await GetLobbyAsync(name, cancellationToken);
            if (lobby is null)
                throw new PoofException(LobbyMessages.LOBBY_NEM_LETEZIK);

            lobby.Connections.Add(connection);
            await context.SaveChangesAsync(cancellationToken);

            if (hub is not null) 
            {
                await hub?.Clients.Group(lobby.Name).UserEntered(new UserViewModel(connection.UserId, connection.Username));
                await hub?.Groups.AddToGroupAsync(connection.ConnectionId, lobby.Name);
                await hub?.Clients.Client(connection.ConnectionId).SetMessages(lobby.Messages.Select(x => new MessageViewModel(x.Kuldo, x.Tartalom, x.Datum)).ToList());
                await hub?.Clients.Client(connection.ConnectionId).SetUsers(lobby.Connections.Select(x => new UserViewModel(x.UserId, x.Username)).ToList());
            }

        }

        public async Task<Lobby> GetLobbyAsync(string name, CancellationToken cancellationToken = default)
        {
            return await context.Lobbies.Include(x => x.Connections).Include(x => x.Messages).SingleOrDefaultAsync(x => x.Name == name, cancellationToken);
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
                await hub?.Clients.All.LobbyDeleted(lobby.Name);

                foreach (var conncetion in lobby.Connections)
                {
                    await hub?.Groups.RemoveFromGroupAsync(conncetion.ConnectionId, lobby.Name);
                }
            }
                
        }

        public async Task RemoveConnectionAsync(string userId, PoofHub hub, CancellationToken cancellationToken = default)
        {
            var lobby = await context.Lobbies.SingleOrDefaultAsync(x => x.Connections.Any(x => x.UserId == userId), cancellationToken);
            if (lobby is null)
                throw new PoofException(LobbyMessages.LOBBY_NEM_LETEZIK);

            var connection = lobby.Connections.FirstOrDefault(x => x.UserId == userId);

            if(lobby.Vezeto == connection.Username) 
            {
                context.Lobbies.Remove(lobby);
                context.Connections.RemoveRange(lobby.Connections);
                context.Messages.RemoveRange(lobby.Messages);

                if (hub is not null) 
                {
                    await hub?.Clients.Group(lobby.Name).LobbyDeleted(lobby.Name);

                    foreach (var conncetion in lobby.Connections)
                    {
                        await hub?.Groups.RemoveFromGroupAsync(conncetion.ConnectionId, lobby.Name);
                    }
                }
            }
            else 
            {
                context.Connections.Remove(connection);

                if (hub is not null) 
                {
                    await hub?.Clients.Group(lobby.Name).UserLeft(userId);
                    await hub?.Groups.RemoveFromGroupAsync(hub.Context.ConnectionId, lobby.Name);
                }
            
            }

            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
