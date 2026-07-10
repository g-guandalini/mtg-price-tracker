using System.Text.Json.Serialization;

namespace CardsService.Infrastructure.Clients.Models;

public class ScryfallCardDto
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("image_uris")]
    public ScryfallImageUris ImageUris { get; set; } = new();

    [JsonPropertyName("prices")]
    public ScryfallPrices Prices { get; set; } = new();
}