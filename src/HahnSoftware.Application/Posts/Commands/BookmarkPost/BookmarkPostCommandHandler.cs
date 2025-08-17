using HahnSoftware.Domain.Entities;
using HahnSoftware.Domain.Interfaces.Repositories;
using HahnSoftware.Domain.Interfaces.Services;
using HahnSoftware.Application.RESTful;

using MediatR;

namespace HahnSoftware.Application.Posts.Commands.CreatePost;

public class BookmarkPostCommandHandler : IRequestHandler<BookmarkPostCommand, Response>
{
    private readonly IUserService _userService;
    private readonly IPostRepository _postRepository;
    private readonly IPostBookmarkRepository _postBookmarkRepository;
    public BookmarkPostCommandHandler(IPostRepository postRepository, IUserService userService, IPostBookmarkRepository postBookmarkRepository)
    {
        _userService = userService;
        _postRepository = postRepository;
        _postBookmarkRepository = postBookmarkRepository;
    }

    public async Task<Response> Handle(BookmarkPostCommand request, CancellationToken cancellationToken)
    {
        Guid userId = _userService.GetUserIdentifier();
        if (await _postRepository.Exists(request.Id))
        {
            if (await _postBookmarkRepository.Exists(x => x.UserId == userId && x.PostId == request.Id) == false)
            {
                PostBookmark postBookmark = new PostBookmark(request.Id, userId);

                await _postBookmarkRepository.Create(postBookmark);
                await _postBookmarkRepository.SaveChanges();
                return Response.Success();
            }

            return Response.BadRequest("This post is already bookmarked!");
        }

        return Response.NotFound();
    }
}