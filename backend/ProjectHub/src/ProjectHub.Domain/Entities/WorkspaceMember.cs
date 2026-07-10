using ProjectHub.Domain.Common;

namespace ProjectHub.Domain.Entities;

public sealed class WorkspaceMember : AuditableEntity
{
    private WorkspaceMember() { }

    public Guid WorkspaceId { get; private set; }

    public Workspace Workspace { get; private set; } = null!;

    public Guid UserId { get; private set; }

    public User User { get; private set; } = null!;

    public string Role { get; private set; } = string.Empty;
}