namespace ProjectHub.Domain.Exceptions;

public sealed class AccountLockedException : Exception
{
    public AccountLockedException()
        : base("Your account has been locked.")
    {
    }
}