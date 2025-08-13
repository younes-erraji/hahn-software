using HahnSoftware.Domain.Entities.Primitives;
using HahnSoftware.Domain.Enums;

namespace HahnSoftware.Domain.Entities;

public sealed class PostReaction : Entity
{
    public ReactionType Type { get; set; }

    public Guid UserId { get; set; }
    public User User { get; set; }

    public Guid PostId { get; set; }
    public Post Post { get; set; }

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
