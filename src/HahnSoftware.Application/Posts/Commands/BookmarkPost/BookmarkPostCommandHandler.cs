using HahnSoftware.Domain.Entities;
using HahnSoftware.Domain.Interfaces;
using HahnSoftware.Application.RESTful;

using MediatR;

namespace HahnSoftware.Application.Posts.Commands.CreatePost;

public class BookmarkPostCommandHandler : IRequestHandler<BookmarkPostCommand, Response>
{
    private readonly IPostRepository _postRepository;

    public BookmarkPostCommandHandler(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }

    public async Task<Response> Handle(BookmarkPostCommand request, CancellationToken cancellationToken)
    {
        Post post = await _postRepository.Get(request.Id);
        post.Bookmark(null);
        await _postRepository.Update(post);
        await _postRepository.SaveChanges();

        return Response.Success();
    }
}