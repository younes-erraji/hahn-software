namespace HahnSoftware.Domain.Events.Posts;

public class PostUnbookmarkEvent : DomainEvent
{
    public Guid PostId { get; }
    public Guid UserId { get; }

    public PostUnbookmarkEvent(Guid postId, Guid userId)
    {
        PostId = postId;
        UserId = userId;
    }
}
