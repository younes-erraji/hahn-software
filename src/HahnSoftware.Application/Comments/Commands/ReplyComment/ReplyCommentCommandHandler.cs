using HahnSoftware.Application.RESTful;
using HahnSoftware.Domain.Entities;

using MediatR;
using HahnSoftware.Domain.Interfaces.Repositories;
using HahnSoftware.Domain.Interfaces.Services;

namespace HahnSoftware.Application.Comments.Commands.ReplyComment;

public class ReplyCommentCommandHandler : IRequestHandler<ReplyCommentCommand, Response>
{
    private readonly IUserService _userService;
    private readonly ICommentRepository _commentRepository;

    public ReplyCommentCommandHandler(ICommentRepository commentRepository, IUserService userService)
    {
        _userService = userService;
        _commentRepository = commentRepository;
    }

    public async Task<Response> Handle(ReplyCommentCommand request, CancellationToken cancellationToken)
    {
        Guid userId = _userService.GetUserIdentifier();
        Comment? comment = await _commentRepository.Get(request.CommentId);
        Comment reply = new Comment(request.Content, comment.PostId, userId, request.CommentId);

        await _commentRepository.Create(reply, cancellationToken);
        await _commentRepository.SaveChanges(cancellationToken);

        return Response.Success();
    }
}
