using HahnSoftware.Domain.Entities.Primitives;
using HahnSoftware.Domain.Enums;

namespace HahnSoftware.Domain.Entities;

public sealed class PostReaction : Entity
{
    public ReactionType Type { get; private set; }

    public Guid UserId { get; private set; }
    public User User { get; private set; }

    public Guid PostId { get; private set; }
    public Post Post { get; private set; }

    public PostReaction()
    {
        
    }

    public PostReaction(Guid postId, Guid userId, ReactionType type)
    {
        Type = type;
        UserId = userId;
        PostId = postId;
    }
}
