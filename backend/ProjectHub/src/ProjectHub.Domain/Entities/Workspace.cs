using ProjectHub.Domain.Common;

namespace ProjectHub.Domain.Entities;

public sealed class Workspace : AuditableEntity, IAggregateRoot
{
    private Workspace() { }

    public string Name { get; private set; } = string.Empty;

    public string Slug { get; private set; } = string.Empty;

    public string? Description { get; private set; }

    public Guid OwnerId { get; private set; }

    public User Owner { get; private set; } = null!;

    public ICollection<WorkspaceMember> Members { get; private set; }
        = new List<WorkspaceMember>();

    public ICollection<Project> Projects { get; private set; }
        = new List<Project>();
}