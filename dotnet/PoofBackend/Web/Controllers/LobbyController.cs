using Application.Interfaces;
using Application.Models.ViewModels;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LobbyController : ControllerBase
    {
        private readonly ILobbyService lobbyService;
        private readonly ICurrentPlayerService currentPlayerService;
        private readonly IMapper mapper;

        public LobbyController(ILobbyService lobbyService, ICurrentPlayerService currentPlayerService, IMapper mapper)
        {
            this.lobbyService = lobbyService;
            this.currentPlayerService = currentPlayerService;
            this.mapper = mapper;
        }
        // GET: api/<LobbyController>
        [HttpGet]
        public async Task<List<LobbyViewModel>> GetList()
        {
            return await lobbyService.GetAllLobby();
        }

        // GET api/<LobbyController>/5
        [HttpGet("{id}/messages")]
        public async Task<List<MessageViewModel>> Get(string name)
        {
            return await lobbyService.GetMessages(name);
        }

        // POST api/<LobbyController>
        [HttpPost]
        public async Task Post([FromQuery] string name)
        {
            await lobbyService.CreateLobby(new Lobby(name, currentPlayerService.Player.Name));
        }

        // PUT api/<LobbyController>/5
        [HttpPut("{name}/messages")]
        public async Task Put(string name, [FromQuery] string value)
        {
            await lobbyService.SendMessage(name, null, new Message(Guid.NewGuid().ToString(), currentPlayerService.Player.Name, value, DateTime.Now));
        }
        
        // PUT api/<LobbyController>/5
        [HttpPut("{name}/user")]
        public async Task PutUser(string name, [FromQuery] string value)
        {
            await lobbyService.AddConnection(name, new Connection(Guid.NewGuid().ToString(), currentPlayerService.Player.Name, currentPlayerService.Player.Id));
        }

        // DELETE api/<LobbyController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
