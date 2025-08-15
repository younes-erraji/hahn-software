using HahnSoftware.Application.RESTful;

using MediatR;

namespace HahnSoftware.Application.Posts.Commands.DeletePost;

public class DeletePostCommand : IRequest<Response>
{
    public Guid Id { get; private set; }

    public DeletePostCommand(Guid id)
    {
        Id = id;
    }
}
