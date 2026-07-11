namespace ProjectHub.Domain.Exceptions;

public sealed class DuplicateEmailException : Exception
{
    public DuplicateEmailException(string email)
        : base($"The email '{email}' is already in use.")
    {
    }
}