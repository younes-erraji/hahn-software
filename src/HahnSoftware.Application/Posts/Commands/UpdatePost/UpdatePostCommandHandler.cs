using HahnSoftware.Domain.Entities;
using HahnSoftware.Application.RESTful;

using MediatR;
using HahnSoftware.Domain.Interfaces.Repositories;

namespace HahnSoftware.Application.Posts.Commands.UpdatePost;

public class UpdatePostCommandHandler : IRequestHandler<UpdatePostCommand, Response>
{
    private readonly IPostRepository _postRepository;

    public UpdatePostCommandHandler(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }

    public async Task<Response> Handle(UpdatePostCommand request, CancellationToken cancellationToken)
    {
        Post post = await _postRepository.Get(request.Id);
        post.Update(request.Title, request.Body, request.Tags);

        await _postRepository.Update(post);
        await _postRepository.SaveChanges();

        return Response.Success();
    }
}