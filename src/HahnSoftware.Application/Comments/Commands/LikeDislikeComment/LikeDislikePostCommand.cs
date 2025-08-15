using HahnSoftware.Application.RESTful;
using HahnSoftware.Domain.Enums;

using MediatR;

namespace HahnSoftware.Application.Comments.Commands.LikeComment;

public class LikeDislikeCommentCommand : IRequest<Response>
{
    public ReactionType Type { get; private set; }
    public Guid CommentId { get; private set; }

    public LikeDislikeCommentCommand(ReactionType type, Guid commentId)
    {
        Type = type;
        CommentId = commentId;
    }
}
