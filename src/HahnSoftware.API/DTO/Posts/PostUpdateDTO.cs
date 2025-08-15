namespace HahnSoftware.API.DTO.Posts;

public class PostUpdateDTO
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
    public List<string> Tags { get; set; }
}
