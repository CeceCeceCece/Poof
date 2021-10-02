using Domain.Configuration.EntityConfiguration;
using Domain.Entities;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class PoofDbContext : IdentityDbContext<Player>
    {
        public DbSet<Card> Cards{ get; set; }
        public DbSet<RoleCard> RoleCards{ get; set; }
        public DbSet<Group> Games{ get; set; }
        public DbSet<Connection> Connections{ get; set; }

        public PoofDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new CardSeedConfiguration());
            modelBuilder.ApplyConfiguration(new RoleCardSeedConfiguration());
        }

    }
}
