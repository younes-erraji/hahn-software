using HahnSoftware.Application.Posts.DTO;
using HahnSoftware.Application.Posts.Mappers;
using HahnSoftware.Domain.Interfaces.Repositories;
using HahnSoftware.Domain.Pagination;
using HahnSoftware.Domain.Entities;

using MediatR;

using Microsoft.EntityFrameworkCore;
using HahnSoftware.Domain.Interfaces.Services;

namespace HahnSoftware.Application.Posts.Queries.GetBookmarks;

public class GetBookmarksQueryHandler : IRequestHandler<GetBookmarksQuery, PageableResponse<Post, PostListDTO>>
{
    private readonly IUserService _userService;
    private readonly IPostRepository _postRepository;

    public GetBookmarksQueryHandler(IPostRepository postRepository, IUserService userService)
    {
        _userService = userService;
        _postRepository = postRepository;
    }

    public async Task<PageableResponse<Post, PostListDTO>> Handle(GetBookmarksQuery request, CancellationToken cancellationToken)
    {
        Guid userId = _userService.GetUserIdentifier();
        IQueryable<Post> query = _postRepository
            .Query(x => x.Bookmarks.Any(z => z.UserId == userId))
            .Include(x => x.User);

        Page<Post> posts = await _postRepository.Paginate(request.Pagination, query);
        IEnumerable<PostListDTO> dtos = PostMapper.MapEntitiesToListDTOs(posts);
        return PageableResponse<Post, PostListDTO>.Get(posts, dtos);
    }
}
