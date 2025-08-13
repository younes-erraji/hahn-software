using HahnSoftware.Domain.Entities.Primitives;

namespace HahnSoftware.Domain.Entities;

public sealed class PostAttachment : Entity
{
    public string Name { get; set; }
    public string Extension { get; set; }
    public string ContentType { get; set; }
    public string Key { get; set; }

    public Guid PostId { get; set; }
    public Post Post { get; set; }
}
