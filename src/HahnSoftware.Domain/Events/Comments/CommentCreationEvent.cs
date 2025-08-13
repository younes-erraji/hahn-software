namespace HahnSoftware.Domain.Events.Comments;

public class CommentCreationEvent : DomainEvent
{
    public Guid PostId { get; }
    public Guid CommentId { get; }
    public string Content { get; }
    public Guid UserId { get; }

    public CommentCreationEvent(Guid postId, Guid commentId, string content, Guid userId)
    {
        PostId = postId;
        CommentId = commentId;
        Content = content;
        UserId = userId;
    }
}
