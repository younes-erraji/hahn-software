namespace HahnSoftware.Domain.Events.Posts;

public class PostArchiveEvent : DomainEvent
{
    public Guid PostId { get; }

    public PostArchiveEvent(Guid postId)
    {
        PostId = postId;
    }
}
