using HahnSoftware.Application.RESTful;

using MediatR;

namespace HahnSoftware.Application.Posts.Commands.UpdatePost;

public class UpdatePostCommand : IRequest<Response>
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
    public List<string> Tags { get; set; }
}