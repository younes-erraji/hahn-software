using HahnSoftware.Domain.Entities.Primitives;

namespace HahnSoftware.Domain.Entities;

public sealed class PostAttachment : Entity
{
    public string Name { get; private set; }
    public string Extension { get; private set; }
    public string ContentType { get; private set; }
    public string Key { get; private set; }

    public Guid PostId { get; private set; }
    public Post Post { get; private set; }
}
