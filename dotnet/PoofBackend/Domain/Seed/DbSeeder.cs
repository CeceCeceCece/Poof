using Domain.Constants;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Seed
{
    public class DbSeeder
    {
        public static async Task SeedDatabase(IServiceProvider services, PoofDbContext context, RoleManager<IdentityRole> roleManager, UserManager<Player> userManager)
        {
            await TryCreateRolesAsync(roleManager);
            var users = await TryCreateUsersAsync(context);
            await AddRoleToUsers(userManager, users);
        }

        private static async Task TryCreateRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            if ((await roleManager.Roles.CountAsync()) > 0)
            {
                return;
            }

            await roleManager.CreateAsync(new IdentityRole(PoofRoles.Owner));
            await roleManager.CreateAsync(new IdentityRole(PoofRoles.Admin));
            await roleManager.CreateAsync(new IdentityRole(PoofRoles.User));
        }
        private static async Task<ICollection<Player>> TryCreateUsersAsync(PoofDbContext context)
        {
            if ((await context.Users.CountAsync()) > 0)
            {
                return new List<Player>();
            }

            var users = new List<Player>
            {
                new Player{ UserName = "Cece"},
                new Player{ UserName = "Csabi"},
                new Player{ UserName = "user1"},
                new Player{ UserName = "user2"}
            };

            PasswordHasher<Player> passwordHasher = new PasswordHasher<Player>();
            foreach (var user in users)
            {
                user.PasswordHash = passwordHasher.HashPassword(user, "admin");
                user.NormalizedUserName = user.UserName.ToUpper();
                user.SecurityStamp = Guid.NewGuid().ToString();
                user.EmailConfirmed = false;
            }

            await context.AddRangeAsync(users);
            await context.SaveChangesAsync();
            return users;
        }

        private static async Task AddRoleToUsers(UserManager<Player> userManager, ICollection<Player> users)
        {
            if (users.Count == 0)
            {
                return;
            }

            for (int i = 0; i < users.Count; i++)
            {
                if (i < 2)
                {
                    await userManager.AddToRoleAsync(users.ElementAt(i), PoofRoles.Admin);
                }
                else
                {
                    await userManager.AddToRoleAsync(users.ElementAt(i), PoofRoles.User);
                }
            }
        }

    }
}