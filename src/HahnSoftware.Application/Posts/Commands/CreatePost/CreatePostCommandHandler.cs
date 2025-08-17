using HahnSoftware.Domain.Entities;
using HahnSoftware.Domain.Interfaces.Repositories;
using HahnSoftware.Domain.Interfaces.Services;
using HahnSoftware.Application.RESTful;

using MediatR;

namespace HahnSoftware.Application.Posts.Commands.CreatePost;

public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, Response>
{
    private readonly IUserService _userService;
    private readonly IPostRepository _postRepository;

    public CreatePostCommandHandler(IPostRepository postRepository, IUserService userService)
    {
        _userService = userService;
        _postRepository = postRepository;
    }

    public async Task<Response> Handle(CreatePostCommand request, CancellationToken cancellationToken)
    {
        Guid userId = _userService.GetUserIdentifier();
        Post post = new (request.Title, request.Body, request.Tags, userId);

        await _postRepository.Create(post, cancellationToken);
        await _postRepository.SaveChanges(cancellationToken);

        return Response.Success();
    }
}