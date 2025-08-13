namespace HahnSoftware.Domain.Events.Comments;

public class CommentUpdateEvent : DomainEvent
{
    public Guid CommentId { get; }
    public string Content { get; }

    public CommentUpdateEvent(Guid commentId, string content)
    {
        CommentId = commentId;
        Content = content;
    }
}
