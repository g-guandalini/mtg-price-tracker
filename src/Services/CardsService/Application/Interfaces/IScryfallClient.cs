using CardsService.Application.Cards.Dto;

namespace CardsService.Application.Interfaces;

public interface IScryfallClient
{
    Task<List<SearchCardResponse>> SearchCardsAsync(string name);
}