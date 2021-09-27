using Application.Interfaces;
using Application.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        // POST api/<UserController>
        [HttpPost("registration")]
        public async Task Post([FromBody] RegisterDto dto, CancellationToken cancellationToken)
        {
            await userService.Register(dto, cancellationToken);
        }
    }
}
