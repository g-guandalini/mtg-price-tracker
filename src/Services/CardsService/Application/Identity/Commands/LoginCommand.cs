namespace CardsService.Application.Identity.Commands;

public record LoginCommand(
    string Username,
    string Password
);