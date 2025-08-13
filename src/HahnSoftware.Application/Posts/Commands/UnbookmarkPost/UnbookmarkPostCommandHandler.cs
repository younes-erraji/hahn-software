using HahnSoftware.Domain.Entities;
using HahnSoftware.Domain.Interfaces;
using HahnSoftware.Application.RESTful;

using MediatR;

namespace HahnSoftware.Application.Posts.Commands.CreatePost;

public class UnbookmarkPostCommandHandler : IRequestHandler<UnbookmarkPostCommand, Response>
{
    private readonly IPostRepository _postRepository;

    public UnbookmarkPostCommandHandler(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }

    public async Task<Response> Handle(UnbookmarkPostCommand request, CancellationToken cancellationToken)
    {
        Post post = await _postRepository.Get(request.Id);
        post.Unbookmark(null);
        await _postRepository.Update(post);
        await _postRepository.SaveChanges();

        return Response.Success();
    }
}