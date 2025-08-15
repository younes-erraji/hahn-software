using HahnSoftware.Application.RESTful;

using MediatR;

namespace HahnSoftware.Application.Posts.Commands.DeletePostReaction;

public class DeletePostReactionCommand : IRequest<Response>
{
    public Guid PostId { get; private set; }

    public DeletePostReactionCommand(Guid postId)
    {
        PostId = postId;
    }
}
