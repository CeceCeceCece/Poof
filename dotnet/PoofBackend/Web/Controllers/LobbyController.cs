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
        public List<MessageViewModel> Get(int name)
        {
            return mapper.ProjectTo<MessageViewModel>(lobbyService.me);
        }

        // POST api/<LobbyController>
        [HttpPost]
        public async Task Post([FromQuery] string name)
        {
            await lobbyService.CreateLobby(new Lobby(name, currentPlayerService.Player.Name));
        }

        // PUT api/<LobbyController>/5
        [HttpPut("{id}/messages")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<LobbyController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
