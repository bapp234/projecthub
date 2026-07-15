using ProjectHub.Domain.Common;

namespace ProjectHub.Domain.Events;

public sealed class RefreshTokenRotatedEvent : IDomainEvent
{
    public Guid UserId { get; }


    public DateTime OccurredOn { get; }


    public RefreshTokenRotatedEvent(Guid userId)
    {
        UserId = userId;
        OccurredOn = DateTime.UtcNow;
    }
}