using HahnSoftware.Application.Comments.DTO;
using HahnSoftware.Application.Comments.Mappers;
using HahnSoftware.Domain.Interfaces.Repositories;
using HahnSoftware.Domain.Entities;

using Microsoft.EntityFrameworkCore;

using MediatR;

namespace HahnSoftware.Application.Comments.Queries.GetPostComments;

public class GetPostCommentsQueryHandler : IRequestHandler<GetPostCommentsQuery, IEnumerable<CommentDetailDTO>>
{
    private readonly ICommentRepository _commentRepository;

    public GetPostCommentsQueryHandler(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }

    public async Task<IEnumerable<CommentDetailDTO>> Handle(GetPostCommentsQuery request, CancellationToken cancellationToken)
    {
        List<Comment> comments = await _commentRepository
            .Query(x => x.Id == request.PostId)
            .Include(x => x.User)
            .Include(x => x.Replies)
            /*
            .Select(x =>
            {
                new CommentDetailDTO
                {
                    Id = x.Id,
                    Content = x.Content,
                    UpdateDate = x.UpdateDate,
                    CreationDate = x.CreationDate,
                    User = x.User.FirstName + " " + x.User.LastName,
                    Likes = x.Reactions.Where(x => x.Type == Domain.Enums.ReactionType.Like).LongCount(),
                    Dislikes = x.Reactions.Where(x => x.Type == Domain.Enums.ReactionType.Dislike).LongCount(),
                };
            })
            */
            .ToListAsync(cancellationToken);

        return CommentMapper.MapEntitiesToDetailDTOs(comments);
    }
}
