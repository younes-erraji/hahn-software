using HahnSoftware.Domain.Entities;
using HahnSoftware.Domain.Interfaces;
using HahnSoftware.Domain.Exceptions;
using HahnSoftware.Application.RESTful;

using MediatR;

namespace HahnSoftware.Application.Posts.Commands.CreatePost;

public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, Response>
{
    private readonly ICommentRepository _commentRepository;
    private readonly IPostRepository _postRepository;

    public CreateCommentCommandHandler(IPostRepository postRepository, ICommentRepository commentRepository)
    {
        _postRepository = postRepository;
        _commentRepository = commentRepository;
    }

    public async Task<Response> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
    {
        if (await _postRepository.Exists(request.PostId))
        {
            Comment comment = new Comment(request.Content, request.PostId, null);
            await _commentRepository.Create(comment, cancellationToken);
            await _commentRepository.SaveChanges(cancellationToken);

            return Response.Success();
        }

        throw new NotFoundException();
    }
}