namespace ProjectHub.Domain.Interfaces;

public interface IClock
{
    DateTime UtcNow { get; }
}