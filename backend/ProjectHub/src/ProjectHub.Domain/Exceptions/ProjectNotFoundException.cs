namespace ProjectHub.Domain.Exceptions;

public sealed class ProjectNotFoundException : Exception
{
    public ProjectNotFoundException(Guid projectId)
        : base($"Project '{projectId}' was not found.")
    {
    }
}