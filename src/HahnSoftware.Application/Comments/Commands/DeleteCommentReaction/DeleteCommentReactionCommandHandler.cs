using HahnSoftware.Application.RESTful;
using HahnSoftware.Domain.Interfaces.Repositories;
using HahnSoftware.Domain.Interfaces.Services;
using HahnSoftware.Domain.Entities;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace HahnSoftware.Application.Comments.Commands.DeleteCommentReaction;

public class DeleteCommentReactionCommandHandler : IRequestHandler<DeleteCommentReactionCommand, Response>
{
    private readonly IUserService _userService;
    private readonly ICommentRepository _commentRepository;
    private readonly ICommentReactionRepository _commentReactionRepository;

    public DeleteCommentReactionCommandHandler(ICommentRepository commentRepository, IUserService userService, ICommentReactionRepository commentReactionRepository)
    {
        _userService = userService;
        _commentRepository = commentRepository;
        _commentReactionRepository = commentReactionRepository;
    }

    public async Task<Response> Handle(DeleteCommentReactionCommand request, CancellationToken cancellationToken)
    {
        Guid userId = _userService.GetUserIdentifier();
        if (await _commentRepository.Exists(request.CommentId))
        {
            CommentReaction? reaction = await _commentReactionRepository.Query(x => x.UserId == userId && x.CommentId == request.CommentId).FirstOrDefaultAsync();

            if (reaction is not null)
            {
                await _commentReactionRepository.Delete(reaction);
                await _commentReactionRepository.SaveChanges();
            }

            return Response.Success();
        }

        return Response.NotFound();
    }
}