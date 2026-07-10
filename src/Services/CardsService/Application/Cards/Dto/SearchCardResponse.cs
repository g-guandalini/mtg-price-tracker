using CardsService.Application.Cards.Dto;

namespace CardsService.Application.Cards.Dto;

public sealed record SearchCardResponse(
    string ScryfallId,
    string Name,
    string ImageUrl,
    decimal CurrentPrice
);