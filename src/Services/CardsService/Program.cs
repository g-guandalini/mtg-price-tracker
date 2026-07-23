using CardsService.Infrastructure;
using Microsoft.EntityFrameworkCore;
using CardsService.Application.Cards.Handlers;
using CardsService.Application.TrackedCards.Handlers;
using CardsService.Application.Interfaces;
using CardsService.Infrastructure.Clients;
using CardsService.Infrastructure.Configuration;
using Microsoft.Extensions.Options;
using CardsService.Endpoints;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using AuthService.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();;
// Configurações de EF Core (PostgreSQL)
builder.Services.AddDbContext<CardsService.Infrastructure.CardsDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("CardsDatabase")));

builder.Services.AddScoped<GetCardByIdHandler>();
builder.Services.AddScoped<CreateCardHandler>();
builder.Services.AddScoped<SearchCardsHandler>();
builder.Services.AddScoped<CreateTrackedCardHandler>();

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

builder.Services.Configure<ScryfallOptions>(
    builder.Configuration.GetSection(ScryfallOptions.SectionName));

builder.Services.AddHttpClient<IScryfallClient, ScryfallClient>(
    (provider, client) =>
    {
        var options =
            provider.GetRequiredService<
                IOptions<ScryfallOptions>>().Value;

        client.BaseAddress = new Uri(options.BaseUrl);

        client.DefaultRequestHeaders.Add(
            "User-Agent",
            options.UserAgent);

        client.DefaultRequestHeaders.Add(
            "Accept",
            options.Accept);
    });

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

app.MapCardsEndpoints();
app.MapTrackedCardsEndpoints();

await app.ApplyMigrationsAsync();

app.Run();
