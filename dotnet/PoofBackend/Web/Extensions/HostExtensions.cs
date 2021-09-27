
using Domain;
using Domain.Entities;
using Domain.Seed;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Web;

namespace Veterinary.Api.Extensions
{
    public static class HostExtensions
    {
        public static IHost MigrateDatabase<TContext>(this IHost host) where TContext : DbContext
        {
            using (var scope = host.Services.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                //dgfagsg
                var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
                var context = serviceProvider.GetRequiredService<TContext>();
                context.Database.Migrate();
            }

            return host;
        }

        public static async Task<IHost> SeedDatabase(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<PoofDbContext>();

                var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = services.GetRequiredService<UserManager<Player>>();

                await DbSeeder.SeedDatabase(services, context, roleManager, userManager);
            }

            return host;
        }
    }
}