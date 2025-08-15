using HahnSoftware.Domain.Entities;
using HahnSoftware.Domain.Interfaces.Repositories;
using HahnSoftware.Domain.Interfaces.Services;
using HahnSoftware.Application.RESTful;

using MediatR;

namespace HahnSoftware.Application.Posts.Commands.LikePost;

public class LikeDislikePostCommandHandler : IRequestHandler<LikeDislikePostCommand, Response>
{
    private readonly IUserService _userService;
    private readonly IPostRepository _postRepository;

    public LikeDislikePostCommandHandler(IPostRepository postRepository, IUserService userService)
    {
        _userService = userService;
        _postRepository = postRepository;
    }

    public async Task<Response> Handle(LikeDislikePostCommand request, CancellationToken cancellationToken)
    {
        Guid userId = _userService.GetUserIdentifier();
        Post post = await _postRepository.Get(request.PostId);
        post.LikeDislike(userId, request.Type);
        await _postRepository.Update(post);
        await _postRepository.SaveChanges();
        return Response.Success();
    }
}