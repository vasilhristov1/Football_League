using football_league.Models;
using football_league.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace football_league.Data;

public class MainContext : DbContext
{
    public MainContext(DbContextOptions<MainContext> options) : base(options) {}

    public DbSet<User> Users { get; set; }
    public DbSet<Team> Teams { get; set; }
    public DbSet<Match> Matches { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("FootballLeagueApp");

        modelBuilder.Entity<User>()
            .Property(u => u.Role)
            .HasConversion<string>();
        
        modelBuilder.Entity<User>().HasData(new User
        {
            Id = 1,
            Username = "admin",
            PasswordHash = "admin123",
            Role = Role.Admin,
            CreatedDateTime = new DateTime(2025, 5, 11),
        });

        var entities = modelBuilder
            .Model.GetEntityTypes()
            .Where(w => w.ClrType.IsSubclassOf(typeof(ModelBase)))
            .Select(p => modelBuilder.Entity(p.ClrType));

        modelBuilder.Entity<Match>()
            .HasOne(m => m.HomeTeam)
            .WithMany()
            .HasForeignKey(m => m.HomeTeamId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Match>()
            .HasOne(m => m.AwayTeam)
            .WithMany()
            .HasForeignKey(m => m.AwayTeamId)
            .OnDelete(DeleteBehavior.Restrict);
        
        foreach (var entity in entities)
        {
            entity.Property("CreatedDateTime")
                .HasDefaultValueSql("GETUTCDATE()")
                .ValueGeneratedOnAdd();
        }

        base.OnModelCreating(modelBuilder);
    }
}