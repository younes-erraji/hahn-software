namespace HahnSoftware.Domain.Events.Posts;

public class PostReactionDeleteEvent : DomainEvent
{
    public Guid PostId { get; }
    public Guid UserId { get; }

    public PostReactionDeleteEvent(Guid postId, Guid userId)
    {
        PostId = postId;
        UserId = userId;
    }
}
