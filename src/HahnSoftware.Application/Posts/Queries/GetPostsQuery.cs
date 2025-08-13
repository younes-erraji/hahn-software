using HahnSoftware.Application.Posts.DTO;

using MediatR;

namespace HahnSoftware.Application.Posts.Queries;

public class GetPostsQuery : IRequest<IEnumerable<PostListDTO>> { }