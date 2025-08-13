namespace HahnSoftware.Domain.Events.Comments;

public class CommentDeleteEvent : DomainEvent
{
    public Guid PostId { get; }

    public CommentDeleteEvent(Guid postId)
    {
        PostId = postId;
    }
}
