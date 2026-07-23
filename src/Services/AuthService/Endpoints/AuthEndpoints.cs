using AuthService.Application.Identity.Commands;
using AuthService.Application.Identity.Dto;
using AuthService.Application.Identity.Handlers;
using AuthService.Application.Exceptions;
using System.Security.Claims;

namespace AuthService.Endpoints;

public static class AuthEndpoints
{
    public static void MapAuthEndpoints(this WebApplication app)
    {
        app.MapPost("/auth/register",
        async (
            RegisterUserRequest request,
            RegisterUserHandler handler) =>
        {
            var command =
                new RegisterUserCommand(
                    request.Username,
                    request.Email,
                    request.Password);

            try
            {
                var id =
                    await handler.Handle(command);


                return Results.Ok(new
                {
                    UserId = id
                });
            }
            catch (UniqueUsernameEmailException ex)
            {
                return Results.Conflict(new
                {
                    message = ex.Message
                });
            }
        });


        app.MapPost("/auth/login",
        async (
            LoginRequest request,
            LoginHandler handler) =>
        {
            var command =
                new LoginCommand(
                    request.Username,
                    request.Password);

            try
            {
                var response = await handler.Handle(command);
                return Results.Ok(response);
            }
            catch (InvalidCredentialsException)
            {
                return Results.Unauthorized();
            }
        });
    }
}