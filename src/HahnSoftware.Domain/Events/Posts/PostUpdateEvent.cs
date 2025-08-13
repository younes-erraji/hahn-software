namespace HahnSoftware.Domain.Events.Posts;

public class PostUpdateEvent : DomainEvent
{
    public Guid PostId { get; }
    public string Title { get; }

    public PostUpdateEvent(Guid postId, string title)
    {
        PostId = postId;
        Title = title;
    }
}
