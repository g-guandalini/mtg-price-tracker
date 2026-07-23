using AuthService.Infrastructure;
using Microsoft.EntityFrameworkCore;
using AuthService.Application.Interfaces;
using Microsoft.Extensions.Options;
using AuthService.Application.Identity.Handlers;
using AuthService.Infrastructure.Security;
using AuthService.Endpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using AuthService.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Configurações de EF Core (PostgreSQL)
builder.Services.AddDbContext<AuthService.Infrastructure.AuthDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("AuthDatabase")));

builder.Services.AddScoped<LoginHandler>();
builder.Services.AddScoped<RegisterUserHandler>();

builder.Services.AddScoped<IPasswordHasherService, PasswordHasherService>();
builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],

            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                    builder.Configuration["Jwt:Key"]!))
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddCors(options =>
{
    options.AddPolicy("frontend",
        policy =>
        {
            policy
                .AllowAnyHeader()
                .AllowAnyMethod()
                .WithOrigins(
                    "http://localhost:5173"
                );
        });
});


var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.UseCors("frontend");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapAuthEndpoints();

await app.ApplyMigrationsAsync();

app.Run();


