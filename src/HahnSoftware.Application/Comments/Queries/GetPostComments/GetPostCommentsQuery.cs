using HahnSoftware.Application.Comments.DTO;
using MediatR;

namespace HahnSoftware.Application.Comments.Queries.GetPostComments;

public class GetPostCommentsQuery : IRequest<IEnumerable<CommentDetailDTO>>
{
    public Guid PostId { get; private set; }

    public GetPostCommentsQuery(Guid postId)
    {
        PostId = postId;
    }
}
