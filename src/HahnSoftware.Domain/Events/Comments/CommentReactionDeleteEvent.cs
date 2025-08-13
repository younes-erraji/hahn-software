namespace HahnSoftware.Domain.Events.Posts;

public class CommentReactionDeleteEvent : DomainEvent
{
    public Guid PostId { get; }
    public Guid UserId { get; }

    public CommentReactionDeleteEvent(Guid postId, Guid userId)
    {
        PostId = postId;
        UserId = userId;
    }
}
