using HahnSoftware.Application.Extensions;
using HahnSoftware.Application.Posts.DTO;
using HahnSoftware.Domain.Entities;

namespace HahnSoftware.Application.Posts.Mappers;

public static class PostMapper
{
    public static PostDetailDTO MapEntityToDetailDTO(Post post)
    {
        ArgumentNullException.ThrowIfNull(post);

        return new PostDetailDTO
        {
            Id = post.Id,
            Title = post.Title,
            Body = post.Body,
            User = post.User?.FullName,
            CreationDate = post.CreationDate,
            UpdateDate = post.UpdateDate,
        };
    }

    public static PostListDTO MapEntityToListDTO(Post post)
    {
        ArgumentNullException.ThrowIfNull(post, nameof(post));

        return new PostListDTO
        {
            Id = post.Id,
            Title = post.Title,
            Body = post.Body,
            User = post.User?.FullName,
            CreationDate = post.CreationDate,
            UpdateDate = post.UpdateDate
        };
    }

    public static IEnumerable<PostListDTO> MapEntitiesToListDTOs(IEnumerable<Post> posts)
    {
        if (posts.IsNotEmpty())
        {
            List<PostListDTO> postsDtos = new();

            foreach (Post post in posts)
            {
                postsDtos.Add(MapEntityToListDTO(post));
            }

            return postsDtos;
        }

        return Enumerable.Empty<PostListDTO>();
    }
}
