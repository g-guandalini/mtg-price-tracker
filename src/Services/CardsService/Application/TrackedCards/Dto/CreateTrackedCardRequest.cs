namespace CardsService.Application.TrackedCards.Dto;

public sealed record CreateTrackedCardsRequest(
    string Name,
    string ImageUrl,
    string ScryfallId,
    decimal CurrentPrice,
    int Quantity
);