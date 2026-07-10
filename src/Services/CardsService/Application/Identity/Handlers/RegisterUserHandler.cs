using CardsService.Application.Identity.Commands;
using CardsService.Application.Interfaces;
using CardsService.Domain;
using CardsService.Infrastructure;

namespace CardsService.Application.Identity.Handlers;

public class RegisterUserHandler
{
    private readonly CardsDbContext _context;
    private readonly IPasswordHasherService _passwordHasher;


    public RegisterUserHandler(
        CardsDbContext context,
        IPasswordHasherService passwordHasher)
    {
        _context = context;
        _passwordHasher = passwordHasher;
    }


    public async Task<Guid> Handle(
        RegisterUserCommand command)
    {
        var user = new User
        {
            Id = Guid.NewGuid(),

            Username = command.Username,

            Email = command.Email,

            PasswordHash =
                _passwordHasher.Hash(command.Password),

            CreatedAt = DateTime.UtcNow
        };


        _context.Users.Add(user);

        await _context.SaveChangesAsync();


        return user.Id;
    }
}