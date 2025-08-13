namespace HahnSoftware.Application.Posts.DTO;

public class PostListDTO
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
    public string User { get; set; }
    public DateTimeOffset CreationDate { get; set; }
    public DateTimeOffset? UpdateDate { get; set; }
}
