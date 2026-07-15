namespace ProjectHub.Domain.Exceptions;

public sealed class InvalidRefreshTokenException : Exception
{
    public InvalidRefreshTokenException()
        : base("Refresh token is invalid.")
    {
    }
}