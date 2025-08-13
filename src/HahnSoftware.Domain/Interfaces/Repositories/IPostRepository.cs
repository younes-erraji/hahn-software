using HahnSoftware.Domain.Entities;

namespace HahnSoftware.Domain.Interfaces.Repositories;

public interface IPostRepository : IEntityRepository<Post>
{
    Task<Post> Get(string slug, CancellationToken cancellationToken = default);
}
