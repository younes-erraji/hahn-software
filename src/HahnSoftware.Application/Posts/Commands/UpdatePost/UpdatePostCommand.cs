using HahnSoftware.Application.RESTful;

using MediatR;

namespace HahnSoftware.Application.Posts.Commands.UpdatePost;

public class UpdatePostCommand : IRequest<Response>
{
    public Guid Id { get; private set; }
    public string Title { get; private set; }
    public string Body { get; private set; }
    public List<string> Tags { get; private set; }

    public UpdatePostCommand(Guid id, string title, string body, List<string> tags)
    {
        Id = id;
        Title = title;
        Body = body;
        Tags = tags;
    }
}