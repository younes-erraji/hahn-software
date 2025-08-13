namespace HahnSoftware.Domain.Events;

public class DomainEvent : IDomainEvent
{
    public Guid Id { get; } = Guid.CreateVersion7();
    public DateTimeOffset OccurredOn { get; } = DateTimeOffset.Now;
}
