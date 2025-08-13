namespace HahnSoftware.Domain.Events.Posts;

public class CommentReactionRemoveEvent : DomainEvent
{
    public Guid PostId { get; }
    public Guid UserId { get; }

    public CommentReactionRemoveEvent(Guid postId, Guid userId)
    {
        PostId = postId;
        UserId = userId;
    }
}
