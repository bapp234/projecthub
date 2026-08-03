using ProjectHub.Domain.Common;

namespace ProjectHub.Domain.Entities;

public sealed class RefreshToken : AggregateRoot
{
    private RefreshToken()
    {
    }

    public Guid UserId { get; private set; }

    public User User { get; private set; } = default!;

    public string Token { get; private set; } = string.Empty;

    public DateTime ExpiresAtUtc { get; private set; }

    public DateTime? RevokedAtUtc { get; private set; }

    public string? ReplacedByToken { get; private set; }

    public string? ReasonRevoked { get; private set; }

    public bool IsExpired =>
        DateTime.UtcNow >= ExpiresAtUtc;

    public bool IsRevoked =>
        RevokedAtUtc.HasValue;

    public bool IsActive =>
        !IsExpired && !IsRevoked;

    public static RefreshToken Create(
        Guid userId,
        string token,
        DateTime expiresAtUtc)
    {
        if (userId == Guid.Empty)
        {
            throw new ArgumentException(
                "User id cannot be empty.",
                nameof(userId));
        }

        if (string.IsNullOrWhiteSpace(token))
        {
            throw new ArgumentException(
                "Refresh token cannot be empty.",
                nameof(token));
        }

        if (expiresAtUtc <= DateTime.UtcNow)
        {
            throw new ArgumentException(
                "Refresh token expiration must be in the future.",
                nameof(expiresAtUtc));
        }

        return new RefreshToken
        {
            UserId = userId,
            Token = token,
            ExpiresAtUtc = expiresAtUtc,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }

    public void Revoke(
        string? replacedByToken,
        string? reason)
    {
        if (IsRevoked)
        {
            return;
        }

        RevokedAtUtc = DateTime.UtcNow;
        ReplacedByToken = replacedByToken;
        ReasonRevoked = reason;
        UpdatedAt = DateTime.UtcNow;
    }
}