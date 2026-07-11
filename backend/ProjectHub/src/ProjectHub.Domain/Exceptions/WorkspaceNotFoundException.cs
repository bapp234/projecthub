namespace ProjectHub.Domain.Exceptions;

public sealed class WorkspaceNotFoundException : Exception
{
    public WorkspaceNotFoundException(Guid workspaceId)
        : base($"Workspace '{workspaceId}' was not found.")
    {
    }
}