namespace HahnSoftware.Domain.Events.Posts;

public class PostBookmarkEvent : DomainEvent
{
    public Guid PostId { get; }
    public Guid UserId { get; }

    public PostBookmarkEvent(Guid postId, Guid userId)
    {
        PostId = postId;
        UserId = userId;
    }
}
