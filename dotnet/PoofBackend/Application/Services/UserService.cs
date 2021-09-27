using Application.Interfaces;
using Application.Models.DTOs;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<Player> userManager;
        public UserService(UserManager<Player> userManager)
        {
            this.userManager = userManager;
        }
        public async Task Register(RegisterDto dto, CancellationToken? cancellationToken)
        {
            var user = new Player { UserName = dto.UserName};
            await userManager.CreateAsync(user, dto.Password);
            await userManager.AddToRoleAsync(user, "User");
        }
    }
}
