using System.Text.Json.Serialization;

namespace CardsService.Infrastructure.Clients.Models;

public class ScryfallImageUris
{
    [JsonPropertyName("normal")]
    public string Normal { get; set; } = string.Empty;
}