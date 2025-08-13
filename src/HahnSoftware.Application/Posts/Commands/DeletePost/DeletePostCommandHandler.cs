using HahnSoftware.Domain.Entities;
using HahnSoftware.Domain.Interfaces;
using HahnSoftware.Application.RESTful;

using MediatR;

namespace HahnSoftware.Application.Posts.Commands.DeletePost;

public class DeletePostCommandHandler : IRequestHandler<DeletePostCommand, Response>
{
    private readonly IPostRepository _postRepository;

    public DeletePostCommandHandler(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }

    public async Task<Response> Handle(DeletePostCommand request, CancellationToken cancellationToken)
    {
        Post post = await _postRepository.Get(request.Id);
        post.Delete();
        await _postRepository.Update(post);
        await _postRepository.SaveChanges();
        return Response.Success();
    }
}