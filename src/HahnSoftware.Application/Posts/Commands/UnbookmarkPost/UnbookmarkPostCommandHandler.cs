using HahnSoftware.Domain.Entities;
using HahnSoftware.Application.RESTful;

using MediatR;
using HahnSoftware.Domain.Interfaces.Repositories;
using HahnSoftware.Domain.Interfaces.Services;
using Microsoft.EntityFrameworkCore;

namespace HahnSoftware.Application.Posts.Commands.CreatePost;

public class UnbookmarkPostCommandHandler : IRequestHandler<UnbookmarkPostCommand, Response>
{
    private readonly IUserService _userService;
    private readonly IPostRepository _postRepository;
    private readonly IPostBookmarkRepository _postBookmarkRepository;

    public UnbookmarkPostCommandHandler(IPostRepository postRepository, IUserService userService, IPostBookmarkRepository postBookmarkRepository)
    {
        _userService = userService;
        _postRepository = postRepository;
        _postBookmarkRepository = postBookmarkRepository;
    }

    public async Task<Response> Handle(UnbookmarkPostCommand request, CancellationToken cancellationToken)
    {
        Guid userId = _userService.GetUserIdentifier();
        if (await _postRepository.Exists(request.PostId))
        {
            PostBookmark? bookmark = await _postBookmarkRepository.Query(x => x.UserId == userId && x.PostId == request.PostId).FirstOrDefaultAsync();

            if (bookmark is not null)
            {
                await _postBookmarkRepository.Delete(bookmark);
                await _postBookmarkRepository.SaveChanges();
            }

            return Response.Success();
        }

        return Response.NotFound();
    }
}