using HahnSoftware.Application.RESTful;

using MediatR;

namespace HahnSoftware.Application.Posts.Commands.CreatePost;

public class CreateCommentCommand : IRequest<Response>
{
    public Guid PostId { get; set; }
    public string Content { get; set; }
}