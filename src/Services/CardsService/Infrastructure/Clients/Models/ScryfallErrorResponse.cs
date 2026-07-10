namespace CardsService.Infrastructure.Clients.Models;

public class ScryfallErrorResponse
{
    public string Object { get; set; } = string.Empty;

    public string Code { get; set; } = string.Empty;

    public int Status { get; set; }

    public string Details { get; set; } = string.Empty;
}