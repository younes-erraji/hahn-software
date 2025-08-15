using HahnSoftware.Application.RESTful;

using MediatR;

namespace HahnSoftware.Application.Comments.Commands.CreateComment;

public class CreateCommentCommand : IRequest<Response>
{
    public Guid PostId { get; private set; }
    public string Content { get; private set; }

    public CreateCommentCommand(Guid postId, string content)
    {
        PostId = postId;
        Content = content;
    }
}