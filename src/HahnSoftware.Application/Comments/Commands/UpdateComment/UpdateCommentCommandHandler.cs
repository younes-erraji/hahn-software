using HahnSoftware.Domain.Entities;
using HahnSoftware.Domain.Exceptions;
using HahnSoftware.Application.RESTful;

using MediatR;
using HahnSoftware.Domain.Interfaces.Repositories;

namespace HahnSoftware.Application.Posts.Commands.UpdatePost;

public class UpdateCommentCommandHandler : IRequestHandler<UpdateCommentCommand, Response>
{
    private readonly ICommentRepository _commentRepository;
    private readonly IPostRepository _postRepository;

    public UpdateCommentCommandHandler(IPostRepository postRepository, ICommentRepository commentRepository)
    {
        _postRepository = postRepository;
        _commentRepository = commentRepository;
    }

    public async Task<Response> Handle(UpdateCommentCommand request, CancellationToken cancellationToken)
    {
        Comment comment = await _commentRepository.Get(request.CommentId);
        comment.Update(request.Content);
        await _commentRepository.Update(comment, cancellationToken);
        await _commentRepository.SaveChanges(cancellationToken);

        return Response.Success();
    }
}