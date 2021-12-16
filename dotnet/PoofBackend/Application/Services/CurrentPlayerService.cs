using Application.Interfaces;
using Application.Models;
using IdentityModel;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

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
