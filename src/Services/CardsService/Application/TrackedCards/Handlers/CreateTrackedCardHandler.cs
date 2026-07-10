using Microsoft.EntityFrameworkCore;
using CardsService.Application.TrackedCards.Dto;
using CardsService.Domain;
using CardsService.Infrastructure;
using CardsService.Application.TrackedCards.Commands;

namespace CardsService.Application.TrackedCards.Handlers;

public class CreateTrackedCardHandler
{
    private readonly CardsDbContext _db;

    public CreateTrackedCardHandler(CardsDbContext db)
    {
        _db = db;
    }
    
    // Fluxo:
    // 1. Procura a Card pelo ScryfallId.
    // 2. Cria a Card caso ela ainda não exista.
    // 3. Verifica se o usuário já monitora a carta.
    // 4. Reativa o monitoramento caso esteja inativo.
    // 5. Cria o vínculo se ainda não existir.
    public async Task<Guid> Handle( CreateTrackedCardCommand command)
    {
        var card = await _db.Cards
            .FirstOrDefaultAsync(c =>
                c.ScryfallId == command.ScryfallId);

        if(card == null)
        {
            card = new Card
            {
                Id = Guid.NewGuid(),
                Name = command.Name,
                ScryfallId = command.ScryfallId,
                ImageUrl = command.ImageUrl,
                CurrentPrice = command.CurrentPrice
            };

            _db.Cards.Add(card);

            await _db.SaveChangesAsync();
        }

        var trackedCard =
            await _db.TrackedCards
                .FirstOrDefaultAsync(t =>
                    t.UserId == command.UserId &&
                    t.CardId == card.Id);

        if (trackedCard != null)
        {
            trackedCard.Quantity = command.Quantity;
            trackedCard.Active = true;

            await _db.SaveChangesAsync();

            return trackedCard.Id;
        }

        var tracked = new TrackedCard
        {
            Id = Guid.NewGuid(),
            UserId = command.UserId,
            CardId = card.Id,
            Active = true,
            Quantity = command.Quantity,
            CreatedAt = DateTime.UtcNow
        };

        _db.TrackedCards.Add(tracked);

        await _db.SaveChangesAsync();

        return tracked.Id;
    }
}