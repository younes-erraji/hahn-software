using HahnSoftware.Domain.Entities;
using HahnSoftware.Domain.Interfaces.Repositories;

namespace HahnSoftware.Infrastructure.Persistence.Repositories;

public class PostBookmarkRepository : EntityRepository<PostBookmark>, IPostBookmarkRepository
{
    public PostBookmarkRepository(HahnSoftwareDbContext context) : base(context)
    {

    }
}
