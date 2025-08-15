using HahnSoftware.Domain.Entities;
using HahnSoftware.Application.RESTful;
using HahnSoftware.Domain.Interfaces.Repositories;
using HahnSoftware.Domain.Interfaces.Services;

using MediatR;

namespace HahnSoftware.Application.Comments.Commands.DeletePost;

public class DeleteCommentCommandHandler : IRequestHandler<DeleteCommentCommand, Response>
{
    private readonly ICommentRepository _commentRepository;
    private readonly IUserService _userService;

    public DeleteCommentCommandHandler(IPostRepository postRepository, ICommentRepository commentRepository, IUserService userService)
    {
        _commentRepository = commentRepository;
        _userService = userService;
    }

    public async Task<Response> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
    {
        Guid userId = _userService.GetUserIdentifier();
        Comment comment = await _commentRepository.Get(request.CommentId);
        if (comment.UserId == userId)
        {
            comment.Delete();
            await _commentRepository.Update(comment, cancellationToken);
            await _commentRepository.SaveChanges(cancellationToken);

            return Response.Success();
        }

        return Response.Forbidden("You do not have the ability to delete this comment");
    }
}