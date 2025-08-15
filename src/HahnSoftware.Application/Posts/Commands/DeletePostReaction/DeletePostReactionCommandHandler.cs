using HahnSoftware.Application.RESTful;
using HahnSoftware.Domain.Interfaces.Repositories;
using HahnSoftware.Domain.Interfaces.Services;
using HahnSoftware.Domain.Entities;

using MediatR;

namespace HahnSoftware.Application.Posts.Commands.DeletePostReaction;

public class DeletePostReactionCommandHandler : IRequestHandler<DeletePostReactionCommand, Response>
{
    private readonly IUserService _userService;
    private readonly IPostRepository _postRepository;

    public DeletePostReactionCommandHandler(IPostRepository postRepository, IUserService userService)
    {
        _userService = userService;
        _postRepository = postRepository;
    }

    public async Task<Response> Handle(DeletePostReactionCommand request, CancellationToken cancellationToken)
    {
        Guid userId = _userService.GetUserIdentifier();
        Post post = await _postRepository.Get(request.PostId);
        post.DeleteReaction(userId);
        await _postRepository.Update(post);
        await _postRepository.SaveChanges();
        return Response.Success();
    }
}