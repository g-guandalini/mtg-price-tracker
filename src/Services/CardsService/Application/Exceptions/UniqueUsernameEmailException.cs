namespace CardsService.Application.Exceptions;

public class UniqueUsernameEmailException : Exception
{
    public UniqueUsernameEmailException()
        : base("Usuário ou e-mail já utilizados.")
    {
    }
}