using HahnSoftware.Application.RESTful;

using MediatR;

namespace HahnSoftware.Application.Posts.Commands.DeletePost;

public class DeletePostCommand : IRequest<Response>
{
    public Guid Id { get; set; }
}
