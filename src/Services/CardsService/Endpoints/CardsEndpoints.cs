using CardsService.Application.Cards.Commands;
using CardsService.Application.Cards.Dto;
using CardsService.Application.Cards.Handlers;
using CardsService.Application.Cards.Queries;

namespace CardsService.Endpoints;

public static class CardsEndpoints
{
    public static void MapCardsEndpoints(this WebApplication app)
    {
        app.MapGet("/cards/{id:guid}", async (
            Guid id,
            GetCardByIdHandler handler) =>
        {
            var card = await handler.Handle(
                new GetCardByIdQuery(id));

            return card is null
                ? Results.NotFound()
                : Results.Ok(card);
        });

        app.MapPost("/cards", async (
            CreateCardCommand command,
            CreateCardHandler handler) =>
        {
            var id = await handler.Handle(command);

            return Results.Created($"/cards/{id}", new { id });
        });

        app.MapGet("/cards/search", async (
            string name,
            SearchCardsHandler handler) =>
        {
            var result = await handler.Handle(
                new SearchCardsQuery(name));

            return Results.Ok(result);
        });
    }
}