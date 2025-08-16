namespace HahnSoftware.Application.Posts.DTO;

public class PostDetailDTO
{
    public Guid Id { get; set; }
    public string Slug { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
    public string User { get; set; }
    public long Likes { get; set; }
    public long Dislikes { get; set; }
    public List<string> Tags { get; set; }
    public DateTimeOffset CreationDate { get; set; }
    public DateTimeOffset? UpdateDate { get; set; }
}
