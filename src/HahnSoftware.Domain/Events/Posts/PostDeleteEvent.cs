namespace HahnSoftware.Domain.Events.Posts;

public class PostDeleteEvent : DomainEvent
{
    public Guid Id { get; set; }

    public PostDeleteEvent(Guid postId)
    {
        Id = postId;
    }
}
