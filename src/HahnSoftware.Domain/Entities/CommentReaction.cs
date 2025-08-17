using HahnSoftware.Domain.Entities.Primitives;
using HahnSoftware.Domain.Events.Comments;
using HahnSoftware.Domain.Enums;

namespace HahnSoftware.Domain.Entities;

public sealed class CommentReaction : Entity
{
    public ReactionType Type { get; private set; }

    public Guid UserId { get; private set; }
    public User User { get; private set; }

    public Guid CommentId { get; private set; }
    public Comment Comment { get; private set; }

    public CommentReaction()
    {

    }

    public CommentReaction(Guid commentId, Guid userId, ReactionType type)
    {
        Type = type;
        UserId = userId;
        CommentId = commentId;
        AddDomainEvent(new CommentReactionCreateEvent(Id, UserId, Type));
    }
}
