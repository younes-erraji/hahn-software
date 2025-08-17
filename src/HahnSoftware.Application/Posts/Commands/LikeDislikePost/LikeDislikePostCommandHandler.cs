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
    private readonly IPostReactionRepository _postReactionRepository;

    public LikeDislikePostCommandHandler(IPostRepository postRepository, IUserService userService, IPostReactionRepository postReactionRepository)
    {
        _userService = userService;
        _postRepository = postRepository;
        _postReactionRepository = postReactionRepository;
    }

    public async Task<Response> Handle(LikeDislikePostCommand request, CancellationToken cancellationToken)
    {
        Guid userId = _userService.GetUserIdentifier();
        if (await _postRepository.Exists(request.PostId))
        {
            if (await _postReactionRepository.Exists(x => x.UserId == userId && x.PostId == request.PostId) == false)
            {
                PostReaction reaction = new PostReaction(request.PostId, userId, request.Type);
                await _postReactionRepository.Create(reaction, cancellationToken);
                await _postReactionRepository.SaveChanges(cancellationToken);
                return Response.Success();
            }

            return Response.BadRequest("This post is already liked or disliked");
        }

        return Response.NotFound();
    }
}