namespace HahnSoftware.Application.Comments.DTO;

public class CommentReplyDTO
{
    public Guid Id { get; set; }
    public string Content { get; set; }
    public string User { get; set; }
    public DateTimeOffset? UpdateDate { get; set; }
    public DateTimeOffset CreationDate { get; set; }
}
