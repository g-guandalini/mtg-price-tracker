using AuthService.Application.Identity.Commands;
using AuthService.Application.Identity.Dto;
using AuthService.Infrastructure;
using AuthService.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using AuthService.Application.Exceptions;

namespace AuthService.Application.Identity.Handlers;

public class LoginHandler
{    
    private readonly AuthDbContext _context;
    private readonly IPasswordHasherService _passwordHasher;
    private readonly IJwtTokenService _jwtTokenService;


    public LoginHandler(
    AuthDbContext context,
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