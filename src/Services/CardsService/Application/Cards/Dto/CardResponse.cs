namespace CardsService.Application.Cards.Dto;

public sealed record CardResponse(
    Guid Id,
    string Name,
    string ImageUrl,
    string ScryfallId,
    decimal CurrentPrice
);