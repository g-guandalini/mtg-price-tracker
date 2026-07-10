namespace CardsService.Infrastructure.Configuration;

public class ScryfallOptions
{
    public const string SectionName = "Scryfall";

    public string BaseUrl { get; set; } = string.Empty;

    public string UserAgent { get; set; } = string.Empty;

    public string Accept { get; set; } = string.Empty;
}