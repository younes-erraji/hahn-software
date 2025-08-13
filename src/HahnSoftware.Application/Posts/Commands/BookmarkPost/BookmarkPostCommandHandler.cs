using HahnSoftware.Domain.Entities;
using HahnSoftware.Application.RESTful;

using MediatR;
using HahnSoftware.Domain.Interfaces.Repositories;
using HahnSoftware.Domain.Interfaces.Services;

namespace HahnSoftware.Application.Posts.Commands.CreatePost;

public class BookmarkPostCommandHandler : IRequestHandler<BookmarkPostCommand, Response>
{
    private readonly IUserService _userService;
    private readonly IPostRepository _postRepository;

    public BookmarkPostCommandHandler(IPostRepository postRepository, IUserService userService)
    {
        _userService = userService;
        _postRepository = postRepository;
    }

    public async Task<Response> Handle(BookmarkPostCommand request, CancellationToken cancellationToken)
    {
        Guid userId = _userService.GetUserIdentifier();
        Post post = await _postRepository.Get(request.Id);
        post.Bookmark(userId);
        await _postRepository.Update(post);
        await _postRepository.SaveChanges();

        return Response.Success();
    }
}