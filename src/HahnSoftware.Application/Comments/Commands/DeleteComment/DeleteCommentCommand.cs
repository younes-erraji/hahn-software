using HahnSoftware.Application.RESTful;

using MediatR;

namespace HahnSoftware.Application.Comments.Commands.DeletePost;

public class DeleteCommentCommand : IRequest<Response>
{
    public Guid CommentId { get; private set; }

    public DeleteCommentCommand(Guid commentId)
    {
        CommentId = commentId;
    }
}