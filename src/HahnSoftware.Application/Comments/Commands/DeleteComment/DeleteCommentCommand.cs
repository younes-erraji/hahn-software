using HahnSoftware.Application.RESTful;

using MediatR;

namespace HahnSoftware.Application.Posts.Commands.DeletePost;

public class DeleteCommentCommand : IRequest<Response>
{
    public Guid CommentId { get; set; }
}