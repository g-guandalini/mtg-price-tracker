using System.Text.Json.Serialization;

namespace CardsService.Infrastructure.Clients.Models;

public class ScryfallPrices
{
    [JsonPropertyName("usd")]
    public string? Usd { get; set; }
}