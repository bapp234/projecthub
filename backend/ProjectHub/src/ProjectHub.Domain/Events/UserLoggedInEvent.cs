using ProjectHub.Domain.Common;

namespace ProjectHub.Domain.Events;

public sealed class UserLoggedInEvent : IDomainEvent
{
    public Guid UserId { get; }


    public DateTime OccurredOn { get; }


    public UserLoggedInEvent(Guid userId)
    {
        UserId = userId;
        OccurredOn = DateTime.UtcNow;
    }
}