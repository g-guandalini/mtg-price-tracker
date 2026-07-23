using Microsoft.EntityFrameworkCore;
using CardsService.Domain;

namespace CardsService.Infrastructure;

public class CardsDbContext : DbContext
{
    public DbSet<Card> Cards { get; set; }

    public DbSet<TrackedCard> TrackedCards { get; set; }


    public CardsDbContext(
        DbContextOptions<CardsDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("mtg_cards");
        base.OnModelCreating(modelBuilder);
    }
}