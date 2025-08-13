using HahnSoftware.Domain.Entities;
using HahnSoftware.Domain.Interfaces;
using HahnSoftware.Application.RESTful;

using MediatR;

namespace HahnSoftware.Application.Posts.Commands.CreatePost;

public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, Response>
{
    private readonly IPostRepository _postRepository;

    public CreatePostCommandHandler(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }

    public async Task<Response> Handle(CreatePostCommand request, CancellationToken cancellationToken)
    {
        Post post = new Post(request.Title, request.Body, request.Tags, null);

        await _postRepository.Create(post);
        await _postRepository.SaveChanges();

        return Response.Success();
    }
}