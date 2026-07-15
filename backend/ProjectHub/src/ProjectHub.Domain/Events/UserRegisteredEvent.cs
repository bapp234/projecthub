using ProjectHub.Domain.Common;

namespace ProjectHub.Domain.Events;

public sealed class UserRegisteredEvent : IDomainEvent
{
    public Guid UserId { get; }

    public DateTime OccurredOn { get; }


    public UserRegisteredEvent(Guid userId)
    {
        UserId = userId;
        OccurredOn = DateTime.UtcNow;
    }
}