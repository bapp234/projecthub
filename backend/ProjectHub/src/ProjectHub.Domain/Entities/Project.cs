using ProjectHub.Domain.Common;

namespace ProjectHub.Domain.Entities;

public sealed class Project : AuditableEntity, IAggregateRoot
{
    private Project() { }

    public Guid WorkspaceId { get; private set; }

    public Workspace Workspace { get; private set; } = null!;

    public string Name { get; private set; } = string.Empty;

    public string? Description { get; private set; }

    public string Status { get; private set; } = string.Empty;

    public ICollection<Task> Tasks { get; private set; }
        = new List<Task>();
}