using HahnSoftware.Domain.Entities.Primitives;

namespace HahnSoftware.Domain.Entities;

public sealed class PostBookmark : Entity
{
    public Guid PostId { get; private set; }
    public Post Post { get; private set; }

    public Guid UserId { get; private set; }
    public User User { get; private set; }

    public PostBookmark() { }
    public PostBookmark(Guid postId, Guid userId)
    {
        PostId = postId;
        UserId = userId;
    }
}
