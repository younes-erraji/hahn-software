namespace HahnSoftware.Application.Comments.DTO;

public class CommentDetailDTO
{
    public Guid Id { get; set; }
    public string Content { get; set; }
    public string User { get; set; }
    public DateTimeOffset? UpdateDate { get; set; }
    public DateTimeOffset CreationDate { get; set; }
    public IEnumerable<CommentReplyDTO> Replies { get; set; }

    public long Likes { get; set; }
    public long Dislikes { get; set; }
}
