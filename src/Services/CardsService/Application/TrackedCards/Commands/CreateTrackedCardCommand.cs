using CardsService.Application.TrackedCards.Dto;

namespace CardsService.Application.TrackedCards.Commands;

public record CreateTrackedCardCommand(
    string Name,
    string ImageUrl,
    string ScryfallId,
    decimal CurrentPrice,
    int Quantity,
    Guid UserId
);