using HahnSoftware.Application.Posts.DTO;
using HahnSoftware.Domain.Entities;
using HahnSoftware.Domain.Pagination;

using MediatR;

namespace HahnSoftware.Application.Posts.Queries.GetPosts;

public class GetPostsQuery : IRequest<PageableResponse<Post, PostListDTO>>
{
    public string Search { get; private set; }
    public PaginationParam Pagination { get; private set; }

    public GetPostsQuery(string search, PaginationParam pagination)
    {
        Search = search;
        Pagination = pagination;
    }
}