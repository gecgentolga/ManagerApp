using Domain.Entities.Concrete;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.DataAccess.Concrete.EntityFramework;

public class Context : DbContext
{
    public Context()
    {
    }

    public Context(DbContextOptions<Context> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (optionsBuilder.IsConfigured) return;

        // Fallback: Local geliştirme için. Tercihen appsettings.json + DI kullanın.
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=ManagerDb;Username=postgres;Password=123Tolga");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Contract>(e => { e.ToTable("Contracts"); });
        modelBuilder.Entity<League>(e => { e.ToTable("Leagues"); });
        modelBuilder.Entity<OwnedPlayer>(e => { e.ToTable("OwnedPlayers"); });
        modelBuilder.Entity<Player>(e => { e.ToTable("Players"); });
        modelBuilder.Entity<Team>(e => { e.ToTable("Teams"); });
        modelBuilder.Entity<Offer>(e => { e.ToTable("Offers"); });
    }

    public DbSet<Contract> Contracts { get; set; } = null!;
    public DbSet<League> Leagues { get; set; } = null!;
    public DbSet<OwnedPlayer> OwnedPlayers { get; set; } = null!;
    public DbSet<Player> Players { get; set; } = null!;
    public DbSet<Team> Teams { get; set; } = null!;
    public DbSet<Offer> Offers { get; set; } = null!;
}