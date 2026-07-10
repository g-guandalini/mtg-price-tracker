using CardsService.Application.Identity.Commands;
using CardsService.Application.Identity.Dto;
using CardsService.Infrastructure;
using CardsService.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using CardsService.Application.Exceptions;

namespace CardsService.Application.Identity.Handlers;

public class LoginHandler
{    
    private readonly CardsDbContext _context;
    private readonly IPasswordHasherService _passwordHasher;
    private readonly IJwtTokenService _jwtTokenService;


    public LoginHandler(
    CardsDbContext context,
    IPasswordHasherService passwordHasher,
    IJwtTokenService jwtTokenService)
    {    
        _context = context;
        _passwordHasher = passwordHasher;
        _jwtTokenService = jwtTokenService;
    }


    public async Task<LoginResponse> Handle(
        LoginCommand command)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Username == command.Username);

        if (user == null)
        {
            throw new InvalidCredentialsException();
        }

        if (!_passwordHasher.Verify(command.Password, user.PasswordHash))
        {
            throw new InvalidCredentialsException();
        }

        return new LoginResponse
        {
            Token = _jwtTokenService.GenerateToken(
                user.Id,
                user.Username)
        };
    }
}