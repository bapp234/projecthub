namespace ProjectHub.Domain.Constants;

public static class AuthenticationConstants
{
    public const int MaxFailedLoginAttempts = 5;

    public static readonly TimeSpan LockoutDuration
        = TimeSpan.FromMinutes(15);

    public static readonly TimeSpan AccessTokenLifetime
        = TimeSpan.FromMinutes(15);

    public static readonly TimeSpan RefreshTokenLifetime
        = TimeSpan.FromDays(7);
}