using Application.Interfaces;
using Application.Models;
using Domain.Constants;
using IdentityModel;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CurrentPlayerService : ICurrentPlayerService
    {
        public CurrentPlayerService(IHttpContextAccessor accessor)
        {
            Player = new CurrentPlayer
            {
                Id = accessor.HttpContext.User.FindFirstValue(JwtClaimTypes.Id),
                Name = accessor.HttpContext.User.FindFirstValue(JwtClaimTypes.Name)
            };
        }

        public CurrentPlayer Player { get; set; }
    }
}
