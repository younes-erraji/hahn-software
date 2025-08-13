using HahnSoftware.Domain.Entities;
using HahnSoftware.Domain.Interfaces;
using HahnSoftware.Application.Posts.DTO;

using MediatR;
using HahnSoftware.Application.Posts.Mappers;

namespace HahnSoftware.Application.Posts.Queries;

public class GetPostQueryHandler : IRequestHandler<GetPostQuery, PostDetailDTO>
{
    private readonly IPostRepository _postRepository;

    public GetPostQueryHandler(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }

    public async Task<PostDetailDTO> Handle(GetPostQuery request, CancellationToken cancellationToken)
    {
        Post post = await _postRepository.Get(request.Slug);
        return PostMapper.MapEntityToDetailDTO(post);
    }
}