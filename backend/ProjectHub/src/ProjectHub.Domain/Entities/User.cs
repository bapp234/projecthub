using ProjectHub.Domain.Common;
using ProjectHub.Domain.Enums;
using ProjectHub.Domain.ValueObjects;

namespace ProjectHub.Domain.Entities;

public sealed class User : AggregateRoot
{
    private User()
    {
        WorkspaceMemberships = new List<WorkspaceMember>();
    }


    public string FullName { get; private set; } = string.Empty;


    public Email Email { get; private set; } = default!;


    public PasswordHash? PasswordHash { get; private set; }= default!;

    public string? AvatarUrl { get; private set; }


    public string? Bio { get; private set; }


    public UserStatus Status { get; private set; }


    public bool EmailVerified { get; private set; }


    public int FailedLoginAttempts { get; private set; }


    public DateTime? LockoutEndUtc { get; private set; }


    public DateTime? LastLoginUtc { get; private set; }




    public ICollection<WorkspaceMember> WorkspaceMemberships { get; private set; }
        = new List<WorkspaceMember>();

    public static User Create(
    string fullName,
    Email email,
    PasswordHash passwordHash,
    string? avatarUrl)
    {
        return new User
        {
            FullName = fullName,
            Email = email,
            PasswordHash = passwordHash,
            AvatarUrl = avatarUrl,
            Status = UserStatus.Active,
            EmailVerified = false,
            FailedLoginAttempts = 0,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }
}