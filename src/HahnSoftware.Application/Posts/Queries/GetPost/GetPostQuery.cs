using HahnSoftware.Application.Posts.DTO;
using MediatR;

namespace HahnSoftware.Application.Posts.Queries.GetPost;

public class GetPostQuery : IRequest<PostDetailDTO>
{
    public string Slug { get; private set; }

    public GetPostQuery(string slug)
    {
        Slug = slug;
    }
}
