using Domain.Entities.Auth;
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
        optionsBuilder.UseNpgsql("Host=SeninHostunuz;Database=SeninVeritabaninuz;Username=SeninKullaniciAdi;Password=;");
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
        modelBuilder.Entity<Manager>(e => { e.ToTable("Managers"); });
        modelBuilder.Entity<User>(e=>{e.ToTable("Users");});
        modelBuilder.Entity<OperationClaim>(e => { e.ToTable("OperationClaims"); });
        modelBuilder.Entity<UserOperationClaim>(e => { e.ToTable("UserOperationClaims"); });
    }

    // Domain
    public DbSet<Contract> Contracts { get; set; } = null!;
    public DbSet<League> Leagues { get; set; } = null!;
    public DbSet<OwnedPlayer> OwnedPlayers { get; set; } = null!;
    public DbSet<Player> Players { get; set; } = null!;
    public DbSet<Team> Teams { get; set; } = null!;
    public DbSet<Offer> Offers { get; set; } = null!;
    public DbSet<Manager> Managers { get; set; } = null!;
    
    // Auth
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<OperationClaim> OperationClaims{ get; set; } = null!;
    public DbSet<UserOperationClaim> UserOperationClaims { get; set; } = null!;
}