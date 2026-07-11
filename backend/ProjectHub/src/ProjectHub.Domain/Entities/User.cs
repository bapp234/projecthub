using ProjectHub.Domain.Common;

namespace ProjectHub.Domain.Entities;

public sealed class User : AggregateRoot
{
    private User() { }

    public string FullName { get; private set; } = string.Empty;

    public string Email { get; private set; } = string.Empty;

    public string? AvatarUrl { get; private set; }

    public ICollection<WorkspaceMember> WorkspaceMemberships { get; private set; }
        = new List<WorkspaceMember>();
}