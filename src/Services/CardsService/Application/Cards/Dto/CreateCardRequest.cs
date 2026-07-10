using CardsService.Application.Cards.Dto;

namespace CardsService.Application.Cards.Dto;

public sealed record CreateCardRequest(
    string Name,
    string ImageUrl,
    string ScryfallId,
    decimal CurrentPrice
);