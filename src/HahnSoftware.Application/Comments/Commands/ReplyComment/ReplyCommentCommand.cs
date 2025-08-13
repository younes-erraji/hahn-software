using HahnSoftware.Application.RESTful;
using MediatR;

namespace HahnSoftware.Application.Comments.Commands.ReplyComment;

public class ReplyCommentCommand : IRequest<Response>
{
    public Guid CommentId { get; set; }
    public string Content { get; set; }
}
