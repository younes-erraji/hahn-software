namespace HahnSoftware.Domain.Events.Posts;

public class PostReactionRemoveEvent : DomainEvent
{
    public Guid PostId { get; }
    public Guid UserId { get; }

    public PostReactionRemoveEvent(Guid postId, Guid userId)
    {
        PostId = postId;
        UserId = userId;
    }
}
