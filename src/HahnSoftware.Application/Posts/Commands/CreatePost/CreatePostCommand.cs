using HahnSoftware.Application.RESTful;

using MediatR;

namespace HahnSoftware.Application.Posts.Commands.CreatePost;

public class CreatePostCommand : IRequest<Response>
{
    public string Title { get; set; }
    public string Body { get; set; }
    public List<string> Tags { get; set; }
}