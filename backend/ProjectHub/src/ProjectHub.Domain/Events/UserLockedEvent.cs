using ProjectHub.Domain.Common;

namespace ProjectHub.Domain.Events;

public sealed class UserLockedEvent : IDomainEvent
{
    public Guid UserId { get; }


    public DateTime OccurredOn { get; }


    public UserLockedEvent(Guid userId)
    {
        UserId = userId;
        OccurredOn = DateTime.UtcNow;
    }
}