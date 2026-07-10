using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CardsService.Domain;

public class Card
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string ScryfallId { get; set; } = string.Empty;

    public string ImageUrl { get; set; } = string.Empty;

    public decimal CurrentPrice { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<TrackedCard> TrackedByUsers { get; set; }
        = new List<TrackedCard>();
}