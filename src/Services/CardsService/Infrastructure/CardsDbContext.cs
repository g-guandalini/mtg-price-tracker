using Microsoft.EntityFrameworkCore;
using CardsService.Domain;

namespace CardsService.Infrastructure;

public class CardsDbContext : DbContext
{
    public DbSet<Card> Cards { get; set; }

    public DbSet<User> Users { get; set; }

    public DbSet<TrackedCard> TrackedCards { get; set; }


    public CardsDbContext(
        DbContextOptions<CardsDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);


        modelBuilder.Entity<TrackedCard>()
            .HasOne(x => x.User)
            .WithMany(x => x.TrackedCards)
            .HasForeignKey(x => x.UserId);


        modelBuilder.Entity<TrackedCard>()
            .HasOne(x => x.Card)
            .WithMany(x => x.TrackedByUsers)
            .HasForeignKey(x => x.CardId);
    }
}