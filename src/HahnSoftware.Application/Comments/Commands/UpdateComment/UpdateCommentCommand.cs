using HahnSoftware.Application.RESTful;

using MediatR;

namespace HahnSoftware.Application.Comments.Commands.UpdatePost;

public class UpdateCommentCommand : IRequest<Response>
{
    public Guid CommentId { get; private set; }
    public string Content { get; private set; }

    public UpdateCommentCommand(Guid commentId, string content)
    {
        CommentId = commentId;
        Content = content;
    }
}