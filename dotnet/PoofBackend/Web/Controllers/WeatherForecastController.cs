using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    //[Authorize]
    public class WeatherForecastController : ControllerBase
    {
        public WeatherForecastController(ICurrentPlayerService playerService)
        {
            this.playerService = playerService;
        }
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
        private readonly ICurrentPlayerService playerService;


        [HttpGet("picim")]
        public async Task<string> GetHello()
        {
            return "Szia Picim Isten Áldjon Meg :)";
        }


        [Authorize]
        [HttpGet]
        public async Task<string> Get()
        {
            return playerService.Player.Name + playerService.Player.Id;
        }
        [Authorize(Policy = "Admin")]
        [HttpGet("admin")]
        public async Task<IEnumerable<WeatherForecast>> Get1()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
        [Authorize(Policy = "User")]
        [HttpGet("user")]
        public async Task<IEnumerable<WeatherForecast>> Get2()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
