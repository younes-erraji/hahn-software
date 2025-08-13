using HahnSoftware.Domain.Entities;
using HahnSoftware.Domain.Interfaces;
using HahnSoftware.Domain.Exceptions;
using HahnSoftware.Application.RESTful;

using MediatR;

namespace HahnSoftware.Application.Posts.Commands.DeletePost;

public class DeleteCommentCommandHandler : IRequestHandler<DeleteCommentCommand, Response>
{
    private readonly ICommentRepository _commentRepository;

    public DeleteCommentCommandHandler(IPostRepository postRepository, ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }

    public async Task<Response> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
    {
        Comment comment = await _commentRepository.Get(request.CommentId);
        comment.Delete();
        await _commentRepository.Update(comment, cancellationToken);
        await _commentRepository.SaveChanges(cancellationToken);

        return Response.Success();
    }
}