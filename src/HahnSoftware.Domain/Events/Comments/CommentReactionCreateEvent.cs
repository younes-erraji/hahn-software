using HahnSoftware.Domain.Enums;

namespace HahnSoftware.Domain.Events.Comments;

public class CommentReactionCreateEvent : DomainEvent
{
    public Guid CommentId { get; set; }
    public Guid UserId { get; set; }
    public ReactionType Type { get; set; }

    public CommentReactionCreateEvent(Guid commentId, Guid userId, ReactionType type)
    {
        CommentId = commentId;
        UserId = userId;
        Type = type;
    }
}
