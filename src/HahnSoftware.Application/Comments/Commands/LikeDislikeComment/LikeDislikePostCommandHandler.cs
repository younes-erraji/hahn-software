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
    private readonly ICommentReactionRepository _commentReactionRepository;

    public LikeDislikeCommentCommandHandler(ICommentRepository commentRepository, IUserService userService, ICommentReactionRepository commentReactionRepository)
    {
        _userService = userService;
        _commentRepository = commentRepository;
        _commentReactionRepository = commentReactionRepository;
    }

    public async Task<Response> Handle(LikeDislikeCommentCommand request, CancellationToken cancellationToken)
    {
        Guid userId = _userService.GetUserIdentifier();
        if (await _commentRepository.Exists(request.CommentId))
        {
            CommentReaction reaction = new CommentReaction(request.CommentId, userId, request.Type);
            await _commentReactionRepository.Create(reaction);
            await _commentReactionRepository.SaveChanges();
            return Response.Success();
        }

        return Response.NotFound();
    }
}