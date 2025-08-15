using HahnSoftware.Application.RESTful;

using MediatR;

namespace HahnSoftware.Application.Posts.Commands.CreatePost;

public class CreatePostCommand : IRequest<Response>
{
    public string Title { get; private set; }
    public string Body { get; private set; }
    public List<string> Tags { get; private set; }

    public CreatePostCommand(string title, string body, List<string> tags)
    {
        Title = title;
        Body = body;
        Tags = tags;
    }
}