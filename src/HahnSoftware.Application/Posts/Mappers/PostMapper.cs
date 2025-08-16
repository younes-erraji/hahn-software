using HahnSoftware.Application.Extensions;
using HahnSoftware.Application.Posts.DTO;
using HahnSoftware.Domain.Entities;
using HahnSoftware.Domain.Enums;
using HahnSoftware.Domain.Pagination;

namespace HahnSoftware.Application.Posts.Mappers;

public static class PostMapper
{
    public static PostDetailDTO MapEntityToDetailDTO(Post post)
    {
        ArgumentNullException.ThrowIfNull(post);

        return new PostDetailDTO
        {
            Id = post.Id,
            Slug = post.Slug,
            Title = post.Title,
            Body = post.Body,
            Tags = post.Tags,
            User = post.User.FullName,
            CreationDate = post.CreationDate,
            UpdateDate = post.UpdateDate,
            Likes = post.Reactions.Where(x => x.Type == ReactionType.Like).Count(),
            Dislikes = post.Reactions.Where(x => x.Type == ReactionType.Dislike).Count()
        };
    }

    public static PostListDTO MapEntityToListDTO(Post post)
    {
        ArgumentNullException.ThrowIfNull(post, nameof(post));

        return new PostListDTO
        {
            Id = post.Id,
            Slug = post.Slug,
            Title = post.Title,
            Body = post.Body,
            User = post.User.FullName,
            Tags = post.Tags,
            CreationDate = post.CreationDate,
            UpdateDate = post.UpdateDate
        };
    }

    public static IEnumerable<PostListDTO> MapEntitiesToListDTOs(List<Post> posts)
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
