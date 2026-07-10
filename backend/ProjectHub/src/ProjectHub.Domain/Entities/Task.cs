using ProjectHub.Domain.Common;

namespace ProjectHub.Domain.Entities;

public sealed class Task : AuditableEntity
{
    private Task() { }

    public Guid ProjectId { get; private set; }

    public Project Project { get; private set; } = null!;

    public Guid? AssigneeId { get; private set; }

    public User? Assignee { get; private set; }

    public string Title { get; private set; } = string.Empty;

    public string? Description { get; private set; }

    public string Status { get; private set; } = string.Empty;

    public DateTimeOffset? DueDate { get; private set; }
}