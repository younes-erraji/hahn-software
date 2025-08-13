using HahnSoftware.Application.Posts.DTO;
using MediatR;

namespace HahnSoftware.Application.Posts.Queries;

public class GetPostQuery : IRequest<PostDetailDTO>
{
    public string Slug { get; set; }

    public GetPostQuery(string slug)
    {
        Slug = slug;
    }
}
