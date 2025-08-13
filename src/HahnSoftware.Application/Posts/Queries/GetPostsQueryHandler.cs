using HahnSoftware.Domain.Interfaces;
using HahnSoftware.Domain.Entities;

using MediatR;
using HahnSoftware.Application.Posts.DTO;
using HahnSoftware.Application.Posts.Mappers;

namespace HahnSoftware.Application.Posts.Queries;

public class GetPostsQueryHandler : IRequestHandler<GetPostsQuery, IEnumerable<PostListDTO>>
{
    private readonly IPostRepository _postRepository;

    public GetPostsQueryHandler(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }

    public async Task<IEnumerable<PostListDTO>> Handle(GetPostsQuery request, CancellationToken cancellationToken)
    {
        List<Post> posts = await _postRepository.Get();
        return PostMapper.MapEntitiesToListDTOs(posts);
    }
}