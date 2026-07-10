using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CardsService.Application.Interfaces;
using CardsService.Domain;
using Microsoft.IdentityModel.Tokens;

namespace CardsService.Infrastructure.Security;

public class JwtTokenService : IJwtTokenService
{
    private readonly IConfiguration _configuration;


    public JwtTokenService(
        IConfiguration configuration)
    {
        _configuration = configuration;
    }


    public string GenerateToken(Guid userId, string username)
    {
        var key =
            _configuration["Jwt:Key"];


        var securityKey =
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(key!));


        var credentials =
            new SigningCredentials(
                securityKey,
                SecurityAlgorithms.HmacSha256);


        var claims = new[]
        {
            new Claim("userId", userId.ToString()),
            new Claim("username", username)
        };


        var token =
            new JwtSecurityToken(
                issuer:
                    _configuration["Jwt:Issuer"],

                audience:
                    _configuration["Jwt:Audience"],

                claims: claims,

                expires:
                    DateTime.UtcNow.AddMinutes(
                        int.Parse(
                            _configuration["Jwt:ExpirationMinutes"]!)),

                signingCredentials:
                    credentials
            );


        return new JwtSecurityTokenHandler()
            .WriteToken(token);
    }
}