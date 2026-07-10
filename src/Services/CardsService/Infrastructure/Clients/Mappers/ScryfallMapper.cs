using CardsService.Application.Cards.Dto;
using CardsService.Infrastructure.Clients.Models;
using System.Globalization;

namespace CardsService.Infrastructure.Mappers;

public static class ScryfallMapper
{
    public static SearchCardResponse ToSearchCardResponse(
        this ScryfallCardDto card)
    {
        decimal.TryParse(
            card.Prices.Usd,
            NumberStyles.Any,
            CultureInfo.InvariantCulture,
            out var price);

        return new SearchCardResponse(
            card.Id,
            card.Name,
            card.ImageUris.Normal,
            price);
    }
}