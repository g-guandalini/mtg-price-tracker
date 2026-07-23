namespace CardsService.Domain;

public class TrackedCard
{
    public Guid Id { get; set; }


    public Guid UserId { get; set; }


    public Guid CardId { get; set; }

    public Card Card { get; set; } = null!;


    public DateTime CreatedAt { get; set; }

    public bool Active { get; set; }

    public int Quantity { get; set; }
}