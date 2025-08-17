using HahnSoftware.Application.RESTful;
using HahnSoftware.Domain.Interfaces.Repositories;
using HahnSoftware.Domain.Interfaces.Services;
using HahnSoftware.Domain.Entities;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace HahnSoftware.Application.Posts.Commands.DeletePostReaction;

public class DeletePostReactionCommandHandler : IRequestHandler<DeletePostReactionCommand, Response>
{
    private readonly IUserService _userService;
    private readonly IPostRepository _postRepository;
    private readonly IPostReactionRepository _postReactionRepository;

    public DeletePostReactionCommandHandler(IPostRepository postRepository, IUserService userService, IPostReactionRepository postReactionRepository)
    {
        _userService = userService;
        _postRepository = postRepository;
        _postReactionRepository = postReactionRepository;
    }

    public async Task<Response> Handle(DeletePostReactionCommand request, CancellationToken cancellationToken)
    {
        /*
        Post post = await _postRepository.Get(request.PostId);
        post.DeleteReaction(userId);
        await _postRepository.Update(post);
        await _postRepository.SaveChanges();
        return Response.Success();
        */

        Guid userId = _userService.GetUserIdentifier();
        if (await _postRepository.Exists(request.PostId))
        {
            PostReaction? reaction = await _postReactionRepository.Query(x => x.UserId == userId && x.PostId == request.PostId).FirstOrDefaultAsync();

            if (reaction is not null)
            {
                await _postReactionRepository.Delete(reaction, cancellationToken);
                await _postReactionRepository.SaveChanges(cancellationToken);
            }

            return Response.Success();
        }

        return Response.NotFound();
    }
}