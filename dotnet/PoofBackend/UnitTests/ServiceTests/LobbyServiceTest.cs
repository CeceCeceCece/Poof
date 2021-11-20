using Application.Services;
using Domain;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

    }
}
