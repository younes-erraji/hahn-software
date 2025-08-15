using HahnSoftware.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HahnSoftware.Infrastructure.Persistence.Configurations;

public class PostReactionTypeConfiguration : EntityTypeConfiguration<PostReaction>
{
    public override void Configure(EntityTypeBuilder<PostReaction> builder)
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

        builder.Property(x => x.Type)
            .IsRequired(true);

        // Relations
        builder.HasOne(pr => pr.User)
            .WithMany()
            .HasForeignKey(pr => pr.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
