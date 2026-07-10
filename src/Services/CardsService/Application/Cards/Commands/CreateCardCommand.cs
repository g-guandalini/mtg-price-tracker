using CardsService.Application.Cards.Dto;

namespace CardsService.Application.Cards.Commands;

public record CreateCardCommand(
    string Name,
    string ImageUrl,
    string ScryfallId,
    decimal CurrentPrice
);