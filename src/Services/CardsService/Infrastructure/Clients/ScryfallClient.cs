using System.Text.Json;
using CardsService.Application.Cards.Dto;
using CardsService.Application.Interfaces;
using CardsService.Infrastructure.Clients.Models;
using System.Net.Http.Json;
using CardsService.Infrastructure.Mappers;

namespace CardsService.Infrastructure.Clients;

public class ScryfallClient : IScryfallClient
{
    private readonly HttpClient _httpClient;

    public ScryfallClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<SearchCardResponse>> SearchCardsAsync(string name)
    {

        var query = Uri.EscapeDataString($"name:\"{name}\"");
        var url = $"cards/search?q={query}&unique=prints";

        var response = await _httpClient.GetAsync(url);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content
                .ReadFromJsonAsync<ScryfallErrorResponse>();

            if (error?.Code == "not_found")
            {
                return [];
            }

            var body = await response.Content.ReadAsStringAsync();

            throw new Exception($"""
                Status: {(int)response.StatusCode}

                URL:
                {new Uri(_httpClient.BaseAddress!, url)}

                Resposta:
                {body}
                """);
        }

        var result = await response.Content.ReadFromJsonAsync<ScryfallSearchResponse>();

        return result.Data?
            .Select(card => card.ToSearchCardResponse())
            .ToList()
            ?? [];
    }
}