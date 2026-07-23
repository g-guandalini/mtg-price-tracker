namespace AuthService.Application.Identity.Commands;

public record RegisterUserCommand(
    string Username,
    string Email,
    string Password
);