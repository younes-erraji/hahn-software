using HahnSoftware.Application.RESTful;
using HahnSoftware.Domain.Enums;

using MediatR;

namespace HahnSoftware.Application.Posts.Commands.LikePost;

public class LikeDislikePostCommand : IRequest<Response>
{
    public ReactionType Type { get; private set; }
    public Guid PostId { get; private set; }

    public LikeDislikePostCommand(ReactionType type, Guid postId)
    {
        Type = type;
        PostId = postId;
    }
}
