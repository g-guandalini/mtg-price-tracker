namespace CardsService.Application.Exceptions;

public class InvalidCredentialsException : Exception
{
    public InvalidCredentialsException()
        : base("Usuário ou senha inválidos.")
    {
    }
}