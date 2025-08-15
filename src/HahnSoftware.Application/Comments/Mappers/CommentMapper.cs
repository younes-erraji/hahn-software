using HahnSoftware.Application.Comments.DTO;
using HahnSoftware.Application.Extensions;
using HahnSoftware.Domain.Entities;

namespace HahnSoftware.Application.Comments.Mappers;

public static class CommentMapper
{
    public static CommentReplyDTO MapEntityToReplyDTO(Comment comment)
    {
        ArgumentNullException.ThrowIfNull(comment, nameof(comment));

        return new CommentReplyDTO
        {
            Id = comment.Id,
            Content = comment.Content,
            User = comment.User?.FullName,
            CreationDate = comment.CreationDate,
            UpdateDate = comment.UpdateDate
        };
    }

    public static IEnumerable<CommentReplyDTO> MapEntitiesToReplyDTOs(IEnumerable<Comment> comments)
    {
        if (comments.IsNotEmpty())
        {
            List<CommentReplyDTO> commentsDtos = new();

            foreach (Comment comment in comments)
            {
                commentsDtos.Add(MapEntityToReplyDTO(comment));
            }

            return commentsDtos;
        }

        return Enumerable.Empty<CommentReplyDTO>();
    }

    public static CommentDetailDTO MapEntityToDetailDTO(Comment comment)
    {
        ArgumentNullException.ThrowIfNull(comment, nameof(comment));

        return new CommentDetailDTO
        {
            Id = comment.Id,
            Content = comment.Content,
            User = comment.User?.FullName,
            CreationDate = comment.CreationDate,
            UpdateDate = comment.UpdateDate,
            Replies = MapEntitiesToReplyDTOs(comment.Replies),
            Likes = comment.Reactions.Where(x => x.Type == Domain.Enums.ReactionType.Like).LongCount(),
            Dislikes = comment.Reactions.Where(x => x.Type == Domain.Enums.ReactionType.Dislike).LongCount(),
        };
    }

    public static IEnumerable<CommentDetailDTO> MapEntitiesToDetailDTOs(IEnumerable<Comment> comments)
    {
        if (comments.IsNotEmpty())
        {
            List<CommentDetailDTO> commentsDtos = new();

            foreach (Comment comment in comments)
            {
                commentsDtos.Add(MapEntityToDetailDTO(comment));
            }

            return commentsDtos;
        }

        return Enumerable.Empty<CommentDetailDTO>();
    }
}
