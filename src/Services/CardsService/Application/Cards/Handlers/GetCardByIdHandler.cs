using Microsoft.EntityFrameworkCore;
using CardsService.Application.Cards.Dto;
using CardsService.Application.Cards.Queries;
using CardsService.Infrastructure;

namespace CardsService.Application.Cards.Handlers;

public class GetCardByIdHandler
{
    private readonly CardsDbContext _db;

    public GetCardByIdHandler(CardsDbContext db)
    {
        _db = db;
    }

    public async Task<CardResponse?> Handle(
        GetCardByIdQuery query,
        CancellationToken cancellationToken = default)
    {
        return await _db.Cards
            .Where(c => c.Id == query.Id)
            .Select(c => new CardResponse(
                c.Id,
                c.Name,
                c.ImageUrl,
                c.ScryfallId,
                c.CurrentPrice))
            .FirstOrDefaultAsync(cancellationToken);
    }
}