using HahnSoftware.Application.Comments.DTO;

using MediatR;

namespace HahnSoftware.Application.Comments.Queries;

public class GetPostCommentsQuery : IRequest<IEnumerable<CommentDetailDTO>>
{
    public Guid PostId { get; set; }
}
