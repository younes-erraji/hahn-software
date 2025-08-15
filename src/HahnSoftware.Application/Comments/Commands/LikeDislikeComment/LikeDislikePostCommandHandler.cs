using HahnSoftware.Domain.Entities;
using HahnSoftware.Domain.Interfaces.Repositories;
using HahnSoftware.Domain.Interfaces.Services;
using HahnSoftware.Application.RESTful;

using MediatR;

namespace HahnSoftware.Application.Comments.Commands.LikeComment;

public class LikeDislikeCommentCommandHandler : IRequestHandler<LikeDislikeCommentCommand, Response>
{
    private readonly IUserService _userService;
    private readonly ICommentRepository _commentRepository;

    public LikeDislikeCommentCommandHandler(ICommentRepository commentRepository, IUserService userService)
    {
        _userService = userService;
        _commentRepository = commentRepository;
    }

    public async Task<Response> Handle(LikeDislikeCommentCommand request, CancellationToken cancellationToken)
    {
        Guid userId = _userService.GetUserIdentifier();
        Comment comment = await _commentRepository.Get(request.CommentId);
        comment.LikeDislike(userId, request.Type);
        await _commentRepository.Update(comment);
        await _commentRepository.SaveChanges();
        return Response.Success();
    }
}