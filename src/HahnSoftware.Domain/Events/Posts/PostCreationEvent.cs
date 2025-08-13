namespace HahnSoftware.Domain.Events.Posts;

public class PostCreationEvent : DomainEvent
{
    public Guid PostId { get; }
    public Guid UserId { get; }
    public string Title { get; set; }

    public PostCreationEvent(Guid postId, Guid userId, string title)
    {
        PostId = postId;
        UserId = userId;
        Title = title;
    }
}
