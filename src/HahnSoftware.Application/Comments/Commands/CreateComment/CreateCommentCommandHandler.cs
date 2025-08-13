using HahnSoftware.Domain.Entities;
using HahnSoftware.Domain.Exceptions;
using HahnSoftware.Application.RESTful;

using MediatR;
using HahnSoftware.Domain.Interfaces.Repositories;
using HahnSoftware.Domain.Interfaces.Services;

namespace HahnSoftware.Application.Posts.Commands.CreatePost;

public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, Response>
{
    private readonly ICommentRepository _commentRepository;
    private readonly IPostRepository _postRepository;
    private readonly IUserService _userService;

    public CreateCommentCommandHandler(IPostRepository postRepository, ICommentRepository commentRepository, IUserService userService)
    {
        _postRepository = postRepository;
        _commentRepository = commentRepository;
        _userService = userService;
    }

    public async Task<Response> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
    {
        if (await _postRepository.Exists(request.PostId))
        {
            Guid userId = _userService.GetUserIdentifier();
            Comment comment = new Comment(request.Content, request.PostId, userId);

            await _commentRepository.Create(comment, cancellationToken);
            await _commentRepository.SaveChanges(cancellationToken);

            return Response.Success();
        }

        throw new NotFoundException();
    }
}
