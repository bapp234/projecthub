using ProjectHub.Domain.Common;
using ProjectHub.Domain.Enums;

namespace ProjectHub.Domain.Entities;

public sealed class WorkspaceMember : AggregateRoot
{
    private WorkspaceMember()
    {
    }


    public Guid UserId { get; private set; }


    public User User { get; private set; } = default!;


    public Guid WorkspaceId { get; private set; }


    public Workspace Workspace { get; private set; } = default!;


    public WorkspaceRole Role { get; private set; }
}