namespace ProjectHub.Domain.Constants;

public static class RefreshTokenMessages
{
    public const string ReplacedByRotation =
        "Replaced by refresh token rotation.";

    public const string RevokedByLogout =
        "Revoked by user logout.";

    public const string RevokedByAdministrator =
        "Revoked by administrator.";

    public const string RevokedBySecurity =
        "Revoked due to security policy.";
}