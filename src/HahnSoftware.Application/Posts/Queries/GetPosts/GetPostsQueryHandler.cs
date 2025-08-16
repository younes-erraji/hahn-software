using HahnSoftware.Domain.Entities;
using HahnSoftware.Domain.Pagination;
using HahnSoftware.Domain.Interfaces.Repositories;
using HahnSoftware.Application.Posts.Mappers;
using HahnSoftware.Application.Posts.DTO;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace HahnSoftware.Application.Posts.Queries.GetPosts;

public class GetPostsQueryHandler : IRequestHandler<GetPostsQuery, PageableResponse<Post, PostListDTO>>
{
    private readonly IPostRepository _postRepository;

    public GetPostsQueryHandler(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }

    public async Task<PageableResponse<Post, PostListDTO>> Handle(GetPostsQuery request, CancellationToken cancellationToken)
    {
        IQueryable<Post> query = _postRepository
            .Query()
            .Include(x => x.User);

        Page<Post> posts = await _postRepository.Paginate(request.Pagination, query);
        IEnumerable<PostListDTO> dtos = PostMapper.MapEntitiesToListDTOs(posts);
        return PageableResponse<Post, PostListDTO>.Get(posts, dtos);
    }
}