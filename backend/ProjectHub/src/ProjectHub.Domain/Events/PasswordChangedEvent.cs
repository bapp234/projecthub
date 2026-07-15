using ProjectHub.Domain.Common;

namespace ProjectHub.Domain.Events;

public sealed class PasswordChangedEvent : IDomainEvent
{
    public Guid UserId { get; }

    public DateTime OccurredOn { get; }


    public PasswordChangedEvent(Guid userId)
    {
        UserId = userId;
        OccurredOn = DateTime.UtcNow;
    }
}