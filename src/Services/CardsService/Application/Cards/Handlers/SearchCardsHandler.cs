using CardsService.Application.Cards.Dto;
using CardsService.Application.Interfaces;
using CardsService.Application.Cards.Queries;

namespace CardsService.Application.Cards.Handlers;

public class SearchCardsHandler
{
    private readonly IScryfallClient _scryfallClient;

    public SearchCardsHandler(IScryfallClient scryfallClient)
    {
        _scryfallClient = scryfallClient;
    }

    public async Task<List<SearchCardResponse>> Handle(
        SearchCardsQuery query)
    {
        return await _scryfallClient.SearchCardsAsync(
            query.Name);
    }
}