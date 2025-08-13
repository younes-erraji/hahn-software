namespace HahnSoftware.Domain.Events.Comments;

public class CommentReplyEvent : DomainEvent
{
    public Guid PostId { get; }
    public Guid CommentId { get; }
    public Guid ThreadCommentId { get; }
    public string Content { get; }
    public Guid UserId { get; }

    public CommentReplyEvent(Guid postId, Guid commentId, string content, Guid userId, Guid threadCommentId)
    {
        PostId = postId;
        UserId = userId;
        Content = content;
        CommentId = commentId;
        ThreadCommentId = threadCommentId;
    }
}
