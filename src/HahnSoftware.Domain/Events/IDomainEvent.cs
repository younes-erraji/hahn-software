using MediatR;

namespace HahnSoftware.Domain.Events;

public interface IDomainEvent : INotification
{
    Guid Id { get; }
    DateTimeOffset OccurredOn { get; }
}
