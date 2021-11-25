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
        public CurrentPlayerService(HttpContext context)
        {
            Player = new CurrentPlayer
            {
                Id = context.User.FindFirstValue(JwtClaimTypes.Id),
                Name = context.User.FindFirstValue(JwtClaimTypes.Name)
            };
        }

        public CurrentPlayer Player { get; set; }
    }
}
