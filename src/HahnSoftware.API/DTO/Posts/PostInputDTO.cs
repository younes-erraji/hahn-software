namespace HahnSoftware.API.DTO.Posts;

public class PostInputDTO
{
    public string Title { get; set; }
    public string Body { get; set; }
    public List<string> Tags { get; set; }
}
