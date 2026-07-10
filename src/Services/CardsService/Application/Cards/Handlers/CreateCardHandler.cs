using Microsoft.EntityFrameworkCore;
using CardsService.Application.Cards.Dto;
using CardsService.Domain;
using CardsService.Infrastructure;
using CardsService.Application.Cards.Commands;

namespace CardsService.Application.Cards.Handlers;

public class CreateCardHandler
{
    private readonly CardsDbContext _db;

    public CreateCardHandler(CardsDbContext db)
    {
        _db = db;
    }

    public async Task<Guid> Handle(CreateCardCommand command)
    {
        var card = new Card
        {
            Id = Guid.NewGuid(),
            Name = command.Name,
            ScryfallId = command.ScryfallId,
            ImageUrl = command.ImageUrl,
            CurrentPrice = command.CurrentPrice
        };

        _db.Cards.Add(card);
        await _db.SaveChangesAsync();

        return card.Id;
    }
}