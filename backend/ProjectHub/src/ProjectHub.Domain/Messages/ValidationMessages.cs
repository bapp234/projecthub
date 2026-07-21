namespace ProjectHub.Domain.Messages;

public static class ValidationMessages
{
    // Common
    public const string Required = "{PropertyName} is required.";

    // Email
    public const string InvalidEmail =
        "{PropertyName} is not a valid email address.";

    // Password
    public const string PasswordsDoNotMatch =
        "Passwords do not match.";
}