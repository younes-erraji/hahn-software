using HahnSoftware.Application.RESTful;
using HahnSoftware.Domain.Interfaces.Repositories;
using HahnSoftware.Domain.Interfaces.Services;
using HahnSoftware.Domain.Entities;

using MediatR;

namespace HahnSoftware.Application.Comments.Commands.DeleteCommentReaction;

public class DeleteCommentReactionCommandHandler : IRequestHandler<DeleteCommentReactionCommand, Response>
{
    private readonly IUserService _userService;
    private readonly ICommentRepository _commentRepository;

    public DeleteCommentReactionCommandHandler(ICommentRepository commentRepository, IUserService userService)
    {
        _userService = userService;
        _commentRepository = commentRepository;
    }

    public async Task<Response> Handle(DeleteCommentReactionCommand request, CancellationToken cancellationToken)
    {
        Guid userId = _userService.GetUserIdentifier();
        Comment comment = await _commentRepository.Get(request.CommentId);
        comment.DeleteReaction(userId);
        await _commentRepository.Update(comment);
        await _commentRepository.SaveChanges();
        return Response.Success();
    }
}