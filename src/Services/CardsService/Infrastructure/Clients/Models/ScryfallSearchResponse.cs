using System.Text.Json.Serialization;

namespace CardsService.Infrastructure.Clients.Models;

public class ScryfallSearchResponse
{
    [JsonPropertyName("data")]
    public List<ScryfallCardDto> Data { get; set; } = [];
}