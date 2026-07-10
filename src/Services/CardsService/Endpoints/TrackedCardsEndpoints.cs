using CardsService.Application.TrackedCards.Commands;
using CardsService.Application.TrackedCards.Dto;
using CardsService.Application.TrackedCards.Handlers;
using System.Security.Claims;

namespace CardsService.Endpoints;

public static class TrackedCardsEndpoints
{
    public static void MapTrackedCardsEndpoints(this WebApplication app)
    {
        app.MapPost("/tracked-cards",
        async (
            HttpContext context,
            CreateTrackedCardsRequest request,
            CreateTrackedCardHandler handler) =>
        {
            var userId = Guid.Parse(
                context.User.FindFirstValue(
                    ClaimTypes.NameIdentifier)!);

            var command =
                new CreateTrackedCardCommand(
                    request.Name,
                    request.ImageUrl,
                    request.ScryfallId,
                    request.CurrentPrice,
                    request.Quantity,
                    userId
                );
            
            var id = await handler.Handle(command);
            
            return Results.Created($"/tracked-cards/{id}", new
            {
                Id = id
            });
        }).RequireAuthorization();
    }
}