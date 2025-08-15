using HahnSoftware.Domain.Entities;

using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HahnSoftware.Infrastructure.Persistence.Configurations;

public class PostBookmarkTypeConfiguration : EntityTypeConfiguration<PostBookmark>
{
    public override void Configure(EntityTypeBuilder<PostBookmark> builder)
    {
        base.Configure(builder);

        builder.HasIndex(x => new
        {
            x.UserId,
            x.PostId
        }).IsUnique(true);

        builder.Property(x => x.UserId)
            .IsRequired(true);

        builder.Property(x => x.PostId)
            .IsRequired(true);
    }
}
