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
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=ManagerDb;Username=postgres;Password=123Tolga");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Contract>(e =>
        {
            e.ToTable("Contracts");
            e.HasOne<Manager>()
                .WithMany()
                .HasForeignKey(o => o.ManagerId)
                .OnDelete(DeleteBehavior.Cascade);
            e.HasOne<Player>()
                .WithMany()
                .HasForeignKey(o => o.PlayerId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<League>(e => { e.ToTable("Leagues"); });

        modelBuilder.Entity<Player>(e =>
        {
            e.ToTable("Players");
            e.HasOne<Team>()
                .WithMany()
                .HasForeignKey(o => o.TeamId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Team>(e =>
        {
            e.ToTable("Teams");
            e.HasOne<League>()
                .WithMany()
                .HasForeignKey(o => o.LeagueId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Offer>(e =>
        {
            e.ToTable("Offers");
            e.HasOne<Manager>()
                .WithMany()
                .HasForeignKey(o => o.ManagerId)
                .OnDelete(DeleteBehavior.Cascade);
            e.HasOne<Player>()
                .WithMany()
                .HasForeignKey(o => o.PlayerId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Manager>(e => { e.ToTable("Managers"); });

        modelBuilder.Entity<User>(e =>
        {
            e.ToTable("Users");
            e.HasOne<Manager>()
                .WithMany()
                .HasForeignKey(o => o.Manager_Id)
                .OnDelete(DeleteBehavior.SetNull);
            e.HasOne<Player>()
                .WithMany()
                .HasForeignKey(o => o.Player_Id)
                .OnDelete(DeleteBehavior.SetNull);
        });

        modelBuilder.Entity<OperationClaim>(e => { e.ToTable("OperationClaims"); });

        modelBuilder.Entity<UserOperationClaim>(e =>
        {
            e.ToTable("UserOperationClaims");
            e.HasOne<User>()
                .WithMany()
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            e.HasOne<OperationClaim>()
                .WithMany()
                .HasForeignKey(o => o.OperationClaimId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }

    // Domain
    public DbSet<Contract> Contracts { get; set; } = null!;
    public DbSet<League> Leagues { get; set; } = null!;
    public DbSet<Player> Players { get; set; } = null!;
    public DbSet<Team> Teams { get; set; } = null!;
    public DbSet<Offer> Offers { get; set; } = null!;
    public DbSet<Manager> Managers { get; set; } = null!;
    
    // Auth
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<OperationClaim> OperationClaims{ get; set; } = null!;
    public DbSet<UserOperationClaim> UserOperationClaims { get; set; } = null!;
}
