namespace ProjectHub.Domain.Exceptions;

public sealed class EmailNotVerifiedException : Exception
{
    public EmailNotVerifiedException()
        : base("Email has not been verified.")
    {
    }
}