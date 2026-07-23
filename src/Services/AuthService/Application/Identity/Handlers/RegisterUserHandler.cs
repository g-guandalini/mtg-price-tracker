using AuthService.Application.Identity.Commands;
using AuthService.Application.Interfaces;
using AuthService.Domain;
using AuthService.Infrastructure;
using Microsoft.EntityFrameworkCore;
using AuthService.Application.Exceptions;

namespace AuthService.Application.Identity.Handlers;

public class RegisterUserHandler
{
    private readonly AuthDbContext _context;
    private readonly IPasswordHasherService _passwordHasher;


    public RegisterUserHandler(
        AuthDbContext context,
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