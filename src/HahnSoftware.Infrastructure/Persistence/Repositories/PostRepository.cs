using HahnSoftware.Domain.Entities;
using HahnSoftware.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HahnSoftware.Infrastructure.Persistence.Repositories;

public sealed class PostRepository : EntityRepository<Post>, IPostRepository
{
    public PostRepository(HahnSoftwareDbContext context) : base(context)
    {
    }

    public async Task<Post> Get(string slug, CancellationToken cancellationToken = default)
    {
        Post? post = await Query(p => p.Slug == slug)
            .Include(p => p.User)
            .Include(p => p.Reactions)
            .Include(p => p.Attachments)
            .FirstOrDefaultAsync(cancellationToken);

        if (post is null)
        {
            throw new ArgumentNullException(nameof(post));
        }

        return post;
    }
}