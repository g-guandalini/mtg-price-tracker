using CardsService.Application.Identity.Commands;
using CardsService.Application.Interfaces;
using CardsService.Domain;
using CardsService.Infrastructure;
using Microsoft.EntityFrameworkCore;
using CardsService.Application.Exceptions;

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


    public async Task<Guid> Handle(RegisterUserCommand command)
    {
        var username = command.Username.Trim().ToLowerInvariant();
        var email = command.Email.Trim().ToLowerInvariant();

        var existingUser = await _context.Users
            .FirstOrDefaultAsync(u =>
                u.Username == username ||
                u.Email == email);

        if (existingUser != null)
        {
            throw new UniqueUsernameEmailException();
        }

        var user = new User
        {
            Id = Guid.NewGuid(),
            Username = username,
            Email = email,
            PasswordHash = _passwordHasher.Hash(command.Password),
            CreatedAt = DateTime.UtcNow
        };

        _context.Users.Add(user);

        await _context.SaveChangesAsync();

        return user.Id;
    }
}