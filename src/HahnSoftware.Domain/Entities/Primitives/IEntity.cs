namespace HahnSoftware.Domain.Entities.Primitives;

public interface IEntity
{
    Guid Id { get; }
    DateTimeOffset CreationDate { get; }
    DateTimeOffset? DeletionDate { get; set; }
}
