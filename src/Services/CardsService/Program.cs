using CardsService.Infrastructure;
using Microsoft.EntityFrameworkCore;
using CardsService.Application.Cards.Handlers;
//using CardsService.Application.Cards.Dto;
//using CardsService.Application.Cards.Queries;
//using CardsService.Application.Cards.Commands;
using CardsService.Application.TrackedCards.Handlers;
//using CardsService.Application.TrackedCards.Dto;
//using CardsService.Application.TrackedCards.Commands;
using CardsService.Application.Interfaces;
using CardsService.Infrastructure.Clients;
using CardsService.Infrastructure.Configuration;
using Microsoft.Extensions.Options;
//using CardsService.Application.Identity.Dto;
//using CardsService.Application.Identity.Commands;
using CardsService.Application.Identity.Handlers;
using CardsService.Infrastructure.Security;
using CardsService.Endpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

Console.WriteLine(
    builder.Configuration.GetConnectionString("DefaultConnection"));

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
// Configurações de EF Core (PostgreSQL) e MediatR
builder.Services.AddDbContext<CardsService.Infrastructure.CardsDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("CardsDatabase")));

builder.Services.AddScoped<GetCardByIdHandler>();
builder.Services.AddScoped<CreateCardHandler>();
builder.Services.AddScoped<SearchCardsHandler>();
builder.Services.AddScoped<LoginHandler>();
builder.Services.AddScoped<RegisterUserHandler>();
builder.Services.AddScoped<CreateTrackedCardHandler>();

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
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapCardsEndpoints();
app.MapAuthEndpoints();
app.MapTrackedCardsEndpoints();

app.MapGet("/", () => "CardsService running");

if (builder.Configuration.GetValue<bool>("RUN_MIGRATIONS"))
{
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<CardsDbContext>();
    await db.Database.MigrateAsync();
    return;
}


app.Run();
