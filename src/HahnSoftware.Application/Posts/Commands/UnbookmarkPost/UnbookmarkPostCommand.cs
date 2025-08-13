using HahnSoftware.Application.RESTful;

using MediatR;

namespace HahnSoftware.Application.Posts.Commands.CreatePost;

public class UnbookmarkPostCommand : IRequest<Response>
{
    public Guid Id { get; set; }
}