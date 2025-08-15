using HahnSoftware.Application.RESTful;

using MediatR;

namespace HahnSoftware.Application.Comments.Commands.DeleteCommentReaction;

public class DeleteCommentReactionCommand : IRequest<Response>
{
    public Guid CommentId { get; private set; }

    public DeleteCommentReactionCommand(Guid commentId)
    {
        CommentId = commentId;
    }
}
