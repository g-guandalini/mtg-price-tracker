using Microsoft.EntityFrameworkCore;
using AuthService.Domain;

namespace AuthService.Infrastructure;

public class AuthDbContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public AuthDbContext(
        DbContextOptions<AuthDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("mtg_auth");
        base.OnModelCreating(modelBuilder);

         modelBuilder.Entity<User>()
            .HasIndex(u => u.Username)
            .IsUnique();

        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();
    }
}