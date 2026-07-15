namespace ProjectHub.Domain.Constants;

public static class PasswordConstants
{
    public const int MinimumLength = 8;

    public const int MaximumLength = 128;

    public const bool RequireUppercase = true;

    public const bool RequireLowercase = true;

    public const bool RequireDigit = true;

    public const bool RequireSpecialCharacter = false;
}