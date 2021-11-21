using Domain.Configuration.EntityConfiguration;
using Domain.Entities;
using Domain.Entities.Characters;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Domain
{
    public class PoofDbContext : IdentityDbContext<Player>
    {
        public PoofDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CharacterCard>()
                .HasDiscriminator(ch => ch.Name)
                .HasValue<BartCassidyCharacterCard>("Bart Cassidy")
                .HasValue<BlackJackCharacterCard>("Black Jack")
                .HasValue<CalamityJanetCharacterCard>("Calamity Janet")
                .HasValue<ElGringoCharacterCard>("El Gringo")
                .HasValue<JesseJonesCharacterCard>("Jesse Jones")
                .HasValue<JourdonnaisCharacterCard>("Jourdonnais")
                .HasValue<KitCarlsonCharacterCard>("Kit Carlson")
                .HasValue<PaulRegretCharacterCard>("Paul Regret")
                .HasValue<PedroRamirezCharacterCard>("Pedro Ramirez")
                .HasValue<RoseDoolanCharacterCard>("Rose Doolan")
                .HasValue<SidKetchumCharacterCard>("Sid Ketchum")
                .HasValue<SuzyLafayetteCharacterCard>("Suzy Lafayette")
                .HasValue<WillytheKidCharacterCard>("Willy the Kid");

            modelBuilder.ApplyConfiguration(new CardSeedConfiguration());
            modelBuilder.ApplyConfiguration(new RoleCardSeedConfiguration());
        }

        public DbSet<Card> Cards { get; set; }
        public DbSet<GameCard> GameCards { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Character> Characters { get; set; }
        public DbSet<Lobby> Lobbies { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Connection> Connections { get; set; }
        public DbSet<CharacterCard> CharacterCards { get; set; }
        public DbSet<BartCassidyCharacterCard> BartCassidyCharacterCards { get; set; }
        public DbSet<BlackJackCharacterCard> BlackJackCharacterCards { get; set; }
        public DbSet<CalamityJanetCharacterCard> CalamityJanetCharacterCards { get; set; }
        public DbSet<ElGringoCharacterCard> ElGringoCharacterCards { get; set; }
        public DbSet<JesseJonesCharacterCard> JesseJonesCharacterCards { get; set; }
        public DbSet<JourdonnaisCharacterCard> JourdonnaisCharacterCards { get; set; }
        public DbSet<KitCarlsonCharacterCard> KitCarlsonCharacterCards { get; set; }
        public DbSet<PaulRegretCharacterCard> PaulRegretCharacterCards { get; set; }
        public DbSet<PedroRamirezCharacterCard> PedroRamirezCharacterCards { get; set; }
        public DbSet<RoseDoolanCharacterCard> RoseDoolanCharacterCards { get; set; }
        public DbSet<SidKetchumCharacterCard> SidKetchumCharacterCards { get; set; }
        public DbSet<SuzyLafayetteCharacterCard> SuzyLafayetteCharacterCards { get; set; }
        public DbSet<WillytheKidCharacterCard> WillytheKidCharacterCards { get; set; }

    }
}
