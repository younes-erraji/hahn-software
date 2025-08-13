using HahnSoftware.Application.RESTful;
using HahnSoftware.Domain.Interfaces;
using HahnSoftware.Domain.Entities;

using MediatR;

namespace HahnSoftware.Application.Comments.Commands.ReplyComment;

public class ReplyCommentCommandHandler : IRequestHandler<ReplyCommentCommand, Response>
{
    private readonly ICommentRepository _commentRepository;

    public ReplyCommentCommandHandler(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }

    public async Task<Response> Handle(ReplyCommentCommand request, CancellationToken cancellationToken)
    {

        Comment? comment = await _commentRepository.Get(request.CommentId);
        Comment reply = new Comment(request.Content, comment.PostId, null, request.CommentId);

        await _commentRepository.Create(reply, cancellationToken);
        await _commentRepository.SaveChanges(cancellationToken);

        return Response.Success();
    }
}
