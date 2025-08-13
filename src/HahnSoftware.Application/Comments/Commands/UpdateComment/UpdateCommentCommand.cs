using HahnSoftware.Application.RESTful;

using MediatR;

namespace HahnSoftware.Application.Posts.Commands.UpdatePost;

public class UpdateCommentCommand : IRequest<Response>
{
    public Guid CommentId { get; set; }
    public string Content { get; set; }
}