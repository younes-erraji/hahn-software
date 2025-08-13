using HahnSoftware.Domain.Enums;

namespace HahnSoftware.Domain.Events.Posts;

public class PostReactionCreateEvent : DomainEvent
{
    public Guid PostId { get; }
    public Guid UserId { get; }
    public ReactionType Type { get; }

    public PostReactionCreateEvent(Guid postId, Guid userId, ReactionType type)
    {
        PostId = postId;
        UserId = userId;
        Type = type;
    }
}
