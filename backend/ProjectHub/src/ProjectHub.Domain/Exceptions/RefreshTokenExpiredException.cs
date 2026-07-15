namespace ProjectHub.Domain.Exceptions;

public sealed class RefreshTokenExpiredException : Exception
{
    public RefreshTokenExpiredException()
        : base("Refresh token has expired.")
    {
    }
}