using HahnSoftware.Domain.Events;
using System.ComponentModel.DataAnnotations.Schema;

namespace HahnSoftware.Domain.Entities.Primitives;

public class Entity : IEntity
{
    public Guid Id { get; } = Guid.CreateVersion7();
    public DateTimeOffset CreationDate { get; } = DateTimeOffset.Now;
    public DateTimeOffset? DeletionDate { get; protected set; }

    // Domain events
    private readonly List<DomainEvent> _domainEvents = new();

    [NotMapped]
    public IReadOnlyCollection<DomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    protected void AddDomainEvent(DomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public void RemoveDomainEvent(DomainEvent domainEvent)
    {
        _domainEvents.Remove(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}
