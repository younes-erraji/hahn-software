using HahnSoftware.Application.Comments.DTO;
using HahnSoftware.Application.Comments.Mappers;
using HahnSoftware.Domain.Interfaces.Repositories;
using HahnSoftware.Domain.Entities;

using Microsoft.EntityFrameworkCore;

using MediatR;

namespace HahnSoftware.Application.Comments.Queries;

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
            .ToListAsync(cancellationToken);

        return CommentMapper.MapEntitiesToDetailDTOs(comments);
    }
}
