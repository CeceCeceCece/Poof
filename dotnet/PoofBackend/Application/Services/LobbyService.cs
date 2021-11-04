using Application.Constants;
using Application.Exceptions;
using Application.Interfaces;
using Application.Models.ViewModels;
using AutoMapper;
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
        private readonly IMapper mapper;

        public LobbyService(PoofDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task CreateLobby(Lobby lobby, CancellationToken cancellationToken = default)
        {
            var hasLobby = await context.Lobbies.AnyAsync(x => x.Name == lobby.Name || x.Vezeto == lobby.Vezeto, cancellationToken);

            if (hasLobby)
                throw new PoofException(LobbyMessages.NEV_MAR_LETEZIK);

            await context.Lobbies.AddAsync(lobby, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
        }

        public async Task SendMessage(string name, string connectionId, Message message, CancellationToken cancellationToken = default)
        {
            var lobby = await GetLobby(name, cancellationToken);
            if (lobby is null)
                throw new PoofException(LobbyMessages.LOBBY_NEM_LETEZIK);

            var isMemeber = lobby.Connections.Any(x => x.ConnectionId == connectionId);
            if (!isMemeber)
                throw new PoofException(LobbyMessages.FELHASZNALO_NEM_A_LOBBY_RESZE);

            lobby.Messages.Add(message);
            await context.SaveChangesAsync(cancellationToken);
        }

        public async Task AddConnection(string name, Connection connection, CancellationToken cancellationToken = default)
        {
            var lobby = await GetLobby(name, cancellationToken);
            if (lobby is null)
                throw new PoofException(LobbyMessages.LOBBY_NEM_LETEZIK);

            lobby.Connections.Add(connection);
            await context.SaveChangesAsync(cancellationToken);
        }

        public async Task<Lobby> GetLobby(string name, CancellationToken cancellationToken = default)
        {
            return await context.Lobbies.SingleOrDefaultAsync(x => x.Name == name, cancellationToken);
        }

        public async Task<Lobby> DeleteLobby(string name, string userName, CancellationToken cancellationToken = default)
        {
            var lobby = await GetLobby(name, cancellationToken);
            if (lobby.Vezeto != userName)
                throw new PoofException(LobbyMessages.ERRE_CSAK_A_SZOBA_TULAJDONOSA_JOGOSULT);

            context.Connections.RemoveRange(lobby.Connections);
            context.Lobbies.Remove(lobby);
            context.Messages.RemoveRange(lobby.Messages);
            await context.SaveChangesAsync(cancellationToken);

            return lobby;
        }

        public async Task<Lobby> RemoveConnection(string connectionId, CancellationToken cancellationToken = default)
        {
            var lobby = await context.Lobbies.SingleOrDefaultAsync(x => x.Connections.Any(x => x.ConnectionId == connectionId), cancellationToken);
            if (lobby is null)
                throw new PoofException(LobbyMessages.LOBBY_NEM_LETEZIK);

            var connection = lobby.Connections.FirstOrDefault(x => x.ConnectionId == connectionId);

            if(lobby.Vezeto == connection.Username) 
            {
                context.Lobbies.Remove(lobby);
                context.Connections.RemoveRange(lobby.Connections);
                context.Messages.RemoveRange(lobby.Messages);
            }
            else 
            {
                context.Connections.Remove(connection);
            }

            await context.SaveChangesAsync(cancellationToken);
            return lobby;
        }

        public async Task<List<LobbyViewModel>> GetAllLobby(CancellationToken cancellationToken = default)
        {
            return await mapper.ProjectTo<LobbyViewModel>(context.Lobbies).ToListAsync(cancellationToken);
        }

        public async Task<List<MessageViewModel>> GetMessages(string name, CancellationToken cancellationToken = default)
        {
            return await mapper.ProjectTo<MessageViewModel>(context.Lobbies.Where(x => x.Name == name).Select(x => x.Messages)).ToListAsync(cancellationToken);
        }
    }
}
