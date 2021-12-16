using Application.Services;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnitTests.ServiceTests.Helpers;
using Xunit;

namespace UnitTests.ServiceTests
{
    public class LobbyServiceTest
    {
        [Fact]
        public async Task CreateLobbyAsyncTest()
        {
            //Arrange
            var context = new ConnectionFactory().CreateContextForSQLite();

            var service = new LobbyService(context);

            //Act
            await service.CreateLobbyAsync(new Lobby
            {
                Name = "Test",
                Vezeto = "TestUserId"
            }, null);

            var lobby = await service.GetLobbyAsync("Test");
            //Result

            Assert.NotNull(lobby);
            Assert.Equal("Test", lobby.Name);
            Assert.Equal("TestUserId", lobby.Vezeto);
        }

        [Fact]
        public async Task AddConnectionAsyncTest()
        {
            //Arrange
            var context = new ConnectionFactory().CreateContextForSQLite();

            var service = new LobbyService(context);

            //Act
            await service.CreateLobbyAsync(new Lobby
            {
                Name = "Test",
                Vezeto = "TestUserId"
            }, null);

            await service.AddConnectionAsync("Test", new Connection("testId", "testName", "testUserId"), null);

            var lobby = await service.GetLobbyAsync("Test");
            var connection = lobby.Connections.FirstOrDefault();

            //Result
            Assert.Single(lobby.Connections);
            Assert.NotNull(connection);
            Assert.Equal("testUserId", connection.UserId);
            Assert.Equal("testName", connection.Username);
            Assert.Equal("testId", connection.ConnectionId);
        }

        [Fact]
        public async Task SendMessageAsyncTest()
        {
            //Arrange
            var context = new ConnectionFactory().CreateContextForSQLite();

            var service = new LobbyService(context);

            //Act
            await service.CreateLobbyAsync(new Lobby
            {
                Name = "Test",
                Vezeto = "TestUserId",
                Connections = new List<Connection> { new Connection("testId", "testName", "testUserId") }
            }, null);

            await service.SendMessageAsync("Test", "testUserId", new Message("messageId", "testName", "Hello", DateTime.Now), null);

            var lobby = await service.GetLobbyAsync("Test");
            var message = lobby.Messages.FirstOrDefault();

            //Result
            Assert.Single(lobby.Connections);
            Assert.NotNull(message);
            Assert.Equal("messageId", message.Id);
            Assert.Equal("testName", message.Kuldo);
            Assert.Equal("Hello", message.Tartalom);
        }

        [Fact]
        public async Task RemoveConnectionAsyncTest()
        {
            //Arrange
            var context = new ConnectionFactory().CreateContextForSQLite();

            var service = new LobbyService(context);

            //Act
            await service.CreateLobbyAsync(new Lobby
            {
                Name = "Test",
                Vezeto = "TestUserId",
                Connections = new List<Connection> { new Connection("testId", "testName", "testUserId") }
            }, null);

            await service.RemoveConnectionAsync("testUserId", null);

            var lobby = await service.GetLobbyAsync("Test");

            var connections = await context.Connections.ToListAsync();
            //Result
            Assert.Empty(lobby.Connections);
            Assert.Empty(connections);
        }

        [Fact]
        public async Task DeletLobbyAsyncTest()
        {
            //Arrange
            var context = new ConnectionFactory().CreateContextForSQLite();

            var service = new LobbyService(context);

            //Act
            await service.CreateLobbyAsync(new Lobby
            {
                Name = "Test",
                Vezeto = "TestUserId",
                Connections = new List<Connection> { new Connection("testId", "testName", "testUserId") },
                Messages = new List<Message> { new Message("id", "kuldo", "hello", DateTime.Now) }
            }, null);

            await service.DeleteLobbyAsync("Test", "TestUserId", null);

            var lobbys = await context.Lobbies.ToListAsync();
            var connections = await context.Connections.ToListAsync();
            var messages = await context.Messages.ToListAsync();

            //Result
            Assert.Empty(lobbys);
            Assert.Empty(connections);
            Assert.Empty(messages);
        }
    }
}
